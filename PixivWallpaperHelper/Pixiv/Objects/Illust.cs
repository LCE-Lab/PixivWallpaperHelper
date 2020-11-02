using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using static PixivWallpaperHelper.Pixiv.Objects.IllustTypes;

namespace PixivWallpaperHelper.Pixiv.Objects
{
    public partial class Illust
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("image_urls")]
        public ImageUrls ImageUrls { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("restrict")]
        public long Restrict { get; set; }

        [JsonProperty("user")]
        public ListUser User { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("tools")]
        public string[] Tools { get; set; }

        [JsonProperty("create_date")]
        public DateTimeOffset CreateDate { get; set; }

        [JsonProperty("page_count")]
        public long PageCount { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("sanity_level")]
        public long SanityLevel { get; set; }

        [JsonProperty("x_restrict")]
        public long XRestrict { get; set; }

        //[JsonProperty("series")]
        //public Series Series { get; set; }

        [JsonProperty("meta_single_page")]
        public IllustSinglePage MetaSinglePage { get; set; }

        [JsonProperty("meta_pages")]
        public IllustPage[] MetaPages { get; set; }

        [JsonProperty("total_view")]
        public long TotalView { get; set; }

        [JsonProperty("total_bookmarks")]
        public long TotalBookmarks { get; set; }

        [JsonProperty("is_bookmarked")]
        public bool IsBookmarked { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("is_muted")]
        public bool IsMuted { get; set; }
    }

    public partial class ListUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("profile_image_urls")]
        public ListProfileImageUrls ProfileImageUrls { get; set; }

        [JsonProperty("is_followed")]
        public bool IsFollowed { get; set; }
    }

    public partial class ListProfileImageUrls
    {
        [JsonProperty("medium")]
        public Uri Medium { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) { return null; }
            string value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "illust":
                    return TypeEnum.Illust;
                case "manga":
                    return TypeEnum.Manga;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            TypeEnum value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Illust:
                    serializer.Serialize(writer, "illust");
                    return;
                case TypeEnum.Manga:
                    serializer.Serialize(writer, "manga");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}

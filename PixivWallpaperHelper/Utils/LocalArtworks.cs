using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PixivWallpaperHelper.Utils
{
    public class LocalArtwork
    { 
        public string Title { get; set; }
        public string Author { get; set; }
        public string WebUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Path { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsChanged { get; set; }
    }

    public class LocalArtworksHelper
    {
        private Dictionary<string, LocalArtwork> localArtworks;
        private static readonly string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}\\PixivWallpaperInfo.json";

        public LocalArtworksHelper()
        {
            FetchFromFile();
        }

        public bool AddAndSaveArtwork(Bitmap image, string path, string title, string author, string Id, string imageUrl)
        {
            if (localArtworks.ContainsKey(Id)) { return false; }
            image.Save(path);
            LocalArtwork newItem = new LocalArtwork
            {
                Title = title,
                Author = author,
                WebUrl = $"https://www.pixiv.net/artworks/{Id}",
                ImageUrl = imageUrl,
                Path = path,
                IsCurrent = false,
                IsChanged = false
            };
            localArtworks.Add(Id, newItem);
            return true;
        }

        public bool getArtworkInfo(string Id, out LocalArtwork value) {
            return localArtworks.TryGetValue(Id, out value);
        }

        private void FetchFromFile()
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                localArtworks = JsonConvert.DeserializeObject<Dictionary<string, LocalArtwork>>(jsonString);
            }
            else
            {
                localArtworks = new Dictionary<string, LocalArtwork>();
            }
        }

        public void Save()
        {
            string jsonString = JsonConvert.SerializeObject(localArtworks, Formatting.Indented);
            File.WriteAllText(path, jsonString);
        }
    }
}
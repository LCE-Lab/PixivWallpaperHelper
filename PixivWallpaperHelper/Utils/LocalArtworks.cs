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
        public bool SetCurrentWallpaper(string Id)
        {
            if (!LocalArtworks.ContainsKey(Id)) { return false; }
            LocalArtworks[Id].IsCurrent = true;
            foreach (string key in LocalArtworks.Keys)
            {
                if (!LocalArtworks.ContainsKey(key)) { continue; }
                if (LocalArtworks[key].IsCurrent && !key.Equals(Id))
                {
                    LocalArtworks[Id].IsCurrent = false;
                    LocalArtworks[Id].IsChanged = true;
                }
            }
            return true;
        }

        public int GetUnchangedWallpaperCount()
        {
            int count = 0;
            foreach (string key in LocalArtworks.Keys)
            {
                if (!LocalArtworks.ContainsKey(key)) { continue; }
                if (!LocalArtworks[key].IsChanged) { count++; }
            }
            return count;
        }

        public void ClearChangedWallpaper()
        {
            foreach (string key in LocalArtworks.Keys)
            {
                if (!LocalArtworks.ContainsKey(key)) { continue; }
                if (LocalArtworks[key].IsChanged)
                {
                    LocalArtwork artworkToBeDelete = LocalArtworks[key];
                    if (File.Exists(artworkToBeDelete.Path)) { File.Delete(artworkToBeDelete.Path); }
                    _ = LocalArtworks.Remove(key);
                }
            }
        }

        public bool GetArtworkInfo(string Id, out LocalArtwork value) {
            return LocalArtworks.TryGetValue(Id, out value);
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
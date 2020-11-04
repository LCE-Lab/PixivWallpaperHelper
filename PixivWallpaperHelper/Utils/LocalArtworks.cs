using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

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
        private Dictionary<string, LocalArtwork> LocalArtworks;
        private static readonly string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}\\PixivWallpaperInfo.json";

        public LocalArtworksHelper()
        {
            FetchFromFile();
        }

        public bool AddAndSaveArtwork(Bitmap image, string path, string title, string author, string Id, string imageUrl)
        {
            if (LocalArtworks.ContainsKey(Id)) { return false; }
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
            LocalArtworks.Add(Id, newItem);
            return true;
        }

        public bool SetCurrentWallpaper(string Id)
        {
            if (!LocalArtworks.ContainsKey(Id)) { return false; }
            LocalArtworks[Id].IsCurrent = true;
            if (LocalArtworks[Id].IsChanged) { LocalArtworks[Id].IsChanged = false; }
            UnsetLastCurrent(Id);
            return true;
        }

        public void UnsetLastCurrent(string Id = "")
        {
            foreach (string key in LocalArtworks.Keys)
            {
                if (!LocalArtworks.ContainsKey(key)) { continue; }
                if (LocalArtworks[key].IsCurrent && !key.Equals(Id))
                {
                    LocalArtworks[key].IsCurrent = false;
                    LocalArtworks[key].IsChanged = true;
                }
            }
        }

        public int GetUnchangedWallpaperCount()
        {
            int count = 0;
            foreach (string key in LocalArtworks.Keys)
            {
                if (!LocalArtworks.ContainsKey(key)) { continue; }
                if (!LocalArtworks[key].IsChanged && !LocalArtworks[key].IsCurrent) { count++; }
            }
            return count;
        }

        public void ClearChangedWallpaper()
        {
            string[] keys = new string[LocalArtworks.Keys.Count];
            LocalArtworks.Keys.CopyTo(keys, 0);
            foreach (string key in keys)
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

        public bool GetArtworkInfo(string Id, out LocalArtwork value)
        {
            return LocalArtworks.TryGetValue(Id, out value);
        }

        private void FetchFromFile()
        {
            if (File.Exists(Path))
            {
                string jsonString = File.ReadAllText(Path);
                LocalArtworks = JsonConvert.DeserializeObject<Dictionary<string, LocalArtwork>>(jsonString);
            }
            else
            {
                LocalArtworks = new Dictionary<string, LocalArtwork>();
            }
        }

        public void Save()
        {
            string jsonString = JsonConvert.SerializeObject(LocalArtworks, Formatting.Indented);
            File.WriteAllText(Path, jsonString);
        }
    }
}
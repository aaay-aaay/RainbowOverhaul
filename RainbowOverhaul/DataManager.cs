using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RWCustom;
using UnityEngine;
using System.Reflection;


namespace Rainbow
{
    public static class DataManager
    {
        static DataManager()
        {

        }

        private static DirectoryInfo directory
        {
            get
            {
                return
                    new DirectoryInfo(string.Concat(new object[] {
                    Custom.RootFolderDirectory(),
                    "Mods",
                    Path.DirectorySeparatorChar,
                    "Rainbow",
                    Path.DirectorySeparatorChar
                    }));
            }
        }
        private static int slot
        {
            get
            {
                return RainbowScript.slot;
            }
        }


        /// <summary>
        /// Default Save Data of this mod. If this isn't needed, just leave it be.
        /// </summary>
        public static string defaultData
        {
            get { return string.Empty; }
        }


        public static List<Texture2D[]> mapDiscoveryTextures;
        public static List<List<string>> tempSheltersDiscovered;



        /// <summary>
        /// string you can save and load however you want.
        /// </summary>
        public static string data
        {
            set
            {
                if (_data != value)
                {
                    _data = value;
                    DataOnChange();
                }
            }
            get
            {
                return _data;
            }
        }
        private static string _data;


        /// <summary>
        /// Event when saved data is changed
        /// This is called when 1. LoadData, 2. Your mod changes data.
        /// </summary>
        public static void DataOnChange()
        {

        }

        /// <summary>
        /// Load your raw data from CompletelyOptional Mod.
        /// Call this by your own.
        /// Check dataTinkered boolean to see if saved data is tinkered or not.
        /// </summary>
        /// <returns>Loaded Data</returns>
        public static void LoadData()
        {
            try
            {
                string data = defaultData != null ? defaultData : string.Empty;
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Name.Substring(file.Name.Length - 4) != ".txt") { continue; }

                    if (file.Name.Substring(0, 4) == "data")
                    {
                        switch (file.Name.Substring(file.Name.Length - 5, 1))
                        {
                            case "1":
                                if (slot != 1) { continue; }
                                break;
                            case "2":
                                if (slot != 2) { continue; }
                                break;
                            case "3":
                                if (slot != 3) { continue; }
                                break;
                        }
                    }
                    else { continue; }

                    //Load Data
                    data = File.ReadAllText(file.FullName, Encoding.UTF8);
                    string key = data.Substring(0, 32);
                    data = data.Substring(32, data.Length - 32);
                    if (Custom.Md5Sum(data) != key)
                    {
                        _tinkered = true;
                    }
                    else
                    {
                        _tinkered = false;
                    }
                    data = Crypto.DecryptString(data, "InsertZandraMeme");
                }

                _data = data;
            }
            catch (Exception ex) { Debug.LogError(ex); }

            _data = defaultData;
        }

        /// <summary>
        /// If you want to see whether your data is tinkered or not.
        /// </summary>
        public static bool dataTinkered
        {
            get { return _tinkered; }
        }
        private static bool _tinkered = false;

        /// <summary>
        /// Save your raw data in file. bool is whether it succeed or not
        /// Call this by your own.
        /// </summary>
        /// <param name="data">Data you want to save in file</param>
        public static bool SaveData()
        {
            try
            {
                if (!directory.Exists)
                {
                    directory.Create();
                }


                string path = string.Concat(new object[] {
                directory.FullName,
                "data_",
                slot.ToString(),
                ".txt"
                });
                string enc = Crypto.EncryptString(_data, "InsertZandraMeme");
                string key = Custom.Md5Sum(enc);

                File.WriteAllText(path, key + enc);


                return true;
            }
            catch (Exception ex) { Debug.LogError(ex); }

            return false;
        }

        public static void BackUpData(string appendName)
        {
            string path = string.Concat(new object[]
            {
                directory.FullName,
                "data_",
                slot.ToString(),
                ".txt"
            });
            if (!File.Exists(path))
            {
                return;
            }
            string text = File.ReadAllText(path);
            string text2 = string.Concat(new object[]
            {
            directory.FullName,
            "data_",
            slot.ToString(),
            appendName,
            ".txt"
            });
            using (StreamWriter streamWriter = File.CreateText(text2))
            {
                streamWriter.Write(text);
            }

        }


        public static Texture2D ReadAtlasPNG(string pngName)
        {

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = string.Concat("Rainbow.Atlas.", pngName, ".txt");
            string result;
            byte[] bytes;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            bytes = System.Convert.FromBase64String(result);

            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);

            return tex;

        }



        public static Texture2D ReadExteriorPNG(string pngName)
        {
            try
            {
                string path = string.Concat(new object[] {
                directory.FullName,
                "Atlas",
                Path.DirectorySeparatorChar,
                pngName,
                ".png"
                });

                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(bytes);
                return tex;
            }
            catch (Exception e) { Debug.LogError(e); }
            return null;
        }

        public static void EncodeExteriorPNG()
        {
            string[] files = Directory.GetFiles(Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "encode") + Path.DirectorySeparatorChar.ToString(), "*.png", SearchOption.TopDirectoryOnly);
            foreach (string path in files)
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(path);
                    string encode = System.Convert.ToBase64String(bytes);
                    string newPath = path.Replace("png", "txt");

                    File.WriteAllText(newPath, encode);
                }
                catch (Exception ex)
                {
                    Debug.LogError(string.Concat("error while encoding ", path));
                    Debug.LogError(ex);
                }
            }


        }


        public static string ReadAtlasTXT(string txtName)
        {

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = string.Concat("Rainbow.Atlas.", txtName, ".txt");
            string result;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;

        }

    }
}

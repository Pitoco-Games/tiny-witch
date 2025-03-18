using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

namespace Utils.Save
{
    public class SaveService
    {
        private Dictionary<Type, SaveData> typeToSaveInstanceCacheDict = new();

        private const string SAVE_PATH = "/Save";

        public void SaveAll()
        {
            foreach (SaveData data in typeToSaveInstanceCacheDict.Values)
            {
                data.UpdateSaveData();
                Save(data);
            }
        }

        public void Save(SaveData data)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string filePath = GetSavePath(data.GetType());

            using (StreamWriter sw = new(File.Open(filePath, FileMode.Create, FileAccess.ReadWrite)))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data);
            }
        }

        public SaveData TryGetSavedData<T>() where T : SaveData, new()
        {
            SaveData data = null;
            if(!typeToSaveInstanceCacheDict.TryGetValue(typeof(T), out data))
            {
                data = TryLoad<T>(typeof(T));
                typeToSaveInstanceCacheDict.Add(typeof(T), data);
            }

            return data;
        }

        private static T TryLoad<T>(Type saveClassType) where T : SaveData, new()
        {
            string savePath = GetSavePath(saveClassType);

            if(!File.Exists(savePath))
            {
                return new T();
            }
            using (StreamReader sr = new StreamReader(savePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer _serializer = new JsonSerializer();
                T savedData = _serializer.Deserialize<T>(reader);
                savedData.HasSavedData = true;
                return savedData;
            }
        }

        private static string GetSavePath(Type saveClassType)
        {
            return @$"{Application.persistentDataPath}{SAVE_PATH}/{saveClassType.Name}.json";
        }
    }
}
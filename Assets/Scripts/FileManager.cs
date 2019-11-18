using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class FileManager {
    public static void Save(object entity, string path) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(Application.persistentDataPath + path);
        formatter.Serialize(stream, entity);
        stream.Close();
    }

    public static T GetFile<T>(string path) {
        if (File.Exists(Application.persistentDataPath + path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(Application.persistentDataPath + path, FileMode.Open);
            var entity = formatter.Deserialize(stream);
            stream.Close();
            return (T)entity;
        }
        return default(T);
    }

    public static void ManageFolder(string folder) {
        if (!Directory.Exists(folder)) {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, folder));
        }
    }

}

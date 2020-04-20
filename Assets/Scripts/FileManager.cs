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

    public static bool CheckFileExist(string paths) {
        return File.Exists(Application.persistentDataPath + paths);
    }

    public static void ManageFolder(string folder) {
        if (!Directory.Exists(folder)) {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, folder));
        }
    }

    public static void SaveToJson(object entity, string filename) {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + filename + ".json", JsonUtility.ToJson(entity));
    }

    public static void DeleteFile(string path) {
        Directory.Delete(Application.persistentDataPath + path, true);
    }

}

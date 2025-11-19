using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static GlobalData globalData;

        private static GlobalData GetGlobalData()
    {
        if (globalData == null)
        {
            globalData = Resources.Load<GlobalData>("GameData");
        }
        return globalData;
    }

    private static string SavePath => 
        Path.Combine(Application.persistentDataPath, "savedata.json");

    public static void SaveGame()
    {
        globalData = GetGlobalData();
        try
        {
            string json = JsonUtility.ToJson(globalData, true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"Game saved to {SavePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
        }
    }

    public static void LoadGame()
    {
        if (File.Exists(SavePath))
        {
                string json = File.ReadAllText(SavePath);
                globalData = JsonUtility.FromJson<GlobalData>(json);
                Debug.Log($"Game loaded from {SavePath}");
            
        } 
    }

    public static bool SaveExists()
    {
        return File.Exists(SavePath);
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Save file deleted");
        }
    }
}
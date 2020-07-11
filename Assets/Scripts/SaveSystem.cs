using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static string settingsPath = Path.Combine(Application.persistentDataPath, "Settings.bin");
    public static string levelPath = Path.Combine(Application.persistentDataPath, "level.bin");
    public static void SaveSettings(MainMenuSelection menu)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(settingsPath, FileMode.Create);

        SettingsData data = new SettingsData(menu);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SettingsData LoadSettings()
    {
        if (File.Exists(settingsPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(settingsPath, FileMode.Open);

            SettingsData readingData = formatter.Deserialize(stream) as SettingsData;
            stream.Close();
            return readingData;
        }
        else
        {
            Debug.LogError("Save File Not Found at " + settingsPath);
            return null;
        }
    }

    public static void SaveLastLevel()
    {
        FileStream stream = new FileStream(levelPath, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(stream);
        bw.Write(Level.lastLevel);
        stream.Close();
    }

    public static void LoadLastLevel()
    {
        if (File.Exists(levelPath))
        {
            FileStream stream = new FileStream(levelPath, FileMode.Open);
            BinaryReader br = new BinaryReader(stream);
            Level.lastLevel = br.ReadInt32();
            stream.Close();
        }
        else
        {
            Debug.LogError("Save File Not Found at " + levelPath);
        }
    }
}

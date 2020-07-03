using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string settingsPath = Path.Combine(Application.persistentDataPath, "Settings.bin");
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
}

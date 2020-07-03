using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public int qualityVal;
    public int difficultyVal;
    public bool violence;
    public int soundVal;
    public int musicVal;

    public SettingsData(MainMenuSelection menu)
    {
        qualityVal = menu.Quality;
        difficultyVal = menu.Difficulty;
        violence = menu.Violence;
        soundVal = menu.SoundVal;
        musicVal = menu.MusicVal;
    }
}

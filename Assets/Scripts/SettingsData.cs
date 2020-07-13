﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public int qualityVal;
    public int difficultyVal;
    public bool violence;
    public int soundVal;
    public bool soundMuted;
    public int musicVal;
    public bool musicMuted;
    public int sensitivity;

    public SettingsData(MainMenuSelection menu)
    {
        qualityVal = menu.Quality;
        difficultyVal = menu.Difficulty;
        violence = menu.Violence;
        soundVal = menu.SoundVal;
        soundMuted = menu.SoundMuted;
        musicVal = menu.MusicVal;
        musicMuted = menu.MusicMuted;
        sensitivity = menu.Sensitivity;
    }

    public SettingsData()
    {
        qualityVal = 1;
        difficultyVal = 1;
        violence = false;
        soundVal = 100;
        soundMuted = false;
        musicVal = 100;
        musicMuted = false;
        sensitivity = 50;
    }
}

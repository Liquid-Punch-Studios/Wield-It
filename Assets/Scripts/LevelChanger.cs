﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private int levelToLoad;
    private int currentlevel;
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    public void LoadNextLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        if (level < Level.levelCount)
        {
            Level.NextLevel(level);
            levelToLoad = level + 1;
        }
        else
        {
            levelToLoad = 0;
        }
        anim.SetTrigger("fadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}

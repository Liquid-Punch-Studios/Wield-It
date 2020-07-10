using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Level
{
    public static int lastLevel = 1;
    public static int levelCount = 3;

    public static void NextLevel(int currentLevel)
    {
        if (currentLevel == lastLevel && lastLevel < levelCount)
            lastLevel++;
    }

    public static IEnumerator LoadLevelDelayed(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        if (level <= levelCount)
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        else
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        TextMeshProUGUI tmp = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().buildIndex != 0)
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + SceneManager.GetActiveScene().buildIndex;
        else
            tmp.gameObject.SetActive(false);
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

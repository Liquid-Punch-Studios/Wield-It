using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    private Health playerHealth;
    public GameObject clickToRespawn;

    private GameObject escapeMenu;
    private GameObject settingsMenu;
    private GameObject darkMask;

    private bool isDead;
    private bool menuOpened = false;
    private bool isPlayed = false;
    private Controls controls;

    

    private void Start()
    {
        controls = GameManager.Instance.controls;
        escapeMenu = transform.Find("DarkMask").Find("EscapeMenu").gameObject;
        settingsMenu = transform.Find("DarkMask").Find("Settings").gameObject;
        darkMask = transform.Find("DarkMask").gameObject;
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    public void FixedUpdate()
    {
        isDead = playerHealth.Hp <= 0;

        if (isDead)
        {
            controls.Player.Disable();
            darkMask.SetActive(true);
            clickToRespawn.SetActive(true);
            if(!isPlayed)
                GameObject.Find("BloodSounds").GetComponent<AudioPlayer>().PlayRandom();
            isPlayed = true;
        }
        else
        {
            controls.Player.Enable();
            clickToRespawn.SetActive(false);
            darkMask.SetActive(false);
        }

        if (controls.UI.MouseClick.triggered)
        {
            isDead = false;
            isPlayed = false;
        }
            

        if (!menuOpened && controls.Player.EscapeMenu.triggered)
        {
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
            menuOpened = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (menuOpened && controls.Player.EscapeMenu.triggered)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            menuOpened = false;
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        }
        if (menuOpened)
        {
            darkMask.SetActive(true);
            escapeMenu.SetActive(true);
            
        }
        else if (!isDead)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            settingsMenu.SetActive(false);
            escapeMenu.SetActive(false);
            darkMask.SetActive(false);
        }
    }

    public void ResumeClick()
    {
        Time.timeScale = 1;
        menuOpened = false;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        darkMask.SetActive(false);
        escapeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartClick()
    {
        Time.timeScale = 1;
        menuOpened = false;
        escapeMenu.SetActive(false);
        darkMask.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SettingsClick()
    {
        escapeMenu.SetActive(false);
        settingsMenu.SetActive(true);
        Load();
    }

    public void MainMenuClick()
    {
        menuOpened = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitClick()
    {
        Application.Quit();
    }

    public void BackClick()
    {
        settingsMenu.SetActive(false);
        escapeMenu.SetActive(true);
    }


    public void Violence()
    {
        Screen.fullScreen = !Screen.fullScreen;
        GameObject.Find("Violence Value").GetComponent<TextMeshProUGUI>().text = Screen.fullScreen ? "Windowed" : "Fullscreen";
    }

    float AddPercent(float value, int percent) => (Mathf.RoundToInt(value * 100) + percent) / 100f;
    int GetPercent(float value) => Mathf.RoundToInt(value * 100);

    public void MusicDecrease()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.MusicVolume = AddPercent(settings.MusicVolume, -10);
        GameObject.Find("Music Value").GetComponent<TextMeshProUGUI>().text = "%" + GetPercent(settings.MusicVolume);
    }

    public void MusicIncrease()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.MusicVolume = AddPercent(settings.MusicVolume, 10);
        GameObject.Find("Music Value").GetComponent<TextMeshProUGUI>().text = "%" + GetPercent(settings.MusicVolume);
    }

    public void SoundDecrease()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.EffectsVolume = AddPercent(settings.EffectsVolume, -10);
        GameObject.Find("Sound Value").GetComponent<TextMeshProUGUI>().text = "%" + GetPercent(settings.EffectsVolume);
    }

    public void SoundIncrease()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.EffectsVolume = AddPercent(settings.EffectsVolume, 10);
        GameObject.Find("Sound Value").GetComponent<TextMeshProUGUI>().text = "%" + GetPercent(settings.EffectsVolume);
    }

    public void SensitivityDecrease()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.Sensitivity = AddPercent(settings.Sensitivity, -5);
        GameObject.Find("Sensitivity Value").GetComponent<TextMeshProUGUI>().text = settings.Sensitivity.ToString();
    }

    public void SensitivityIncrease()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.Sensitivity = AddPercent(settings.Sensitivity, 5);
        GameObject.Find("Sensitivity Value").GetComponent<TextMeshProUGUI>().text = settings.Sensitivity.ToString();
    }

    public void DifficultyNext()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.Difficulty = (Difficulty)((int)(settings.Difficulty + 1) % Enum.GetNames(typeof(Difficulty)).Length);
        GameObject.Find("Difficulty Value").GetComponent<TextMeshProUGUI>().text = settings.Difficulty.ToString();
    }

    public void LanguageNext()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.Language = (Language)((int)(settings.Language + 1) % Enum.GetNames(typeof(Language)).Length);
        GameObject.Find("Language Value").GetComponent<TextMeshProUGUI>().text = settings.Language.ToString();
    }

    public void QualityNext()
    {
        var settings = SettingsManager.Instance.Settings;
        settings.GraphicsQuality = (settings.GraphicsQuality + 1) % QualitySettings.names.Length;
        GameObject.Find("Quality Value").GetComponent<TextMeshProUGUI>().text = QualitySettings.names[settings.GraphicsQuality];
    }

    private void Load()
    {
        var settings = SettingsManager.Instance.Settings;
        GameObject.Find("Music Value").GetComponent<TextMeshProUGUI>().text = "%" + GetPercent(settings.MusicVolume);
        GameObject.Find("Sound Value").GetComponent<TextMeshProUGUI>().text = "%" + GetPercent(settings.EffectsVolume);
        GameObject.Find("Sensitivity Value").GetComponent<TextMeshProUGUI>().text = settings.Sensitivity.ToString();
        GameObject.Find("Difficulty Value").GetComponent<TextMeshProUGUI>().text = settings.Difficulty.ToString();
        GameObject.Find("Language Value").GetComponent<TextMeshProUGUI>().text = settings.Language.ToString();
        GameObject.Find("Quality Value").GetComponent<TextMeshProUGUI>().text = QualitySettings.names[settings.GraphicsQuality];
        GameObject.Find("Violence Value").GetComponent<TextMeshProUGUI>().text = !Screen.fullScreen ? "Windowed" : "Fullscreen";
    }
}

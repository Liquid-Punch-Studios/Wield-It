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
    public Health playerHealth;
    public GameObject clickToRespawn;

    private GameObject escapeMenu;
    private GameObject settingsMenu;
    private GameObject darkMask;

    private bool isSettingsOpen;
    private bool isPaused = false;
    private Controls controls;

    private SettingsData settings;

    private string[] qualitySteps = { "LOW", "MEDIUM", "HIGH" };
    private string[] difficultySteps = { "EASY", "MEDIUM", "HARD" };

    private void Start()
    {
        settings = new SettingsData();
        escapeMenu = transform.Find("DarkMask").Find("EscapeMenu").gameObject;
        settingsMenu = transform.Find("DarkMask").Find("Settings").gameObject;
        darkMask = transform.Find("DarkMask").gameObject;
    }

    public void FixedUpdate()
    {
        if(playerHealth.Hp <= 0)
        {
            darkMask.SetActive(true);
            clickToRespawn.SetActive(true);
            if (controls.Player.Angle.triggered)
            {
                clickToRespawn.SetActive(false);
                darkMask.SetActive(false);
            }
                
        }
        if (!isPaused && controls.Player.EscapeMenu.triggered)
        {
            Time.timeScale = 0;

            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
            darkMask.SetActive(true);
            escapeMenu.SetActive(true);

            isPaused = true;
        }
    }

    private void Update()
    {
        if (isPaused && controls.Player.EscapeMenu.triggered)
        {
            Time.timeScale = 1;

            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
            darkMask.SetActive(false);
            escapeMenu.SetActive(false);
            settingsMenu.SetActive(false);

            isPaused = false;
        }
    }

    public void LoadSettings()
    {
        SettingsData data = SaveSystem.LoadSettings();
        settings.difficultyVal = data.difficultyVal;
        settings.qualityVal = data.qualityVal;
        settings.musicVal = data.musicVal;
        settings.musicMuted = data.musicMuted;
        settings.soundVal = data.soundVal;
        settings.soundMuted = data.soundMuted;
        settings.violence = data.violence;
        settings.sensitivity = data.sensitivity;
        GameObject.Find("Difficulty Value").GetComponent<TextMeshProUGUI>().text = difficultySteps[settings.difficultyVal];
        GameObject.Find("Quality Value").GetComponent<TextMeshProUGUI>().text = qualitySteps[settings.qualityVal];
        GameObject.Find("Music Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.musicVal;
        if (settings.musicMuted)
            GameObject.Find("Music Value").GetComponent<TextMeshProUGUI>().text = "Muted";
        GameObject.Find("Sound Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.soundVal;
        if (settings.soundMuted)
            GameObject.Find("Sound Value").GetComponent<TextMeshProUGUI>().text = "Muted";
        GameObject.Find("Violence Value").GetComponent<TextMeshProUGUI>().text = settings.violence ? "ON" : "OFF";
        GameObject.Find("Sensitivity Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.sensitivity;

    }

    private void OnEnable()
    {
        if (controls == null)
            controls = new Controls();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void ResumeClick()
    {
        Time.timeScale = 1;

        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        darkMask.SetActive(false);
        escapeMenu.SetActive(false);
        settingsMenu.SetActive(false);

        isPaused = false;
    }

    public void SettingsClick()
    {
        escapeMenu.SetActive(false);
        settingsMenu.SetActive(true);
        LoadSettings();
    }

    public void MainMenuClick()
    {
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
        SaveSystem.SaveSettings(settings);
    }

    public void Violence()
    {
        settings.violence = !settings.violence;
        GameObject.Find("Violence Value").GetComponent<TextMeshProUGUI>().text = settings.violence ? "ON" : "OFF";
    }

    public void MusicDecrease()
    {
        if (settings.musicVal > 0)
            settings.musicVal -= 10;
        GameObject.Find("Music Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.musicVal;
    }

    public void MusicIncrease()
    {
        if (settings.musicVal < 100)
            settings.musicVal += 10;
        GameObject.Find("Music Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.musicVal;
    }

    public void SoundDecrease()
    {
        if (settings.soundVal > 0)
            settings.soundVal -= 10;
        GameObject.Find("Sound Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.soundVal;
    }

    public void SoundIncrease()
    {
        if (settings.soundVal < 100)
            settings.soundVal += 10;
        GameObject.Find("Sound Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.soundVal;
    }

    public void SensitivityDecrease()
    {
        if (settings.sensitivity > 0)
            settings.sensitivity -= 10;
        GameObject.Find("Sensitivity Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.sensitivity;
    }

    public void SensitivityIncrease()
    {
        if (settings.sensitivity < 100)
            settings.sensitivity += 10;
        GameObject.Find("Sensitivity Value").GetComponent<TextMeshProUGUI>().text = "%" + settings.sensitivity;
    }

    public void DifficultyNext()
    {
        settings.difficultyVal = settings.difficultyVal < difficultySteps.Length - 1 ? settings.difficultyVal + 1 : 0;
        GameObject.Find("Difficulty Value").GetComponent<TextMeshProUGUI>().text = difficultySteps[settings.difficultyVal];
    }

    public void QualityNext()
    {
        settings.qualityVal = settings.qualityVal < qualitySteps.Length - 1 ? settings.qualityVal + 1 : 0;
        GameObject.Find("Quality Value").GetComponent<TextMeshProUGUI>().text = qualitySteps[settings.qualityVal];
    }
}

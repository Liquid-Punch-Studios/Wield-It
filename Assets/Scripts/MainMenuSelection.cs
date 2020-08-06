﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

public class MainMenuSelection : MonoBehaviour
{

    public Camera cam;
    public Rigidbody carrier;
    public GameObject credits;

    public Material wood;
    public Material darkWood;

    public float scrollMax;
    public float scrollMin;
    public float scrollTop;

    private Vector3 velocity;

    private bool settingsOn = false;
    private bool creditsOn = false;
    private float divider = 1000;
    private Controls cont;
    private Animator cameraAnim;
    private float scrollPosition;

    public AudioPlayer musics;
    private AudioPlayer woodSounds;
    private AudioPlayer chainSounds;
    

    private void Start()
    {
        Load();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        woodSounds = GameObject.Find("WoodImpact").GetComponent<AudioPlayer>();
        chainSounds = GameObject.Find("ChainImpact").GetComponent<AudioPlayer>();
        scrollPosition = scrollTop;
        cameraAnim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (creditsOn && (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame))
        {
            creditsOn = false;
            credits.GetComponent<Animator>().SetBool("isSet", false);
            credits.SetActive(false);
        }

        if (cont.UI.MouseClick.triggered)
        {
            RaycastHit hit;
            Vector2 vec = cont.UI.Mouse.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(vec);
            if (Physics.Raycast(ray, out hit) && !creditsOn)
            {
                string objectName = hit.collider.gameObject.name;
                //Debug.Log(objectName);
                TextMeshPro textComponent;
                var settings = SettingsManager.Instance.Settings;
                switch (objectName)
                {
                    case "Play":
                        cameraAnim.SetBool("isAtLevels", true);
                        break;

                    case string a when a.Contains("Demo"):
                        int clickedLevel;
                        Match m = Regex.Match(objectName, @"\d+");
                        string levelNumber = m.Success ? m.Value : "1";
                        if (Int32.TryParse(levelNumber,out clickedLevel))
                        {
                            if (clickedLevel <= Level.lastLevel)
                                StartCoroutine(PlayButtonClick(objectName));
                        }
                       
                        break;

                    case "Return":
                        cameraAnim.SetBool("isAtLevels", false);
                        break;

                    case "Settings":
                        settingsOn = !settingsOn;
                        scrollPosition = settingsOn ? scrollMax : scrollTop;
                        break;

                    case "Credits":
                        settingsOn = false;
                        scrollPosition = scrollTop;
                        creditsOn = true;
                        credits.SetActive(true);
                        credits.GetComponent<Animator>().SetBool("isSet", true);
                        break;

                    case "Exit":
                        StartCoroutine(ExitButtonClick());
                        break;
                        /*
                                            case "Violence":
                                                textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                                                SettingsManager.Instance.Settings.Violence = !SettingsManager.Instance.Settings.Violence;

                                                textComponent.text = Violence ? "ON" : "OFF";
                                                break;*/

                    case "Quality":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        settings.GraphicsQuality = (settings.GraphicsQuality + 1) % QualitySettings.names.Length;
                        textComponent.text = QualitySettings.names[settings.GraphicsQuality];
                        break;

                    case "Difficulty":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        settings.Difficulty = (Difficulty)((int)(settings.Difficulty + 1) % Enum.GetNames(typeof(Difficulty)).Length);
                        textComponent.text = settings.Difficulty.ToString();
                        break;

                    case "Music":
                        settings.MusicMuted = !settings.MusicMuted;
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        textComponent.text = settings.MusicMuted ? "Muted" : "%" + Mathf.Round(settings.MusicVolume * 100);
                        break;

                    case "MusicPlus":
                        settings.MusicMuted = false;
                        settings.MusicVolume = settings.MusicVolume < 1 ? settings.MusicVolume + 0.1f : 1;
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.MusicVolume * 100);
                        break;

                    case "MusicMinus":
                        settings.MusicMuted = false;
                        settings.MusicVolume = settings.MusicVolume > 0 ? settings.MusicVolume - 0.1f : 0;
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.MusicVolume * 100);
                        break;

                    case "Sound":
                        settings.EffectsMuted = !settings.EffectsMuted;
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        textComponent.text = settings.EffectsMuted ? "Muted" : "%" + Mathf.Round(settings.EffectsVolume * 100);
                        break;

                    case "SoundPlus":
                        settings.EffectsMuted = false;
                        settings.EffectsVolume = settings.EffectsVolume < 1 ? settings.EffectsVolume + 0.1f : 1;
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.EffectsVolume * 100);
                        break;

                    case "SoundMinus":
                        settings.EffectsMuted = false;
                        settings.EffectsVolume = settings.EffectsVolume > 0 ? settings.EffectsVolume - 0.1f : 0;
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.EffectsVolume * 100);
                        break;

                    case "SensitivityPlus":
                        settings.Sensitivity = settings.Sensitivity < 1 ? settings.Sensitivity + 0.1f : 1;
                        textComponent = GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>();
                        textComponent.text = settings.Sensitivity.ToString("F2", CultureInfo.InvariantCulture);
                        break;

                    case "SensitivityMinus":
                        settings.Sensitivity = settings.Sensitivity > 0 ? settings.Sensitivity - 0.1f : 0;
                        textComponent = GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>();
                        textComponent.text = settings.Sensitivity.ToString("F2", CultureInfo.InvariantCulture);
                        break;
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(ray.direction * 400);
                    if (hit.transform.tag == "Wood")
                        woodSounds.PlayRandom();
                    else if (hit.transform.tag == "Chain")
                        chainSounds.PlayRandom();
                }

                //if (settingsOn)
                //    SaveSystem.SaveSettings(this);
            }
        }


        
        Vector2 scrollMovement = cont.UI.Scroll.ReadValue<Vector2>();
        Vector3.SmoothDamp(carrier.transform.position, new Vector3(carrier.transform.position.x, scrollPosition, carrier.transform.position.z)
            , ref velocity, .5f, .25f, Time.fixedDeltaTime);
        carrier.transform.position += velocity;

        if (settingsOn)
        {
            scrollPosition += scrollMovement.y / divider;
            scrollPosition = scrollPosition < scrollMin ? scrollMin : scrollPosition;
            scrollPosition = scrollPosition > scrollMax ? scrollMax : scrollPosition;
        }
    }

    private void UpdateMusicVolume(int volume)
    {
        foreach(AudioSource audio in musics.audioList)
        {
            audio.volume = volume / 100.0f;
        }
    }

    private void UpdateSoundVolume(int volume)
    {
        foreach (AudioSource audio in chainSounds.audioList)
        {
            audio.volume = volume / 100.0f;
        }

        foreach (AudioSource audio in woodSounds.audioList)
        {
            audio.volume = volume / 100.0f;
        }
    }


    private void OnEnable()
    {
        if (cont == null)
            cont = new Controls();
        cont.Enable();
    }

    private void OnDisable()
    {
        cont.Disable();
    }

    IEnumerator PlayButtonClick(string objName)
    {
        yield return new WaitForSeconds(1);
        Debug.Log(objName);
        SceneManager.LoadSceneAsync(objName, LoadSceneMode.Single);
        yield return null;
    }
    
    IEnumerator ExitButtonClick()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
        yield return null;
    }

    public void Load()
    {
        var settings = SettingsManager.Instance.Settings;

        GameObject.Find("DifficultyVal").GetComponent<TextMeshPro>().text = settings.Difficulty.ToString();
        GameObject.Find("QualityVal").GetComponent<TextMeshPro>().text = QualitySettings.names[settings.GraphicsQuality];

        GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text  = "%" + Mathf.Round(settings.MusicVolume * 100);
        if (settings.MusicMuted)
            GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text = "Muted";

        GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "%" + Mathf.Round(settings.EffectsVolume * 100);
        if (settings.EffectsMuted)
            GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "Muted";

        GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>().text = settings.Sensitivity.ToString("F2", CultureInfo.InvariantCulture);

    }

}

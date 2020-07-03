﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuSelection : MonoBehaviour
{
    public Camera cam;
    public Rigidbody carrier;

    public float scrollMax;
    public float scrollMin;
    public float scrollTop;

    private int quality;
    public int Quality { get { return quality; } set { quality = value; }}

    private int difficulty;
    public int Difficulty { get { return difficulty; } set { difficulty = value; } }

    private bool musicMuted;
    public bool MusicMuted { get { return musicMuted; } set { musicMuted = value; } }

    private int previousMusicVal;
    private int musicVal;
    public int MusicVal { get { return musicVal; } set { musicVal = value; } }

    public int previousSoundVal;
    private bool soundMuted;
    public bool SoundMuted { get { return soundMuted; } set { soundMuted = value; } }

    private int soundVal;
    public int SoundVal { get { return soundVal; } set { soundVal = value; } }

    private bool violence;
    public bool Violence { get { return violence; } set { violence = value; } }


    private Vector3 velocity;

    private bool settingsOn = false;
    private float divider = 1500;
    private Controls cont;
    private float scrollPosition;

    

    private string[] qualitySteps = { "LOW", "MEDIUM", "HIGH" };
    private string[] difficultySteps = { "EASY", "MEDIUM", "HARD" };
    private void Start()
    {
        scrollPosition = scrollTop;
        Load();
    }

    void FixedUpdate()
    {
        if (cont.UI.MouseClick.triggered)
        {
            RaycastHit hit;
            Vector2 vec = cont.UI.Mouse.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(vec);

            if (Physics.Raycast(ray, out hit))
            {
                string objectName = hit.collider.gameObject.name;
                //Debug.Log(objectName);
                TextMeshPro textComponent;
                switch (objectName)
                {
                    case "Play":
                        StartCoroutine(PlayButtonClick());
                        break;
                    
                    case "Settings":
                        settingsOn = !settingsOn;
                        scrollPosition = settingsOn ? scrollMax : scrollTop;

                        break;

                    case "Exit":
                        StartCoroutine(ExitButtonClick());
                        break;

                    case "Violence":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        violence = !violence;

                        textComponent.text = Violence ? "ON" : "OFF";
                        break;

                    case "Quality":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        quality = quality < qualitySteps.Length -1 ? quality + 1 : 0;

                        textComponent.text = qualitySteps[Quality];
                        break;

                    case "Difficulty":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        difficulty = difficulty < difficultySteps.Length - 1 ? difficulty + 1 : 0;

                        textComponent.text = difficultySteps[Difficulty];
                        break;

                    case "Music":
                        MusicMuted = !MusicMuted;
                        if (MusicMuted)
                        {
                            previousMusicVal = MusicVal;
                            MusicVal = 0;
                        }
                        else
                            MusicVal = previousSoundVal;
                        MusicVal = MusicMuted ? 0 : previousMusicVal;
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        textComponent.text = MusicMuted ? "Muted" : "%" + musicVal;

                        break;

                    case "MusicPlus":
                        MusicMuted = false;
                        MusicVal = previousMusicVal;
                        MusicVal = MusicVal < 100 ? (MusicMuted ? previousMusicVal + 10 : MusicVal + 10) : 100;
                        previousMusicVal = MusicVal;
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + MusicVal;
                        break;

                    case "MusicMinus":
                        MusicMuted = false;
                        MusicVal = previousMusicVal;
                        MusicVal = MusicVal > 0 ? (MusicMuted ? previousMusicVal - 10 : MusicVal - 10) : 0;
                        previousMusicVal = MusicVal;
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + MusicVal;
                        break;

                    case "Sound":
                        SoundMuted = !SoundMuted;
                        if (SoundMuted)
                        {
                            previousSoundVal = SoundVal;
                            SoundVal = 0;
                        }
                        else
                            SoundVal = previousSoundVal;
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        textComponent.text = SoundMuted ? "Muted" : "%" + SoundVal;
                        Debug.Log(previousSoundVal);
                        break;

                    case "SoundPlus":
                        SoundVal = previousSoundVal;
                        SoundMuted = false;
                        SoundVal = SoundVal < 100 ? (SoundMuted ? previousSoundVal + 10 : SoundVal + 10) : 100;
                        previousSoundVal = SoundVal;
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + SoundVal;
                        break;

                    case "SoundMinus":
                        SoundMuted = false;
                        SoundVal = previousSoundVal;
                        SoundVal = SoundVal > 0 ? (SoundMuted ? previousSoundVal - 10 : SoundVal - 10) : 0;
                        previousSoundVal = SoundVal;
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + SoundVal;
                        break;
                }

                
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(ray.direction * 200);
                }

                if (settingsOn)
                    SaveSystem.SaveSettings(this);
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

    IEnumerator PlayButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);
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
        SettingsData data = SaveSystem.LoadSettings();
        Difficulty = data.difficultyVal;
        GameObject.Find("DifficultyVal").GetComponent<TextMeshPro>().text = difficultySteps[Difficulty];
        Quality = data.qualityVal;
        GameObject.Find("QualityVal").GetComponent<TextMeshPro>().text = qualitySteps[Quality];

        MusicVal = data.musicVal;
        previousMusicVal = data.musicVal;
        GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text = "%"+ MusicVal;
        MusicMuted = data.musicMuted;
        if (MusicMuted)
            GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text = "Muted";

        SoundVal = data.soundVal;
        previousSoundVal = data.soundVal;
        GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "%" + SoundVal;
        SoundMuted = data.soundMuted;
        if (SoundMuted)
            GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "Muted";

        Violence = data.violence;
        GameObject.Find("ViolenceVal").GetComponent<TextMeshPro>().text = Violence ? "ON" : "OFF";
    }
}
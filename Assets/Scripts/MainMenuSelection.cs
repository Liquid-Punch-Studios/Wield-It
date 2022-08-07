using System;
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
    private Controls controls;
    private Animator cameraAnim;
    private float scrollPosition;
    private bool prevSettings = false;
    private bool isLevelClicked = false;

    public AudioPlayer musics;
    private AudioPlayer woodSounds;
    private AudioPlayer chainSounds;
    public AudioSource plusSound;
    public AudioSource minusSound;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Load();
        controls = GameManager.Instance.controls;
        Cursor.visible = true;
        woodSounds = GameObject.Find("WoodImpact").GetComponent<AudioPlayer>();
        chainSounds = GameObject.Find("ChainImpact").GetComponent<AudioPlayer>();
        scrollPosition = scrollTop;
        cameraAnim = gameObject.GetComponent<Animator>();
    }

    float AddPercent(float value, int percent) => (Mathf.RoundToInt(value * 100) + percent) / 100f;
    int GetPercent(float value) => Mathf.RoundToInt(value * 100);

    void FixedUpdate()
    {

        if (!creditsOn && controls.UI.MouseClick.triggered)
        {
            RaycastHit hit;
            Vector2 vec = controls.UI.Mouse.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(vec);
            if (Physics.Raycast(ray, out hit))
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
                        Match m = Regex.Match(objectName, @"\d+");
                        string levelNumber = m.Success ? m.Value : "1";
                        //if (Int32.TryParse(levelNumber,out clickedLevel))
                        //{
                            StartCoroutine(PlayButtonClick(objectName));
                        //}
                       
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

                    case "DisplayMode":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        //settings.DisplayMode = Screen.fullScreen ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
                        Screen.fullScreen = !Screen.fullScreen;
                        textComponent.text = Screen.fullScreen ? 
                            settings.displayModes[settings.Language][1] : settings.displayModes[settings.Language][0];

                        break;

                    case "Quality":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        settings.GraphicsQuality = (settings.GraphicsQuality + 1) % QualitySettings.names.Length;
                        textComponent.text = settings.qualities[settings.Language][QualitySettings.GetQualityLevel()];
                        break;

                    case "Difficulty":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        settings.Difficulty = (Difficulty)((int)(settings.Difficulty + 1) % Enum.GetNames(typeof(Difficulty)).Length);
                        textComponent.text = settings.difficulties[settings.Language][(int)settings.Difficulty];
                        break;

                    case "Music":
                        settings.MusicMuted = !settings.MusicMuted;
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        textComponent.text = settings.MusicMuted ? "Muted" : "%" + Mathf.Round(settings.MusicVolume * 100);
                        break;

                    case "MusicPlus":
                        settings.MusicMuted = false;
                        settings.MusicVolume = AddPercent(settings.MusicVolume, 10);
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.MusicVolume * 100);
                        plusSound.PlayOneShot(plusSound.clip);
                        break;

                    case "MusicMinus":
                        settings.MusicMuted = false;
                        settings.MusicVolume = AddPercent(settings.MusicVolume, -10);
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.MusicVolume * 100);
                        minusSound.PlayOneShot(minusSound.clip);
                        break;

                    case "Sound":
                        settings.EffectsMuted = !settings.EffectsMuted;
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        textComponent.text = settings.EffectsMuted ? "Muted" : "%" + Mathf.Round(settings.EffectsVolume * 100);
                        break;

                    case "SoundPlus":
                        settings.EffectsMuted = false;
                        settings.EffectsVolume = AddPercent(settings.EffectsVolume, 10);
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.EffectsVolume * 100);
                        plusSound.PlayOneShot(plusSound.clip);
                        break;

                    case "SoundMinus":
                        settings.EffectsMuted = false;
                        settings.EffectsVolume = AddPercent(settings.EffectsVolume, -10);
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Mathf.Round(settings.EffectsVolume * 100);
                        minusSound.PlayOneShot(minusSound.clip);
                        break;

                    case "SensitivityPlus":
                        settings.Sensitivity = AddPercent(settings.Sensitivity, 5);
                        textComponent = GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>();
                        textComponent.text = settings.Sensitivity.ToString("F2", CultureInfo.InvariantCulture);
                        plusSound.PlayOneShot(plusSound.clip);
                        break;

                    case "SensitivityMinus":
                        settings.Sensitivity = AddPercent(settings.Sensitivity, -5);
                        textComponent = GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>();
                        textComponent.text = settings.Sensitivity.ToString("F2", CultureInfo.InvariantCulture);
                        minusSound.PlayOneShot(minusSound.clip);
                        break;

                    case "Language":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        settings.Language = (Language)((int)(settings.Language + 1) % Enum.GetNames(typeof(Language)).Length);
                        textComponent.text = settings.languages[settings.Language];
                        GameObject.Find("DifficultyVal").GetComponent<TMP_Text>().text = 
                            settings.difficulties[settings.Language][(int)settings.Difficulty];
                        GameObject.Find("QualityVal").GetComponent<TMP_Text>().text = 
                            settings.qualities[settings.Language][QualitySettings.GetQualityLevel()];
                        GameObject.Find("DisplayModeVal").GetComponent<TMP_Text>().text =
                            Screen.fullScreen ? settings.displayModes[settings.Language][1] : settings.displayModes[settings.Language][0];
                        break;
                    default:
                        break;
                }

                if (hit.rigidbody != null)
                {
                    hit.collider.attachedRigidbody.AddForce(ray.direction * 400);
                    if (hit.collider.transform.CompareTag("Wood"))
                        woodSounds.PlayRandom(0.1f);
                    else if (hit.collider.transform.CompareTag("Chain"))
                        chainSounds.PlayRandom(0.1f);
                }
            }
        }

        else if (creditsOn && (Keyboard.current.anyKey.wasPressedThisFrame || controls.UI.MouseClick.triggered))
        {
            creditsOn = false;
            credits.GetComponent<Animator>().SetBool("isSet", false);
            credits.SetActive(false);
        }


        Vector2 scrollMovement = controls.UI.Scroll.ReadValue<Vector2>();
        Vector3.SmoothDamp(carrier.transform.position, new Vector3(carrier.transform.position.x, scrollPosition, carrier.transform.position.z)
            , ref velocity, .5f, .25f, Time.fixedDeltaTime);
        carrier.transform.position += velocity;


        var audio = carrier.GetComponent<AudioPlayer>();

        if (settingsOn != prevSettings)
        {
            audio.PlayRandom(0.1f);
            prevSettings = settingsOn;
        }

        if (settingsOn)
        {
            if (scrollMovement.y != 0 && scrollPosition != scrollMax && scrollPosition != scrollMin && !audio.currentAudio.isPlaying)
                audio.PlayRandom();
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


    IEnumerator PlayButtonClick(string objName)
    {
        yield return new WaitForSeconds(1);
        Debug.Log(objName);
        if (!isLevelClicked)
        {
            SceneManager.LoadSceneAsync(objName, LoadSceneMode.Single);
            isLevelClicked = true;
        }
        
        yield return null;
    }
    
    IEnumerator ExitButtonClick()
    {
        yield return new WaitForSeconds(1);
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
              Application.Quit();
        #endif
        //Application.Quit();
        yield return null;
    }

    public void Load()
    {
        var settings = SettingsManager.Instance.Settings;

        GameObject.Find("DifficultyVal").GetComponent<TextMeshPro>().text = 
            settings.difficulties[settings.Language][(int)settings.Difficulty];

        GameObject.Find("QualityVal").GetComponent<TextMeshPro>().text = 
            settings.qualities[settings.Language][QualitySettings.GetQualityLevel()];

        GameObject.Find("DisplayModeVal").GetComponent<TextMeshPro>().text = 
            Screen.fullScreen ? settings.displayModes[settings.Language][0] : settings.displayModes[settings.Language][1];

        GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text  = "%" + Mathf.Round(settings.MusicVolume * 100);
        if (settings.MusicMuted)
            GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text = "Muted";

        GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "%" + Mathf.Round(settings.EffectsVolume * 100);
        if (settings.EffectsMuted)
            GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "Muted";

        GameObject.Find("LanguageVal").GetComponent<TextMeshPro>().text = settings.languages[settings.Language];
        GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>().text = 
            settings.Sensitivity.ToString("F2", CultureInfo.InvariantCulture);
    }

}

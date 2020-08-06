using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;

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

    private int quality;
    public int Quality { get { return quality; } set { quality = value; } }

    private int difficulty;
    public int Difficulty { get { return difficulty; } set { difficulty = value; } }

    private bool musicMuted;
    public bool MusicMuted { get { return musicMuted; } set { musicMuted = value; } }

    private int previousMusicVal;
    private int musicVal;
    public int MusicVal { get { return musicVal; } set { musicVal = value; } }

    private int previousSoundVal;
    private bool soundMuted;
    public bool SoundMuted { get { return soundMuted; } set { soundMuted = value; } }

    private int soundVal;
    public int SoundVal { get { return soundVal; } set { soundVal = value; } }

    private bool violence;
    public bool Violence { get { return violence; } set { violence = value; } }

    private int sensitivity;
    public int Sensitivity { get { return sensitivity; } set { sensitivity = value; } }


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

    private string[] qualitySteps = { "LOW", "MEDIUM", "HIGH" };

    private string[] difficultySteps = { "EASY", "MEDIUM", "HARD" };

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        woodSounds = GameObject.Find("WoodImpact").GetComponent<AudioPlayer>();
        chainSounds = GameObject.Find("ChainImpact").GetComponent<AudioPlayer>();
        scrollPosition = scrollTop;
        cameraAnim = gameObject.GetComponent<Animator>();
        Load();
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
                        UpdateMusicVolume(MusicVal);
                        break;

                    case "MusicPlus":
                        MusicMuted = false;
                        MusicVal = previousMusicVal;
                        MusicVal = MusicVal < 100 ? (MusicMuted ? previousMusicVal + 10 : MusicVal + 10) : 100;
                        previousMusicVal = MusicVal;
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + MusicVal;
                        UpdateMusicVolume(musicVal);
                        break;

                    case "MusicMinus":
                        MusicMuted = false;
                        MusicVal = previousMusicVal;
                        MusicVal = MusicVal > 0 ? (MusicMuted ? previousMusicVal - 10 : MusicVal - 10) : 0;
                        previousMusicVal = MusicVal;
                        textComponent = GameObject.Find("MusicVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + MusicVal;
                        UpdateMusicVolume(musicVal);
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
                        UpdateSoundVolume(SoundVal);
                        break;

                    case "SoundPlus":
                        SoundVal = previousSoundVal;
                        SoundMuted = false;
                        SoundVal = SoundVal < 100 ? (SoundMuted ? previousSoundVal + 10 : SoundVal + 10) : 100;
                        previousSoundVal = SoundVal;
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + SoundVal;
                        UpdateSoundVolume(SoundVal);
                        break;

                    case "SoundMinus":
                        SoundMuted = false;
                        SoundVal = previousSoundVal;
                        SoundVal = SoundVal > 0 ? (SoundMuted ? previousSoundVal - 10 : SoundVal - 10) : 0;
                        previousSoundVal = SoundVal;
                        textComponent = GameObject.Find("SoundVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + SoundVal;
                        UpdateSoundVolume(SoundVal);
                        break;

                    case "SensitivityPlus":
                        Sensitivity = sensitivity < 100 ? sensitivity + 10 : 100;
                        textComponent = GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Sensitivity;
                        break;

                    case "SensitivityMinus":
                        Sensitivity = sensitivity > 0 ? sensitivity - 10 : 0;
                        textComponent = GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>();
                        textComponent.text = "%" + Sensitivity;
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
        //if (File.Exists(SaveSystem.levelPath))
        //{
        //    SaveSystem.LoadLastLevel();
        //}
        //else
        //{
        //    SaveSystem.SaveLastLevel();
        //}

        //if (File.Exists(SaveSystem.settingsPath))
        //{
        //    GameSettings data = SaveSystem.LoadSettings();
        //    Difficulty = data.difficultyVal;
        //    Quality = data.qualityVal;
        //    MusicVal = data.musicVal;
        //    previousMusicVal = data.musicVal;
        //    MusicMuted = data.musicMuted;
        //    SoundVal = data.soundVal;
        //    previousSoundVal = data.soundVal;
        //    SoundMuted = data.effectsMuted;
        //    Violence = data.violence;
        //    Sensitivity = data.sensitivity;
        //}
        //else
        //{
        //    Difficulty = 1;
        //    Quality = 1;
        //    MusicVal = 100;
        //    previousMusicVal = MusicVal;
        //    MusicMuted = false;
        //    SoundVal = 100;
        //    previousSoundVal = SoundVal;
        //    SoundMuted = false;
        //    Violence = false;
        //    Sensitivity = 50;
        //    Level.lastLevel = 1;
        //    SaveSystem.SaveSettings(this);
        //}
        //GameObject.Find("DifficultyVal").GetComponent<TextMeshPro>().text = difficultySteps[Difficulty];
        //GameObject.Find("QualityVal").GetComponent<TextMeshPro>().text = qualitySteps[Quality];

        //GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text = "%" + MusicVal;
        //if (MusicMuted)
        //    GameObject.Find("MusicVal").GetComponent<TextMeshPro>().text = "Muted";
        //UpdateMusicVolume(MusicVal);

        //GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "%" + SoundVal;
        //if (SoundMuted)
        //    GameObject.Find("SoundVal").GetComponent<TextMeshPro>().text = "Muted";
        //UpdateSoundVolume(SoundVal);

        //GameObject.Find("ViolenceVal").GetComponent<TextMeshPro>().text = Violence ? "ON" : "OFF";
        //GameObject.Find("SensitivityVal").GetComponent<TextMeshPro>().text = "%" + Sensitivity;


        //GameObject[] levelLogs = new GameObject[Level.levelCount];

        //for (int i = 0; i< Level.levelCount; i++)
        //{
        //    levelLogs[i] = GameObject.Find("Demo" + (i+1).ToString());
        //    TextMeshPro text = levelLogs[i].GetComponentInChildren<TextMeshPro>();
        //    if (i <= Level.lastLevel - 1)
        //    {
        //        levelLogs[i].GetComponent<MeshRenderer>().material = wood;
        //        text.color = new Color(1, 1, 1);
        //    }

        //    else
        //    {
        //        levelLogs[i].GetComponent<MeshRenderer>().material = darkWood;
        //        text.color = new Color(0.35f, 0.35f, 0.35f);
        //    }
                
        //}
    }

}

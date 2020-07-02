using System;
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

    private Vector3 velocity;

    private bool settingsOn = false;
    private bool violence = false;
    private float divider = 1500;
    private Controls cont;
    private float scrollPosition;

    private int quality;
    private int difficulty;

    private string[] qualitySteps = { "LOW", "MEDIUM", "HIGH" };
    private string[] difficultySteps = { "EASY", "MEDIUM", "HARD" };
    private void Start()
    {
        scrollPosition = scrollTop;
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

                        textComponent.text = violence ? "ON" : "OFF";
                        break;

                    case "Quality":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        quality = quality < qualitySteps.Length -1 ? quality + 1 : 0;

                        textComponent.text = qualitySteps[quality];
                        break;

                    case "Difficulty":
                        textComponent = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        difficulty = difficulty < difficultySteps.Length - 1 ? difficulty + 1 : 0;

                        textComponent.text = difficultySteps[difficulty];
                        break;
                }

                
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(ray.direction * 200);
                }
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
}

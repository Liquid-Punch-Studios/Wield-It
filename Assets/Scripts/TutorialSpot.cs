using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialSpot : MonoBehaviour
{
    public LayerMask triggerLayerMask;
    public GameObject button;
    public GameObject tutorial;
    public Sprite keyboardButton;
    public Sprite psButton;

    private bool playerInCollider;
    private Controls controls;
    
    private Animator buttonAnim;
    private Animator tutorialAnim;

    private Gamepad gamepad;
    private Keyboard keyboard;
    private Mouse mouse;

    private void Start()
    {
        controls = GameManager.Instance.controls;
        buttonAnim = button.GetComponent<Animator>();
        gamepad = Gamepad.current;
        keyboard = Keyboard.current;
        mouse = Mouse.current;
        //tutorialAnim = tutorial.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (gamepad != null && gamepad.wasUpdatedThisFrame)
        {
            button.GetComponent<Image>().sprite = psButton;
        }

        if (keyboard.wasUpdatedThisFrame || mouse.wasUpdatedThisFrame)
        {
            button.GetComponent<Image>().sprite = keyboardButton;
        }

        if (playerInCollider)
        {
            if (controls.Player.Interaction.triggered)
            {
                
                button.SetActive(false);
                tutorial.SetActive(true);

                //tutorialAnim.SetBool("isSet", true);
            }
        }
        else
        {
            tutorial.SetActive(false);
            //tutorialAnim.SetBool("isSet", false);
            //StartCoroutine(DelayedRemove());
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
        {
            button.SetActive(true);
            playerInCollider = true;
            buttonAnim.SetBool("isSet", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
        {
            button.SetActive(false);
            playerInCollider = false;
            buttonAnim.SetBool("isSet", false);
        }
    }

    IEnumerator DelayedRemove()
    {
        yield return new WaitForSeconds(1);
        tutorial.SetActive(false);
        yield return null;
    }
}


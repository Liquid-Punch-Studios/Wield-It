using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSpot : MonoBehaviour
{
    public LayerMask triggerLayerMask;
    public GameObject button;
    public GameObject tutorial;

    private bool playerInCollider;
    private Controls controls;
    
    private Animator buttonAnim;
    private Animator tutorialAnim;

    private void Start()
    {
        buttonAnim = button.GetComponent<Animator>();
        //tutorialAnim = tutorial.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
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
}


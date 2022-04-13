using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public LayerMask triggerLayerMask;
    public GameObject button;
    private bool playerInCollider;
    private Controls controls;

    private Animator buttonAnim;
    private Health health;
    Animator anim;
    AudioSource audioSrc;
    void Start()
    {
        anim = GetComponentInChildren(typeof(Animator)) as Animator;
        audioSrc = gameObject.GetComponent<AudioSource>();
        controls = GameManager.Instance.controls;
        buttonAnim = button.GetComponent<Animator>();
        health = gameObject.GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        if (playerInCollider)
        {
            if (controls.Player.Interaction.triggered)
            {
                button.SetActive(false);
                anim.SetBool("isSet", true);
                audioSrc.Play();
                health.Hp -= health.MaxHp;
            }
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
}

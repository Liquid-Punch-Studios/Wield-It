using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{

    public Health playerHealth;

    Animator anim;
    AudioSource audio;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        playerHealth.Died += PlayerHealth_Died;
    }

    private void PlayerHealth_Died(object sender, System.EventArgs e)
    {
        anim.SetBool("IsSet", true);
        audio.Play();
    }
}

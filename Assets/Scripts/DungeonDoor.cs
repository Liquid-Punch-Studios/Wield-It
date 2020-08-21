using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{

    public Health enemyHealth;

    Animator anim;
    AudioSource audioSrc;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        enemyHealth.Died += EnemyHealth_Died;
    }

    private void EnemyHealth_Died(object sender, System.EventArgs e)
    {
        anim.SetBool("isSet", true);
        audioSrc.Play();
    }
}

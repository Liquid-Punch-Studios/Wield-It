using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool hasPassed = false;
    Animator[] animators;
    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasPassed)
            if(other.transform == GameManager.Instance.player.transform)
            {
                foreach(Animator anim in animators)
                    anim.SetTrigger("isSet");
                GameManager.Instance.RespawnPoint = transform.Find("RespawnPoint");
                hasPassed = true;
            }
    }
}

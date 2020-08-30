using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool hasPassed = false;
    Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasPassed)
            if(other.transform == GameManager.Instance.player.transform)
            {
                animator.SetTrigger("isSet");
                GameManager.Instance.RespawnPoint = transform.Find("RespawnPoint");
                hasPassed = true;
            }
    }
}

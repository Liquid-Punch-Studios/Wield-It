using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool hasPassed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPassed)
            if(other.transform == GameManager.Instance.player.transform)
            {
                GameManager.Instance.RespawnPoint = transform.Find("RespawnPoint");
                hasPassed = true;
            }
    }
}

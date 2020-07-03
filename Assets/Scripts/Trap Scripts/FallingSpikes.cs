using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
        Debug.Log(other.gameObject.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (anim.GetBool("isSet"))
        {
            Rigidbody rb = gameObject.GetComponentInChildren<Rigidbody>();
            rb.isKinematic = false;
            anim.SetBool("isSet", false);
            StartCoroutine(Reset(rb));
        }
        
    }

    IEnumerator Reset(Rigidbody rb)
    {
        yield return new WaitForSeconds(3);
        //rb.isKinematic = true;
        //anim.SetBool("isSet",true);
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isSet", true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isSet", false);
    }
}

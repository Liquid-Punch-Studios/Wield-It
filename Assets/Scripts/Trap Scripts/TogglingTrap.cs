using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglingTrap : MonoBehaviour
{
    public float delay;
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        anim.enabled = false;
        StartCoroutine(DelayedAnimation());
    }

    IEnumerator DelayedAnimation()
    {
        yield return new WaitForSeconds(delay);
        anim.enabled = true;
    }
}

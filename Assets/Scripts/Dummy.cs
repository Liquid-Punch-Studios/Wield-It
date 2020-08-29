using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Health
{
	public Animator animator;

    private void OnEnable()
    {
        DamageReceived += Dummy_DamageReceived;
    }
    private void OnDisable()
    {
        DamageReceived -= Dummy_DamageReceived;
    }

    private void Dummy_DamageReceived(object sender, float e)
    {
        if (Random.Range(0f, 1f) < 0.5f)
            animator.SetTrigger("hit1");
        else
            animator.SetTrigger("hit2");
    }
}

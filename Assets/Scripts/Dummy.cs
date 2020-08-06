using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
	public Animator animator;

	private void OnTriggerEnter(Collider other)
	{
		if (Random.Range(0f, 1f) < 0.5f)
			animator.SetTrigger("hit1");
		else
			animator.SetTrigger("hit2");
	}
}

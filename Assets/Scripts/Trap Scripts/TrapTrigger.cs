using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	public Animator anim;

	public LayerMask triggerLayerMask;

	private void OnTriggerEnter(Collider other)
	{
		if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
			anim.SetBool("isSet", true);
	}

	private void OnTriggerExit(Collider other)
	{
		if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
			anim.SetBool("isSet", false);
	}
}

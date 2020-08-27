using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	public Animator anim;
	public LayerMask triggerLayerMask;
	private Health health;

    private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out health))
            health.Died += Health_Died;
		if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
			anim.SetBool("isSet", true);
	}

    private void Health_Died(object sender, System.EventArgs e)
    {
		anim.SetBool("isSet", false);
	}

    private void OnTriggerExit(Collider other)
	{
		if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
			anim.SetBool("isSet", false);
	}
}

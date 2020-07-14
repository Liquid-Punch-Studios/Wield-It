using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamBreak : MonoBehaviour
{
	public LayerMask triggerLayerMask;

	private void OnTriggerEnter(Collider other)
	{
		if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
		{
			Debug.Log(other.name);
			if (other.gameObject.TryGetComponent<Movement>(out Movement movement) && movement.Slamming)
			{
				transform.Find("Original Door").gameObject.SetActive(false);
				Vector3 exp = transform.Find("Explosion").transform.position;
				GameObject go = transform.Find("Broken Door").gameObject;
				go.SetActive(true);
				foreach (Transform child in go.transform)
				{
					child.GetComponent<Rigidbody>().AddExplosionForce(1000, exp, 100);
				}
			}
		}
	}
}

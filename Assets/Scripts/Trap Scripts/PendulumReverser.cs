using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumReverser : MonoBehaviour
{
	public Transform obj;

	public void Reverse()
	{
		var rot = obj.localRotation.eulerAngles;
		var qua = Quaternion.Euler(rot.x, rot.y + 180, rot.z);
		obj.localRotation = qua;
	}
}

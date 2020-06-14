using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(RelativeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Weapon : MonoBehaviour
{
	public Vector2 weaponHandle;
	public float Damage;
	public float speedTreshold;

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Vector3 handle = this.transform.TransformPoint(new Vector3(weaponHandle.x, weaponHandle.y));
		Gizmos.DrawSphere(handle, 0.1f);
	}
}

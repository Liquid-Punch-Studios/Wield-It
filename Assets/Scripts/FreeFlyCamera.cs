using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFlyCamera : MonoBehaviour
{
	public float moveSpeed = 0.5f;
	public float smoothing = 0.25f;
	public float sensitivity = 0.25f;

	private Controls controls;
	private Vector3 velocity;
	private Vector3 angularVelocity;

    private void Start()
    {
		controls = GameManager.Instance.controls;
    }

    private void FixedUpdate()
	{
		if (controls.Spectator.Turn.ReadValue<float>() > 0f)
		{
			Vector2 delta = controls.Spectator.Look.ReadValue<Vector2>() * sensitivity;
			transform.Rotate(new Vector3(0, delta.x), Space.World);
			transform.Rotate(new Vector3(-delta.y, 0), Space.Self);
		}
		
		Vector3 move = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0) * controls.Spectator.Move.ReadValue<Vector2>();
		move += Vector3.up * controls.Spectator.Fly.ReadValue<float>();
		Vector3.SmoothDamp(transform.position, transform.position + move, ref velocity, smoothing, moveSpeed, Time.fixedDeltaTime);
		transform.position += velocity;
	}
}

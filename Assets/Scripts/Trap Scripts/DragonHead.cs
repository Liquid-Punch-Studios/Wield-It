using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour
{
	public ParticleSystem dragonBreath;

	public HazardTrigger trigger;

	public float playTime;
	public float pauseTime;

	private float playTimer;
	private float pauseTimer;

	private bool playing = false;

	private void Start()
	{
		pauseTimer = pauseTime;
		playTimer = playTime;
	}

	void FixedUpdate()
	{
		float timePassed = Time.fixedDeltaTime;

		if (playing)
		{
			playTimer -= timePassed;
			if (playTimer <= 0.0f)
			{
				playing = false;
				playTimer = playTime;
				if (dragonBreath.isPlaying)
					dragonBreath.Stop(true);
				trigger.Deactivate();
			}
		}
		else
		{
			pauseTimer -= timePassed;
			if (pauseTimer <= 0.0f)
			{
				playing = true;
				pauseTimer = pauseTime;
				if (!dragonBreath.isPlaying)
					dragonBreath.Play();
				trigger.Activate();
			}
		}
	}
}

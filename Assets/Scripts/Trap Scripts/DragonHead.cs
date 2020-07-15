using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour
{
	public ParticleSystem dragonBreath;

	public HazardTrigger trigger;

	public AudioPlayer audio;

	public float playTime;
	public float pauseTime;

	private float playTimer;
	private float pauseTimer;

	private bool playing = false;
	private bool isAudioPlayed = false;

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
			if(!isAudioPlayed)
				audio.PlayRandom();
			isAudioPlayed = true;
			playTimer -= timePassed;
			if (playTimer <= 0.0f)
			{
				playing = false;
				pauseTimer = pauseTime;
				if (dragonBreath.isPlaying)
					dragonBreath.Stop(true);
				trigger.Deactivate();
			}
		}
		else
		{
			isAudioPlayed = false;
			foreach(AudioSource a in audio.audioList)
				a.Stop();
			pauseTimer -= timePassed;
			if (pauseTimer <= 0.0f)
			{
				playing = true;
				playTimer = playTime;
				if (!dragonBreath.isPlaying)
					dragonBreath.Play();
				trigger.Activate();
			}
		}
	}
}

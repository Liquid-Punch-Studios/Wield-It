using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticleTrigger : MonoBehaviour
{
	public ParticleSystem particle;
	public AudioPlayer audioPlayer;

	public void Trigger()
	{
		particle.Play(true);
		if(audioPlayer != null)
			audioPlayer.PlayRandom(0.05f);
	}
}

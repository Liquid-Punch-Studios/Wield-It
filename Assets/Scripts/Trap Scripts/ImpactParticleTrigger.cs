using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ImpactParticleTrigger : MonoBehaviour
{
	public ParticleSystem particle;
	public AudioPlayer audioPlayer;
	public CinemachineImpulseSource impulse;

	public void Trigger()
	{
		particle.Play(true);
		audioPlayer?.PlayRandom(0.05f);
		impulse?.GenerateImpulse();
	}
}

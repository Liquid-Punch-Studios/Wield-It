using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 
/// </summary>
public class AudioPlayer : MonoBehaviour
{
	public AudioSource[] audioList;
	public bool playOnAwake = false;
	public event System.EventHandler<int> Triggered;
	public event System.EventHandler ClipEnded;
	[HideInInspector]
	public AudioSource currentAudio;

	private void Awake()
	{
		if (playOnAwake)
			PlayRandom();
	}

	public void PlayRandom(float pitchShift = 0)
	{
		int index = Random.Range(0, audioList.Length);
		currentAudio = audioList[index];
		currentAudio.pitch = Mathf.Clamp(Random.Range(1 - pitchShift, 1 + pitchShift), 0.1f, 10f);
		currentAudio.Play(); // TODO: Check if sound replays or plays a new one
		StartCoroutine(WaitUntilClipEnds());
		Triggered?.Invoke(this, index);

	}

	public void PlayOneShotRandom(float pitchShift = 0)
	{
		int index = Random.Range(0, audioList.Length);
		currentAudio = audioList[index];
		currentAudio.pitch = Mathf.Clamp(Random.Range(1 - pitchShift, 1 + pitchShift), 0.1f, 10f);
		currentAudio.PlayOneShot(currentAudio.clip);
		StartCoroutine(WaitUntilClipEnds());
		Triggered?.Invoke(this, index);
	}

	public void Play(int audioIndex)
	{
		if (audioIndex < audioList.Length)
		{
			audioList[audioIndex].Play(); // TODO: Check if sound replays or plays a new one
			StartCoroutine(WaitUntilClipEnds());
			Triggered?.Invoke(this, audioIndex);
		}
		else
			Debug.LogError("Invalid audio index: " + audioIndex);
	}
	
	public IEnumerator WaitUntilClipEnds()
    {
		Debug.Log(currentAudio.clip.name + " Playing / Length: " + currentAudio.clip.length);
		yield return new WaitForSeconds(currentAudio.clip.length);
		Debug.Log("Clip Ended");
		ClipEnded?.Invoke(this, null);
    }
}

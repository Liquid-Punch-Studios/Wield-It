using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour
{
	public static Persistence instance;
	AudioPlayer audioPlayer;
	private void Awake()
	{
		if (instance == null)
        {
			audioPlayer = GetComponent<AudioPlayer>();
			audioPlayer.ClipEnded += AudioPlayer_ClipEnded;
			instance = this;
		}
			
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

    private void AudioPlayer_ClipEnded(object sender, EventArgs e)
    {
		audioPlayer.PlayRandom();
    }
}
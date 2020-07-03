using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;


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

    void Update()
    {
        float now = Time.deltaTime;
        
        if (playing)
        {
            if(!fireParticleSystem.isPlaying)
                fireParticleSystem.Play(true);
            playTimer -= now;
            if (playTimer <= 0.0f)
            {
                playing = false;
                playTimer = playTime;
            }
        }
        else
        {
            if(fireParticleSystem.isPlaying)
                fireParticleSystem.Stop(true);
            pauseTimer -= now;
            if (pauseTimer <= 0.0f)
            {
                playing = true;
                pauseTimer = pauseTime;
            }
        }
    }
}

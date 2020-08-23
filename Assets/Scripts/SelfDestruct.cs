using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public bool isTimer;
    public float destructionTimer;
    private float timerStart;
    void Start()
    {
        timerStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timerStart > destructionTimer && isTimer)
            Destroy(gameObject);
    }

    void SelfDestruction()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStop : MonoBehaviour
{
    private void Awake()
    {
        if(Persistence.instance != null)
        {
            Destroy(gameObject);
        }
    }
}

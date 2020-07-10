using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
	Animator anim;

	public LayerMask triggerLayerMask;

    private void Start()
    {
		anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
	{
		int currentLevel = SceneManager.GetActiveScene().buildIndex;
		if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
        {
			anim.SetBool("isSet", true);
			Level.NextLevel(currentLevel);
			Debug.Log(currentLevel);
			StartCoroutine(Level.LoadLevelDelayed(1f, currentLevel + 1));
			SaveSystem.SaveLastLevel();
		}
	}

}

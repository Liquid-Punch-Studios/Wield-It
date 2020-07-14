using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
	public Rigidbody door;
	Animator anim;

	public LayerMask triggerLayerMask;

    private void Start()
    {
		anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
	{
		if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
        {
			anim.SetBool("isSet", true);
			GameObject.Find("Level Changer").GetComponentInChildren<LevelChanger>().LoadNextLevel();
			SaveSystem.SaveLastLevel();
			door.isKinematic = false;
		}
	}

}

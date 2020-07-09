using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class BarrelDispenser : MonoBehaviour
{
    public GameObject barrelPrefab;
    public float tick;
    public float prefabLasts;
    public float thrust;
    Animator anim;
    float last;
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
       if( Time.time - last >= tick)
       {
            anim.ResetTrigger("isSet");
            anim.SetTrigger("isSet");
            GameObject go = Instantiate(barrelPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            go.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -100 * thrust));
            StartCoroutine(KillAt(go, prefabLasts));
            last = Time.time;
       }
    }

    IEnumerator KillAt(GameObject obj, float killTime)
    {
        yield return new WaitForSeconds(killTime);
        Destroy(obj);
        yield return null;
    }
}

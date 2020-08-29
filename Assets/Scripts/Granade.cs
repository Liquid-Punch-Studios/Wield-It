using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public float timer;
    GameObject Explosion;
    
    void Start()
    {
        Explosion = Resources.Load<GameObject>("ExplosionEffect");
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(timer);
        var ex = Instantiate(Explosion);
        ex.transform.position = transform.position;
        Harm();
        Destroy(gameObject);
        yield return null;
    }

    public void Harm()
    {
        RaycastHit[] rcs = Physics.SphereCastAll(transform.position, 6, Vector3.up);

        foreach(RaycastHit rc in rcs)
        {
            var distanceToRaycast = (transform.position - rc.transform.position).magnitude;
            if (rc.transform.TryGetComponent(out SlamBreak sb) && distanceToRaycast < 3)
                sb.Break();
            if (rc.transform.TryGetComponent(out Health health))
            {
                Debug.Log(rc.distance);
                var damage = (100 / distanceToRaycast);
                if(rc.transform.gameObject != GameManager.Instance.player)
                    health.ReceiveDamage(damage);
            }

            if (rc.transform.TryGetComponent(out Rigidbody rb))
            {
                if (distanceToRaycast != 0)
                    //TODO: Change values to make perfect explosion :)
                    rb.AddExplosionForce(500 / distanceToRaycast, transform.position, 10, 0.5f, ForceMode.Impulse);
            }
                
                
        }
    }
}

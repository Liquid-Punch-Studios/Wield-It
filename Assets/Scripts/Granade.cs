using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public float timer;
    public float deadLine = 3;
    GameObject Explosion, damageIndicator;
    CinemachineImpulseSource impulse;
    void Start()
    {
        Explosion = Resources.Load<GameObject>("ExplosionEffect");
        damageIndicator = (GameObject)Resources.Load("DamageIndicator");
        impulse = GetComponent<CinemachineImpulseSource>();
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(timer);
        impulse.GenerateImpulse();
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
            if (rc.collider.attachedRigidbody == null)
                continue;
            var distanceToRaycast = (transform.position - rc.transform.position).magnitude;
            if (rc.transform.TryGetComponent(out SlamBreak sb) && distanceToRaycast < 3.5f)
            {
                sb.Break();
                continue;
            }

            var hasHealth = rc.collider.attachedRigidbody.transform.TryGetComponent(out Health health);
            var isPlayer = rc.transform == GameManager.Instance.player.transform;

            if (!isPlayer)
            {
                if (hasHealth && distanceToRaycast < deadLine)
                {
                    health.ReceiveDamage(300);
                    IndicateDamage(300, rc.collider.attachedRigidbody.transform.Find("DISpawn"));
                }
                else if (hasHealth)
                {
                    var damage = (150 / distanceToRaycast);
                    if (rc.transform.gameObject != GameManager.Instance.player)
                    {
                        health.ReceiveDamage(damage);
                        IndicateDamage(damage, rc.collider.attachedRigidbody.transform.Find("DISpawn"));
                    }
                        
                }
            }

            var hasRigidbody = rc.transform.TryGetComponent(out Rigidbody rb);

            if (hasRigidbody && distanceToRaycast < deadLine)
                rb.AddExplosionForce(500, transform.position, 10, 0.5f, ForceMode.Impulse);
            else if (hasRigidbody)
            {
                if (distanceToRaycast != 0)
                    //TODO: Change values to make perfect explosion :)
                    rb.AddExplosionForce(500 / distanceToRaycast, transform.position, 10, 0.5f, ForceMode.Impulse);
            }
        }

        void IndicateDamage(float damage, Transform DISpawn)
        {
            if (DISpawn == null)
                return;
            var di = Instantiate(damageIndicator);
            if (damage > 0)

                di.GetComponentInChildren<TextMeshPro>().text = "-" + (int)damage;

            else
            {
                di.transform.Find("Block").gameObject.SetActive(true);
                di.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
            }
            di.transform.position = DISpawn.transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    public float damage = 10f;
    public float force = 2f;
    public float radius = 5f;
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            var wormCol = collider.GetComponent<WormData>();
            if (wormCol != null)
            {
                if (wormCol.HasBeenHit() == false)
                {
                    Vector3 temp = wormCol.transform.position - transform.position;
                    wormCol.GetComponent<BasicWormPhysics>().KnockBack(force * (1 - temp.magnitude / radius), (temp.normalized + Vector3.up * 0.8f).normalized);
                }
                wormCol.TakeDamage((int)damage);

            }
        }
        Destroy(gameObject);
    }
    void Start()
    {

    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    public float damage = 10f;
    public float force = 2f;
    public float radius = 5f;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
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
                    RaycastHit ray;
                    Physics.Raycast(transform.position, collider.bounds.center - transform.position, out ray, radius);
                    if (ray.collider.CompareTag("Player"))
                    {
                        Vector3 temp = wormCol.transform.position - transform.position;
                        wormCol.GetComponent<BasicWormPhysics>().KnockBack(force * (1 - temp.magnitude / radius), (temp.normalized + Vector3.up * 1.3f).normalized);
                        wormCol.TakeDamage((int)(damage * temp.magnitude / radius));
                    }         
                }
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

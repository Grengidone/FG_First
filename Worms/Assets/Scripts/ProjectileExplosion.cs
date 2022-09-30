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
                    
                    wormCol.GetComponent<BasicWormPhysics>().KnockBack(force, this.transform.position, radius);
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

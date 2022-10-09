using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("AAAAAAAAAAAH");
            collision.gameObject.GetComponent<WormData>().TakeDamage(10000);
        }
    }
}

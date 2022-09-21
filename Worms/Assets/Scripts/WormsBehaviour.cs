using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{

}
public class WormsBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _cameraLookAt;

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}



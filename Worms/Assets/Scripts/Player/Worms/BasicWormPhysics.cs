using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWormPhysics : MonoBehaviour
{
    CharacterController _characterController;
    float _gravity = 9.81f;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _characterController.SimpleMove(Vector3.zero);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BasicWormPhysics : MonoBehaviour
{

    float _gravity = -9.81f;
    [Header("Y-Axis")]

    [SerializeField] float _maxFallSpeedRatio = 2.5f;
    float _maxFallSpeed;

    [Space]
    [Space]

    [SerializeField] float slowSpeed = 4f;
    [SerializeField] CharacterController charController;


    Vector3 velVector;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        velVector = Vector3.zero;
        _maxFallSpeed = _maxFallSpeedRatio * _gravity;
    }

    // Update is called once per frame
    void Update()
    {

        velVector.y = charController.velocity.y + _gravity * Time.deltaTime;
        charController.Move(velVector * Time.deltaTime);
    }

    public void KnockBack(float force, Vector3 expPos, float radius)
    {
        if (expPos.y < 0) expPos.y = -expPos.y;
    }
}

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
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float groundDelay = 4f;
    [SerializeField] CharacterController charController;
    [SerializeField] bool testGrounded = true;
    [SerializeField] float groundCheckRadius;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] float slowDownSpeed;
    Vector3 groundCheckPosition;
    [SerializeField] Vector3 groundCheckSize;

    public bool isGrounded = true;


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
        groundCheckPosition = transform.position - groundCheckOffset;
        
        if (testGrounded) GroundCheck();

        if (isGrounded) // Remove
        {
            velVector.y = 0;
            Vector3 tempVector = new Vector3(velVector.x, 0f, velVector.z).normalized;
            float tempX = Mathf.Abs(velVector.x) - (tempVector.x * slowDownSpeed * Time.deltaTime) ;
            if (tempX > 0)
            {
                velVector.x -= TestNegative(velVector.x) * tempX ;
            }
            else if (tempX <= 0) 
            { velVector.x = 0; }

            float tempZ = Mathf.Abs(velVector.z) - (tempVector.z * slowDownSpeed * Time.deltaTime) ;
            if (tempZ > 0)
            {
                Debug.LogWarning(tempZ);
                velVector.z -= TestNegative(velVector.z) * tempZ ;
            }
            else if (tempZ <= 0)
            { velVector.z = 0; }
        }
        else
        {
            velVector.y += _gravity * Time.deltaTime;
            Mathf.Clamp(velVector.y, _maxFallSpeed, Mathf.Infinity);
            
        }
        if (gameObject.GetComponent<WormData>().id == 6 && gameObject.GetComponent<WormData>().playerID == 6)
        {
            Debug.LogWarning(velVector.y);

        }
        charController.Move(velVector*Time.deltaTime);

    }

    void GroundCheck()
    {
        isGrounded = charController.isGrounded;
        //isGrounded = Physics.CheckBox (transform.position - groundCheckOffset, groundCheckSize, Quaternion.identity, _layerMask);
    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawCube(groundCheckPosition, groundCheckSize * 2);
    }

    float TestNegative (float test)
    {
        if (test < 0)
        {
            return -1;
        } else if (test > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    IEnumerator GroundedDelay()
    {
        testGrounded = false;
        isGrounded = false;
        yield return new WaitForSeconds(groundDelay);
        testGrounded = true;

    }

    public void KnockBack(float force, Vector3 direction)
    {
        if (direction.y < 0) direction.y = -direction.y;
        velVector += force * direction;
        StartCoroutine(GroundedDelay());
    }
}

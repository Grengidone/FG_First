using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

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

    bool hasJumped = false;

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
        groundCheckPosition = transform.position + groundCheckOffset;
        velVector.y += _gravity * Time.deltaTime;
        velVector.y = Mathf.Clamp(velVector.y, _maxFallSpeed, Mathf.Infinity);
        if (GroundCheck())
        {
            velVector.y = Mathf.Clamp(velVector.y, 0f, Mathf.Infinity);
        } 
       

        charController.Move(velVector * Time.deltaTime);
        /*if (ActivePlayerManager.instance.activePlayer.GetCurrentWorm().id == gameObject.GetComponent<WormData>().id && ActivePlayerManager.instance.activePlayer.GetCurrentWorm().playerID == gameObject.GetComponent<WormData>().playerID)
        {
            //Debug.LogWarning(GroundCheck() + " : Is the check for this object : " + gameObject.name + Time.time);
        }*/
    }
    IEnumerator JumpDelay(float jumpDelay)
    {
        hasJumped = true;
        yield return new WaitForSeconds(jumpDelay);
        hasJumped = false;
    }

    private void FixedUpdate()
    {
        if (GroundCheck() || (ActivePlayerManager.instance.activePlayer.GetCurrentWorm().id == gameObject.GetComponent<WormData>().id && ActivePlayerManager.instance.activePlayer.GetCurrentWorm().playerID == gameObject.GetComponent<WormData>().playerID))
        {
            Vector2 vectorTemp = Vector2.Lerp(new Vector2(velVector.x, velVector.z), new Vector2(0f, 0f), 1 - Time.fixedDeltaTime * 2) ;
            velVector -= new Vector3(vectorTemp.x, 0f, vectorTemp.y);
        }
        
    }

    bool GroundCheck()
    {
        if (!Physics.CheckSphere(groundCheckPosition, groundCheckRadius, _layerMask, QueryTriggerInteraction.Collide) && !charController.isGrounded)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);

    }

    public void Jump(float jumpForce)
    {
        if (GroundCheck() && !hasJumped)
        {
            velVector += Vector3.up * jumpForce;
            StartCoroutine(JumpDelay(0.1f));
        }
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

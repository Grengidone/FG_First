using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private CinemachineFreeLook _cmFreeLook;


    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;
    [SerializeField] Transform _cam;
    private GameObject _currentPlayerWorm;

    void Start()
    {
        _currentPlayerWorm = InitializeManager.players[2].GetWorms()[2].gameObject;
        _characterController = _currentPlayerWorm.GetComponent<CharacterController>();
        _cmFreeLook.GetRig(2).LookAt = _currentPlayerWorm.transform;
        _cmFreeLook.LookAt= _currentPlayerWorm.transform;
        _cmFreeLook.Follow = _currentPlayerWorm.transform;
    }

    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_currentPlayerWorm.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime );
            _currentPlayerWorm.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
    }
}

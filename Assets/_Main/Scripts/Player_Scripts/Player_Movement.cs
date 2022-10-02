using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _floatingJoystick;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator _animator;

    private Vector2 targetDirection;
    private Vector3 targetAngle;
    private float playerSpeed;

    private void Update()
    {
        InputChecker();
        RotatePlayer();
        MovePlayer();
    }

    private void InputChecker()
    {
        targetDirection.x = _floatingJoystick.Horizontal;
        targetDirection.y = _floatingJoystick.Vertical;

        if (targetDirection.magnitude < 0.1f)
        {
            _animator.SetBool("isWalking", false);
            return;
        }
        else
        {
            _animator.SetBool("isWalking", true);
            _animator.SetFloat("RunSpeedMultiplier", Mathf.Lerp(0,1,targetDirection.magnitude));
        }
    }

    private void RotatePlayer()
    {
        targetAngle.y = Mathf.Atan2(targetDirection.x, targetDirection.y) * 180 / Mathf.PI;
        if (targetAngle.y == 0)
            return;
        transform.rotation = Quaternion.Euler(targetAngle);
    }

    private void MovePlayer()
    {
        playerSpeed = movementSpeed * Time.deltaTime * targetDirection.magnitude;
        _rigidbody.velocity = transform.forward * playerSpeed;
    }

}

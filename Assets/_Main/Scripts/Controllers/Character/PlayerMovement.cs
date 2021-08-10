using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the base movement of the player
/// </summary>
public class PlayerMovement : BaseCharacter
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _speed = 4f;
    private float _smoothTime = 0.1f;
    private float _turnSmoothVelocity;

    #region Base Character Inheritance

    public override void SetupCharacter()
    {
        base.SetupCharacter();
        characterCanMove = true;
        playerIsReady = true;
    }

    protected override void MoveCharacter()
    {
        if (!characterCanMove)
            return;

        float horizontalDirection = Input.GetAxisRaw("Horizontal");
        float verticalDirection = Input.GetAxisRaw("Vertical");

        float runSpeed = Input.GetKey(KeyCode.LeftShift) ? 3f : 0f;

        Vector3 direction = new Vector3(horizontalDirection, 0f, verticalDirection).normalized;

        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
            characterRigidbody.velocity = moveDir * (_speed + runSpeed);
            SetSpeedValueAnimation(direction.magnitude * (_speed + runSpeed));
        }
        else
        {
            characterRigidbody.velocity = Vector3.zero;
            SetSpeedValueAnimation(0);
        }

    }

    #endregion

    private void Update()
    {
        if (playerIsReady)
            MoveCharacter();
    }
}

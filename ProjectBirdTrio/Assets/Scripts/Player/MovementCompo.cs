using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class MovementCompo : MonoBehaviour
{
    [SerializeField] Player player = null;

    [SerializeField] float speed = 5;
    [SerializeField] float defautSpeed = 5;
    [SerializeField] float rotationSpeed = 100;
    [SerializeField] float defautGravity = 1;
    [SerializeField] float gravity = 1;
    [SerializeField] float gravityRate = 5;
    [SerializeField] float forwardOnFly = 1;
    [SerializeField] float sprintSpeed = 10;

    [SerializeField] bool isTakeOff = false;
    [SerializeField] bool isFlying = false;
    [SerializeField] bool isLanding = false;
    [SerializeField] bool isFlyUp = false;
    [SerializeField] bool isSprinting = false;

    [SerializeField] float currentHeight = 0;
    [SerializeField] float minFlyHeight = 1.5f;
    [SerializeField] float takeOffSpeed = 2.2f;

    [SerializeField] Rigidbody rigidBody = null;
    [SerializeField] int groundLayer = 6;

    [SerializeField] Quaternion targetRotation;


    public bool IsFlying => isFlying;
    public bool IsLanding => isLanding;
    public bool IsTakeOff => isTakeOff;
    public bool IsSprinting => isSprinting; 

    void Start()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player.Stamina <= 0) isSprinting = false;
        speed = isSprinting ? sprintSpeed : defautSpeed;

        if (isTakeOff)
        {
            TakeOff();
        }
        else if (isFlying)
        {
            ApplyGravity();               
            FlyMove();
            FlyRotate();
            FlyUp();
        }
        else if (isLanding)
        {
            SmoothLanding();
            Move();
            //Rotate();
        }
        else
        {
            Move();
            Rotate();
        }

    }

    private void FlyMove()
    {
        Vector2 _flyDir = player.Input.FlyMove.ReadValue<Vector2>();        

        transform.position += transform.forward * _flyDir.y * speed * Time.deltaTime;
        transform.position += transform.right * _flyDir.x * speed * Time.deltaTime;
    }

    public void FlyUp()
    {
        if (isFlyUp && player.Stamina > 0)
        {
            player.DrainStamina(player.StaminaDrainUse);
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    public void OnFlyUp(InputAction.CallbackContext _context)
    {
        if (_context.performed)isFlyUp = true;
        else if (_context.canceled)isFlyUp = false;
    }

    public void OnSprint(InputAction.CallbackContext _context)
    {
        if (_context.performed) isSprinting = true;
        else if (_context.canceled) isSprinting = false;
    }

    void FlyRotate()
    {
        Vector2 _rotateDir = player.Input.FlyRotate.ReadValue<Vector2>();

        transform.eulerAngles += Vector3.up * _rotateDir.x * rotationSpeed * Time.deltaTime;
        transform.eulerAngles += Vector3.right * -_rotateDir.y * (rotationSpeed * 0.5f) * Time.deltaTime;
    }

    private void ApplyGravity()
    {
        if (player.Stamina <= 0)
            gravity = gravityRate;
        else if (player.Stamina >= 0)
            gravity = defautGravity;
        transform.position += Vector3.down * gravity * Time.deltaTime;
        transform.position += transform.forward * forwardOnFly * Time.deltaTime;
    }

    void Move()
    {
        if (!player) return;
        Vector2 _moveDir = player.Input.Move.ReadValue<Vector2>();

        transform.position += transform.forward * _moveDir.y * speed * Time.deltaTime;
        //transform.position += transform.right * _moveDir.x * moveSpeed * Time.deltaTime;

        //player.Animations.UpdateForwardAnimatorParam(_moveDir.y);
        //player.Animations.UpdateRightAnimatorParam(_moveDir.x);
    }

    void Rotate()
    {
        if (!player) return;
        Vector2 _rotateDir = player.Input.Rotate.ReadValue<Vector2>();
        transform.eulerAngles += transform.up * _rotateDir.x * rotationSpeed * Time.deltaTime;
    }

    public void StartFlying()
    {
        if (isTakeOff && isFlying) return;
        Debug.Log("Start to Fly");
        isTakeOff = true;
        rigidBody.useGravity = false;
        currentHeight = transform.position.y;
    }

    void TakeOff()
    {
        if (transform.position.y < currentHeight + minFlyHeight)
        {
            transform.position += Vector3.up * takeOffSpeed * Time.deltaTime;
            isFlying = true;
        }
        else
        {
            isTakeOff = false;
            player.Input.SwitchToFlyMode();
            Debug.Log("Fly Input");
        }
    }

    public void Land()
    {
        isLanding = true;
        rigidBody.useGravity = true;

        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        player.Input.SwitchToGroundMode();
       // Debug.Log("Ground Input");
    }

    public void ForceLand()
    {
        if (isFlying)
        {
            Land(); 
        }
    }

    void SmoothLanding()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            isLanding = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            Land();
            isFlying = false;
        }
    }
}

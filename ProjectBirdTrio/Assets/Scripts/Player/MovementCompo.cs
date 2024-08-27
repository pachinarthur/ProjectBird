using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCompo : MonoBehaviour
{
    [SerializeField] Player player = null;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 25f;

    [SerializeField] bool isTakeOff = false;
    [SerializeField] bool isFlying = false;
    [SerializeField] bool isLanding = false;

    [SerializeField] float currentHeight = 0;
    [SerializeField] float minFlyHeight = 1.5f;
    [SerializeField] float flySpeed = 10f;
    [SerializeField] float flyRotateSpeed = 250;
    [SerializeField] float takeOffSpeed = 2.2f;

    [SerializeField] Rigidbody rigidBody = null;
    [SerializeField] int groundLayer = 6;

    [SerializeField] Quaternion targetRotation;
    [SerializeField] float rotationSpeed = 50f;

    public bool IsFlying => isFlying;
    public bool IsLanding => isLanding;
    public bool IsTakeOff => isTakeOff;

    void Start()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isTakeOff)
        {
            TakeOff();
        }
        else if (isFlying)
        {
            FlyMove();
            FlyRotate();
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
        Vector3 _flyDir = player.Input.FlyMove.ReadValue<Vector3>();

        transform.position += transform.forward * _flyDir.z * flySpeed * Time.deltaTime;
        transform.position += transform.right * _flyDir.x * flySpeed * Time.deltaTime;

        transform.position += transform.up * _flyDir.y * flySpeed * Time.deltaTime;
    }

    void FlyRotate()
    {
        Vector2 _rotateDir = player.Input.FlyRotate.ReadValue<Vector2>()*2;
        transform.eulerAngles += Vector3.up * _rotateDir.x * flyRotateSpeed * Time.deltaTime;
        transform.eulerAngles += Vector3.right * -_rotateDir.y * flyRotateSpeed * Time.deltaTime;
    }


    void Move()
    {
        if (!player) return;
        Vector2 _moveDir = player.Input.Move.ReadValue<Vector2>();
        transform.position += transform.forward * _moveDir.y * moveSpeed * Time.deltaTime;
        //transform.position += transform.right * _moveDir.x * moveSpeed * Time.deltaTime;

        //player.Animations.UpdateForwardAnimatorParam(_moveDir.y);
        //player.Animations.UpdateRightAnimatorParam(_moveDir.x);
    }

    void Rotate()
    {
        if (!player) return;
        float _rotateDir = player.Input.Rotate.ReadValue<float>();
        transform.eulerAngles += transform.up * _rotateDir * rotateSpeed * Time.deltaTime;
    }

    public void StartFlying()
    {
        isTakeOff = true;
        rigidBody.useGravity = false;
        currentHeight = transform.position.y;
    }

    void TakeOff()
    {
        if (transform.position.y < currentHeight + minFlyHeight)
        {
            transform.position += Vector3.up * takeOffSpeed * Time.deltaTime;
        }
        else
        {
            isTakeOff = false;
            isFlying = true;
            player.Input.SwitchToFlyMode();
            Debug.Log("Fly Input");
        }
    }

    public void Land()
    {
        isFlying = false;
        isLanding = true;
        rigidBody.useGravity = true;

        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        player.Input.SwitchToGroundMode();
        Debug.Log("Ground Input");
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
        }
    }
}

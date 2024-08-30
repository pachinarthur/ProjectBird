using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test : MonoBehaviour
{

    [SerializeField] InputComponent inputs = null;
    private CharacterController characterController;
    [SerializeField] float speed = 5;
    [SerializeField] float rotateSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        characterController = GetComponent<CharacterController>();
        inputs = GetComponent<InputComponent>();
    }

    private void OnEnable()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector2 _movement = inputs.Move.ReadValue<Vector2>();
        
        float _forwardValue = _movement.y;
        float _rightValue = _movement.x;

        Vector3 _move = new Vector3(_rightValue, 0, _forwardValue);

        characterController.Move(_move*Time.deltaTime*speed);
    }

    void Rotate()
    {
        float _rotValue = inputs.Rotate.ReadValue<float>();
        transform.eulerAngles += transform.up * rotateSpeed * _rotValue * Time.deltaTime;
    }
}

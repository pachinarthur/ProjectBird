using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    [SerializeField] protected Player_Test playerRef = null;

    [SerializeField] Input_Player controls = null;
    [SerializeField] InputAction move = null;
    [SerializeField] InputAction rotate = null;


    public InputAction Move => move;
    public InputAction Rotate => rotate;

    private void Awake()
    {
        controls = new Input_Player();
    }

    private void OnEnable()
    {
        move = controls.Ground.Move;
        rotate = controls.Ground.Rotate;

        move.Enable();
        rotate.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        playerRef = GetComponent<Player_Test>();
    }
}

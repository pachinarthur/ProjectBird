using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputCompo : MonoBehaviour
{
    [SerializeField] PlayerControls controls = null;
    [SerializeField] Player player = null;

    [SerializeField] InputAction move = null;
    [SerializeField] InputAction rotate = null;
    [SerializeField] InputAction fly = null;
    [SerializeField] InputAction flyMove = null;
    [SerializeField] InputAction flyRotate = null;
    [SerializeField] InputAction forceLanding = null;
    [SerializeField] InputAction flyUp = null;

    [SerializeField] InputAction sprintAir = null;
    [SerializeField] InputAction sprintGround = null;

    public InputAction Move => move;
    public InputAction Rotate => rotate;
    public InputAction Fly => fly;
    public InputAction FlyMove => flyMove;
    public InputAction FlyRotate => flyRotate;
    public InputAction ForceLanding => forceLanding;
    public InputAction FlyUp => flyUp;
    public InputAction SprintAir => sprintAir;
    public InputAction SprintGround => sprintGround;

    private void Awake()
    {
        controls = new PlayerControls();
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
        Invoke("Init", 0.5f);      
    }
    private void Init()
    {
        move = controls.Ground.Movement;
        rotate = controls.Ground.Rotation;
        fly = controls.Ground.Fly;

        flyMove = controls.Fly.Movement;
        flyRotate = controls.Fly.Rotation;
        forceLanding = controls.Fly.Land;
        flyUp = controls.Fly.FlyUp;

        sprintGround = controls.Ground.Sprint;
        sprintAir = controls.Fly.Sprint;

        move.Enable();
        rotate.Enable();
        fly.Enable();
        flyMove.Enable();
        flyRotate.Enable();
        forceLanding.Enable();
        flyUp.Enable();
        sprintGround.Enable();
        sprintAir.Enable();

        fly.performed += player.FlyMode;
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
        fly.Disable();
        flyMove.Disable();
        flyRotate.Disable();

        fly.performed -= player.FlyMode;
        forceLanding.performed -= player.LandMode;
    }

    public void SwitchToFlyMode()
    {
        move.Disable();
        rotate.Disable();
        fly.Disable();
        sprintGround.performed -= player.Movement.OnSprint;
        sprintGround.canceled -= player.Movement.OnSprint;

        flyUp.Enable();
        flyMove.Enable();
        flyRotate.Enable();
        forceLanding.Enable();

        flyUp.performed += player.Movement.OnFlyUp;
        flyUp.canceled += player.Movement.OnFlyUp;
        forceLanding.performed += player.LandMode;

        sprintAir.performed += player.Movement.OnSprint;
        sprintAir.canceled += player.Movement.OnSprint;
    }
    public void SwitchToGroundMode()
    {
        flyMove.Disable();
        flyRotate.Disable();
        flyUp.Disable();
        forceLanding.Disable();
        flyUp.performed -= player.Movement.OnFlyUp;
        flyUp.canceled -= player.Movement.OnFlyUp;
        forceLanding.performed -= player.LandMode;
        sprintAir.performed -= player.Movement.OnSprint;
        sprintAir.canceled -= player.Movement.OnSprint;


        move.Enable();
        rotate.Enable();
        fly.Enable();
        sprintGround.performed += player.Movement.OnSprint;
        sprintGround.canceled += player.Movement.OnSprint;
    }
}

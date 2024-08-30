using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputCompo : MonoBehaviour
{
    [SerializeField] PlayerSeedMechanics playerSeedMechanics = null;

    [SerializeField] PlayerControls controls = null;
    [SerializeField] Player player = null;
    [SerializeField] PoopBirdMechanic poopBirdMechanic = null;

    [SerializeField] InputAction move = null;
    [SerializeField] InputAction rotate = null;
    [SerializeField] InputAction fly = null;
    [SerializeField] InputAction flyMove = null;
    [SerializeField] InputAction flyRotate = null;
    [SerializeField] InputAction forceLanding = null;
    [SerializeField] InputAction flyUp = null;
    [SerializeField] InputAction pickup = null;
    [SerializeField] InputAction aim = null;
    [SerializeField] InputAction poop = null;

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
    public InputAction Pickup => pickup;
    public InputAction Poop => poop;

    private void Awake()
    {
        controls = new PlayerControls();
        player = GetComponent<Player>();
        playerSeedMechanics = GetComponent<PlayerSeedMechanics>();
        poopBirdMechanic = GetComponent<PoopBirdMechanic>();
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
        pickup = controls.Ground.PickUp;
        aim = controls.Fly.Aim;
        poop = controls.Fly.Poop;

        move.Enable();
        rotate.Enable();
        fly.Enable();
        flyMove.Enable();
        flyRotate.Enable();
        forceLanding.Enable();
        flyUp.Enable();
        sprintGround.Enable();
        sprintAir.Enable();
        pickup.Enable();
        poop.Enable();

        fly.performed += player.Movement.FlyMode;
        pickup.performed += playerSeedMechanics.OnCollectSeed;

        aim.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
        fly.Disable();
        flyMove.Disable();
        flyRotate.Disable();
        aim.Disable();

        fly.performed -= player.Movement.FlyMode;
        //forceLanding.performed -= player.LandMode;
        pickup.performed -= playerSeedMechanics.OnCollectSeed;
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
        forceLanding.performed += player.Movement.LandMode;

        sprintAir.performed += player.Movement.OnSprint;
        sprintAir.canceled += player.Movement.OnSprint;

        if (poopBirdMechanic != null)
        {
            aim.performed += poopBirdMechanic.OnAim;
            aim.canceled += poopBirdMechanic.OnAim;
        }
        else
        {
            Debug.LogError("PoopBirdMechanic is not assigned or initialized!");
        }

        poop.performed += poopBirdMechanic.OnPoop;
    }
    public void SwitchToGroundMode()
    {
        flyMove.Disable();
        flyRotate.Disable();
        flyUp.Disable();
        forceLanding.Disable();
        flyUp.performed -= player.Movement.OnFlyUp;
        flyUp.canceled -= player.Movement.OnFlyUp;
        forceLanding.performed -= player.Movement.LandMode;
        sprintAir.performed -= player.Movement.OnSprint;
        sprintAir.canceled -= player.Movement.OnSprint;
        aim.performed -= poopBirdMechanic.OnAim;
        aim.canceled -= poopBirdMechanic.OnAim;
        poop.performed -= poopBirdMechanic.OnPoop;


        move.Enable();
        rotate.Enable();
        fly.Enable();
        sprintGround.performed += player.Movement.OnSprint;
        sprintGround.canceled += player.Movement.OnSprint;
    }
}

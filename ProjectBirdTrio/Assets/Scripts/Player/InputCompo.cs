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

    public InputAction Move => move;
    public InputAction Rotate => rotate;
    public InputAction Fly => fly;
    public InputAction FlyMove => flyMove;
    public InputAction FlyRotate => flyRotate;
    public InputAction ForceLanding => forceLanding;
    public InputAction FlyUp => flyUp;

    private void Awake()
    {
        controls = new PlayerControls();
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        move = controls.Ground.Movement;
        rotate = controls.Ground.Rotation;
        fly = controls.Ground.Fly;

        flyMove = controls.Fly.Movement;
        flyRotate = controls.Fly.Rotation;
        forceLanding = controls.Fly.Land;
        flyUp = controls.Fly.FlyUp;

        move.Enable();
        rotate.Enable();
        fly.Enable();
        flyMove.Enable();
        flyRotate.Enable();        
        forceLanding.Enable();
        flyUp.Enable();

        fly.performed += player.FlyMode;
        
        forceLanding.performed += player.LandMode;

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

        flyUp.Enable();
        flyMove.Enable();
        flyRotate.Enable();
        forceLanding.Enable();
        flyUp.performed += player.Movement.OnFlyUp;
        flyUp.canceled += player.Movement.OnFlyUp;
    }
    public void SwitchToGroundMode()
    {
        flyMove.Disable();
        flyRotate.Disable();
        flyUp.Disable();
        forceLanding.Disable();
        flyUp.performed -= player.Movement.OnFlyUp;
        flyUp.canceled -= player.Movement.OnFlyUp;


        move.Enable();
        rotate.Enable();
        fly.Enable();
    }
}

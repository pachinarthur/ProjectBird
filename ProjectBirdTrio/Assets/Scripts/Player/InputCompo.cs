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

    public InputAction Move => move;
    public InputAction Rotate => rotate;
    public InputAction Fly => fly;
    public InputAction FlyMove => flyMove;
    public InputAction FlyRotate => flyRotate;
    public InputAction ForceLanding => forceLanding;

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

        move.Enable();
        rotate.Enable();
        fly.Enable();
        flyMove.Enable();
        flyRotate.Enable();
        forceLanding.Enable();

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

        fly.Enable();
        flyMove.Enable();
        flyRotate.Enable();
    }
    public void SwitchToGroundMode()
    {
        flyMove.Disable();
        flyRotate.Disable();

        move.Enable();
        rotate.Enable();
    }
}

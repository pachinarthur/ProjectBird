using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] MovementCompo movement = null;
    [SerializeField] InputCompo input = null;
    [SerializeField] AnimCompo animations = null;

    [SerializeField] bool isFlying = false;

    public MovementCompo Movement => movement;
    public InputCompo Input => input;
    public AnimCompo Animations => animations;

    void Start()
    {
        Init();
    }

    void Update()
    {
        isFlying = movement.IsFlying;
    }

    void Init()
    {
        movement = GetComponent<MovementCompo>();
        input = GetComponent<InputCompo>();
        animations = GetComponent<AnimCompo>();
    }
    public void FlyMode(InputAction.CallbackContext _context)
    {
        if (isFlying)
        {
            return;
        }

        movement.StartFlying();
        isFlying = true;
    }

    public void LandMode(InputAction.CallbackContext context)
    {
        Debug.Log("LandMode");

        if (isFlying)
        {
            Debug.Log("Landing...?");
            movement.ForceLand();
            isFlying = false;
        }
    }
}

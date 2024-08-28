using System;
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

    [SerializeField] float stamina = 100;
    [SerializeField] float maxStamina = 100;
    [SerializeField] float staminaDrainRate = 20;
    [SerializeField] float staminaRecoveryRate = 50;

    public MovementCompo Movement => movement;
    public InputCompo Input => input;
    public AnimCompo Animations => animations;
    public float Stamina => stamina;
    public float StaminaDrainRate => staminaDrainRate;

    void Start()
    {
        Init();
    }

    void Update()
    {
        isFlying = movement.IsFlying;
        if (!isFlying)
            RecoverStamina();
    }

    public void DrainStamina(float _amount)
    {
        stamina -= _amount * Time.deltaTime;
        if (stamina < 0)
        {
            stamina = 0;
            movement.ForceLand(); 
        }
    }

    private void RecoverStamina()
    {
        stamina += staminaRecoveryRate * Time.deltaTime;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
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

    public void LandMode(InputAction.CallbackContext _context)
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

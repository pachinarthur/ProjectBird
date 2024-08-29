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
    [SerializeField] float staminaDrainRate = 5;
    [SerializeField] float staminaDrainUse = 10;
    [SerializeField] float staminaRecoveryRate = 50;

    public MovementCompo Movement => movement;
    public InputCompo Input => input;
    public AnimCompo Animations => animations;
    public float Stamina => stamina;
    public float StaminaDrainRate => staminaDrainRate;
    public float StaminaDrainUse => staminaDrainUse;

    void Start()
    {
        Init();
    }

    void Init()
    {
        movement = GetComponent<MovementCompo>();
        input = GetComponent<InputCompo>();
        animations = GetComponent<AnimCompo>();
        stamina = maxStamina;
      //  Debug.Log("Init Fait");
    }
    void Update()
    {
        isFlying = movement.IsFlying;
        if (!isFlying && !movement.IsSprinting)
            RecoverStamina();
        else if (isFlying && !movement.IsSprinting)
            DrainStamina(StaminaDrainRate);
        if (movement.IsSprinting)
            DrainStamina(staminaDrainUse);
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

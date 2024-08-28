using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PoopBirdMechanic : MonoBehaviour
{
    public event Action decrementPoopValue = null;
    [SerializeField] PlayerSeedMechanics seedMecha = null;
    [SerializeField] Player playerRef = null;
    [SerializeField] GameObject projectileRef = null;
    [SerializeField] bool canAttack = false; //, candecrementvalue = false;             //Can attack que en vol + changement de camera
    //[SerializeField] bool attack = false;             
    [SerializeField] bool isLanding = false;

    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera aimCamera;
    [SerializeField] MovementCompo movementComponent;


    //[SerializeField] float maxvalue = 100, decrementvalue = 2, actualvalue = 100, t = 0;
    // Start is called before the first frame update
    void Start()
    {
        seedMecha = GetComponent<PlayerSeedMechanics>();
        movementComponent = GetComponent<MovementCompo>();
        playerRef = GetComponent<Player>();

        //mainCamera.Priority = 10;
        //aimCamera.Priority = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementComponent != null && movementComponent.IsLanding)
        {
            SwitchToMainCamera();
        }
        if (isLanding) SwitchToMainCamera();
        if (canAttack)
        {
            poopProjectileLaunch();
            canAttack = false;
        }
        //print(seedMecha.PoopMeter);
        //decrement();
        //t += Time.deltaTime;
    }

     void SwitchToMainCamera()
    {
        aimCamera.Priority = 5;
        mainCamera.Priority = 10;  
    }

    void poopProjectileLaunch()
    {
        if (seedMecha.PoopMeter >= 1)
        {
            Instantiate(projectileRef, transform.position, transform.rotation);
            //attack = false;
            decrementPoopValue?.Invoke();
        }
        else
        {
        print("can't poop");
        }
    }

    //void decrement()
    //{
    //    if (candecrementvalue)
    //    {
    //    }
    //}

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mainCamera.Priority = 5;
            aimCamera.Priority = 10;
        }
        else if (context.canceled)
        {
            aimCamera.Priority = 5;
            mainCamera.Priority = 10;
        }
    }

    public void OnPoop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canAttack = true;
        }
    }
}

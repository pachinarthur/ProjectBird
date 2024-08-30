using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.TimeZoneInfo;

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

    [SerializeField] public CinemachineBrain cinemachineBrain;
    [SerializeField] float transitionTime = 3;


    //[SerializeField] float maxvalue = 100, decrementvalue = 2, actualvalue = 100, t = 0;
    // Start is called before the first frame update
    void Start()
    {
        seedMecha = GetComponent<PlayerSeedMechanics>();
        movementComponent = GetComponent<MovementCompo>();
        playerRef = GetComponent<Player>();

        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        //mainCamera.Priority = 10;
        //aimCamera.Priority = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.Movement.IsFlying == false)
        {
            aimCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }
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
        SetTransitionTime();
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
        //if (context.performed)
        //{
        //    mainCamera.Priority = 5;
        //    aimCamera.Priority = 10;
        //}
        //else if (context.canceled)
        //{
        //    aimCamera.Priority = 5;
        //    mainCamera.Priority = 10;
        //}

        if (context.performed)
        {
            mainCamera.gameObject.SetActive(false);
            aimCamera.gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            aimCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }
    }

    public void OnPoop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canAttack = true;
        }
    }
    public void SetTransitionTime()
    {
        if (cinemachineBrain != null)
        {
            cinemachineBrain.m_DefaultBlend.m_Time = transitionTime;
        }
    }
}
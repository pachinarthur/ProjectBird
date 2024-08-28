using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoopBirdMechanic : MonoBehaviour
{
    public event Action decrementPoopValue = null;
    [SerializeField] PlayerSeedMechanics playerRef = null;
    [SerializeField] GameObject projectileRef = null;
    [SerializeField] bool attack = false;
    [SerializeField] bool canAttack = false, candecrementvalue = false;


    [SerializeField] float maxvalue = 100, decrementvalue = 2, actualvalue = 100, t = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GetComponent<PlayerSeedMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack == true) poopProjectileLaunch();
        print(playerRef.PoopMeter);
        decrement();
        t += Time.deltaTime;
    }
    void poopProjectileLaunch()
    {
        if (attack == true && playerRef.PoopMeter >= 1)
        {
            Instantiate(projectileRef, transform.position, transform.rotation);
            attack = false;
            decrementPoopValue.Invoke();
        }
        attack = false;
        print("can't poop");
    }

    void decrement()
    {
        if (candecrementvalue)
        {
        }
    }
}

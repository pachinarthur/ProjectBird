using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSeedMechanics : MonoBehaviour                    // A.K.A Pickup Mechanics
{
    [SerializeField] Player playerRef = null;
    [SerializeField] bool canPoop = false;
    //[SerializeField] bool canEat = false;
    [SerializeField] int poopMeter = 0;
    [SerializeField] List<SeedMechanics> allSeeds = null;
    [SerializeField] SeedMechanics closestSeeds = null;
    [SerializeField] PoopBirdMechanic poopBirdRef = null;

    public bool CanPoop => canPoop;
    public int PoopMeter => poopMeter;
    // Start is called before the first frame update
    void Start()
    {
        poopBirdRef = GetComponent<PoopBirdMechanic>();
        playerRef = GetComponent<Player>();
        poopBirdRef.decrementPoopValue += DecrementPoop;
}
    void SortByClosest()
    {
        allSeeds = allSeeds.OrderBy(_x => Vector3.Distance(transform.position, _x.transform.position)).ToList();
        closestSeeds = allSeeds.FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        //if (canEat == true) EatSeed();
    }
    void DecrementPoop()
    {
        poopMeter -= 1;
    }
    private void OnTriggerEnter(Collider _other)
    {
        
        Physics.IgnoreLayerCollision(8, 9); //mettre le numéro du layer projectile et player)


        //allSeeds = FindObjectsOfType<SeedMechanics>().ToList();           //Taf de correntin
        //SortByClosest();
        //closestSeeds = allSeeds[0];
        //canEat = true;

        SeedMechanics seed = _other.GetComponent<SeedMechanics>();          // Taf revisité
        if (seed != null)
        {
            allSeeds.Add(seed);
            SortByClosest();
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        //canEat = false;
        //allSeeds.Clear();

        SeedMechanics _seed = _other.GetComponent<SeedMechanics>();
        if (_seed != null && allSeeds.Contains(_seed))
        {
            allSeeds.Remove(_seed);
            SortByClosest();
        }
    }
    public void EatSeed()
    {
        if (closestSeeds != null)
        {
            playerRef.Animations.UpdatePickupAnimatorParam(true);
            SeedMechanics _seedToRemove = closestSeeds;
            _seedToRemove.gameObject.transform.localScale = Vector3.zero;
            poopMeter += 1;
            allSeeds.Remove(_seedToRemove);
            closestSeeds = allSeeds.Count > 0 ? allSeeds[0] : null;
            Invoke("StopPickupAnimation", 0.5f);

            //closestSeeds.gameObject.transform.localScale = Vector3.zero;
            //closestSeeds = allSeeds[0];
            //allSeeds.Remove(closestSeeds);
            //canEat = false;
        }
    }
    void StopPickupAnimation()          //Triche timer
    {
        playerRef.Animations.UpdatePickupAnimatorParam(false);
    }

    public void OnCollectSeed(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            EatSeed();
        }
    }
}

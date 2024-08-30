using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSeedMechanics : MonoBehaviour                    // A.K.A Pickup Mechanics
{

    [SerializeField] bool canPoop = false;
    //[SerializeField] bool canEat = false;
    [SerializeField] int poopMeter = 0;
    //[SerializeField] List<SeedMechanics> allSeeds = null;
    [SerializeField] SeedMechanics closestSeeds = null;
    [SerializeField] PoopBirdMechanic poopBirdRef = null;

    public bool CanPoop => canPoop;
    public int PoopMeter => poopMeter;
    // Start is called before the first frame update
    void Start()
    {
        poopBirdRef = GetComponent<PoopBirdMechanic>();
        poopBirdRef.decrementPoopValue += DecrementPoop;
}
    //void SortByClosest()
    //{
    //    allSeeds = allSeeds.OrderBy(_x => Vector3.Distance(transform.position, _x.transform.position)).ToList();
    //    closestSeeds = allSeeds.FirstOrDefault();
    //}

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

        //SeedMechanics seed = _other.GetComponent<SeedMechanics>();          // Taf revisité
        //if (seed != null)
        //{
        //    allSeeds.Add(seed);
        //   // SortByClosest();
        //}
       closestSeeds = _other.GetComponent<SeedMechanics>();          // Taf revisité v2 (corentin)
    }
    private void OnTriggerExit(Collider _other)
    {
        //canEat = false;
        //allSeeds.Clear();

        //  SeedMechanics seed = _other.GetComponent<SeedMechanics>();
        // if (seed != null && allSeeds.Contains(seed))
        //{
        //    allSeeds.Remove(seed);
        //   // SortByClosest();
        //}
        closestSeeds = null;
    }
    public void EatSeed()
    {
        if (closestSeeds != null)
        {
            SeedMechanics _seedToRemove = closestSeeds;
            _seedToRemove.gameObject.transform.localScale = Vector3.zero;
            poopMeter += 1;
          //  allSeeds.Remove(_seedToRemove);
            //closestSeeds = allSeeds.Count > 0 ? allSeeds[0] : null;

            closestSeeds = null;

            //closestSeeds.gameObject.transform.localScale = Vector3.zero;
            //closestSeeds = allSeeds[0];
            //allSeeds.Remove(closestSeeds);
            //canEat = false;
        }
    }

    public void OnCollectSeed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EatSeed();
        }
    }
}

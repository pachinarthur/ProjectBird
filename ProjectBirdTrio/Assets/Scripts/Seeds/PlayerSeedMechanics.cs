using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSeedMechanics : MonoBehaviour
{

    [SerializeField] bool canPoop = false;
    [SerializeField] bool canEat = false;
    [SerializeField] bool buttonEat = false; //a remplacer par l'input qui servira a interagir avec les graines
    [SerializeField] int poopMeter = 0;
    [SerializeField] List<SeedMechanics> allSeeds = null;
    [SerializeField] SeedMechanics closestSeeds = null;
    [SerializeField] PoopBirdMechanic poopBirdRef = null;

    public bool CanPoop => canPoop; //a utiliser pour le mode vol
    public int PoopMeter => poopMeter;
    // Start is called before the first frame update
    void Start()
    {
        poopBirdRef = GetComponent<PoopBirdMechanic>();
        poopBirdRef.decrementPoopValue += DecrementPoop;
}
    void SortByClosest()
    {
        allSeeds = allSeeds.OrderBy(_x => Vector3.Distance(transform.position, _x.transform.position)).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (canEat == true) EatSeed();

    }
    void DecrementPoop()
    {
        poopMeter -= 1;
    }
    private void OnTriggerEnter(Collider _other)
    {
        
        Physics.IgnoreLayerCollision(8, 9); //mettre le numéro du layer projectile et player)
        allSeeds = FindObjectsOfType<SeedMechanics>().ToList();
        SortByClosest();
        closestSeeds = allSeeds[0];
        canEat = true;
    }
    private void OnTriggerExit(Collider other)
    {
        canEat = false;
        allSeeds.Clear();
    }
    void EatSeed()
    {
        if(buttonEat == true) //retirer cette ligne pour faire fonctionner l'input une fois mis en place
        {
            closestSeeds.gameObject.transform.localScale = Vector3.zero;
            poopMeter += 1;
            closestSeeds = allSeeds[0];
            canEat = false;
            buttonEat = false;
        }
    }
}

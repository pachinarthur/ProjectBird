using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GarbageManager : MonoBehaviour
{
    [SerializeField] GarbageCollectible garbageCollectible = null; //s'active tout seul quand tu es proche d'un déchet
    [SerializeField] GarbageTrash garbageTrash = null; //s'active tout seul quand tu es proche d'une poubelle
    [SerializeField] GarbageCollected garbageCollected = null; //récupère le déchet dans l'enfant pour le rendre invisible (regarde le prefab se trouvant dans assets > Scripts > Corentin PoopBirdMechanics > Piaf avec les méchaniques de grab de déchets
    [SerializeField] bool canCollect = false; //s'active tout seul quand tu es proche d'un déchet
    [SerializeField] bool inputGrab = false, inputDeposit = false; //A remplacer par un input
    [SerializeField] bool cantGetOtherCollectible = false; //s'active tout seul quand tu a rammasé un déchet
    [SerializeField] bool canDepositCollectible = false; //s'active tout seul quand tu es proche d'une poubelle
    [SerializeField] int score = 0;

    public int Score => score;
    // Start is called before the first frame update
    void Start()
    {
        garbageCollected = GetComponentInChildren<GarbageCollected>();
        garbageCollected.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Grab();
        Deposit();
    }
    private void OnTriggerEnter(Collider _other)
    {
        garbageCollectible = _other.gameObject.GetComponent<GarbageCollectible>();
        garbageTrash = _other.gameObject.GetComponent<GarbageTrash>();
        if (garbageCollectible != null) canCollect = true;
        if (garbageTrash != null) canDepositCollectible = true;
    }
    private void OnTriggerExit(Collider _other)
    {
        garbageCollectible = null;
        garbageTrash = null;
        canCollect = false;
        canDepositCollectible = false;
    }
    void Grab() //a faire en input
    {
        if (inputGrab == true && canCollect == true && garbageCollectible != null && cantGetOtherCollectible == false)
        {
            GarbageCollectible _toDestroy = garbageCollectible;
            garbageCollectible = null;
            Destroy(_toDestroy.gameObject);
            _toDestroy = null;
            inputGrab = false;
            garbageCollected.gameObject.SetActive(true);
            cantGetOtherCollectible = true;
        }
        if (inputGrab == true && canCollect == true && garbageCollectible != null && cantGetOtherCollectible == true) //a retirer quand les inputs seront mis
            print("i can't eat i alredy have something in my bec je sais pas comment on dit bec en anglais deso");
        inputGrab = false;
    }
    void Deposit()
    {
        if(inputDeposit == true && cantGetOtherCollectible == true && canDepositCollectible == true)
        {
            garbageCollected.gameObject.SetActive(false);
            cantGetOtherCollectible = false;
            score += 1;
        }
        if (inputDeposit == true && cantGetOtherCollectible == false && canDepositCollectible == true)
            print("i can't place something in the trash bc i don't have anything pls give me something to get like that i can trow it in this trash i don't like when i can't place something in a trash");
    }

}

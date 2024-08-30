using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone_ZoneBase : MonoBehaviour
{
    [SerializeField] int maxNpc = 0;
    [SerializeField] List<GameObject> npcs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFull()
    {
        return npcs.Count >= maxNpc;
    }

    
    public void AddNpc(GameObject npc)
    {
        npcs.Add(npc);
    }

    public void RemoveNpc(GameObject npc) {
        npcs.Remove(npc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)//12 for NPC
        {
            Debug.Log("NPC enter Zone");  
            //On ajoute le npc a la zone
            AddNpc(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            Debug.Log("NPC exit Zone");
            //On retire le npc de la zone
            RemoveNpc(other.gameObject);
        }
    }
   
}

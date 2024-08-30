using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GarbageCollectible : MonoBehaviour
{
    [SerializeField] GameObject garbageItem = null;
    [SerializeField] GameObject itemToMove = null;

    public GameObject GarbageItem => garbageItem;
    // Start is called before the first frame update
    void Start()
    {
        //script des déchets au sol
        itemToMove = Instantiate(garbageItem, new Vector3(transform.position.x, transform.position.y + 1,transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

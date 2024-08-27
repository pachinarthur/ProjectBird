using LowPolyWater;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBounce : MonoBehaviour
{
    [SerializeField] LowPolyWater.LowPolyWater polyWater = null;
    [SerializeField] float boatDeepWater = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        polyWater = FindAnyObjectByType<LowPolyWater.LowPolyWater>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _boatPoision = gameObject.transform.position;
       _boatPoision.y =  polyWater.Test - boatDeepWater;
        gameObject.transform.position = _boatPoision;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpanGroundPoop : MonoBehaviour
{
    [SerializeField] float lifeSpan = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime();
    }
    void lifeTime()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0) DestroyItem();
    }
    void DestroyItem()
    {
        Destroy(gameObject);
    }
}

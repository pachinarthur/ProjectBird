using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IA_PNJ_ThrowComponent : MonoBehaviour
{

    public event Action OnTrashThrown = null;
    [SerializeField] GarbageCollectible trash = null;
    [SerializeField] bool hasThrow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Init()
    {
        SetHasThrow(false);
    }

    public void ThrowTrash()
    {
        Instantiate(trash, transform.position, Quaternion.identity);
        SetHasThrow(false);
        OnTrashThrown?.Invoke();
    }

    public void SetHasThrow(bool _hasThrow)
    {
        hasThrow = _hasThrow;
    }

    public bool GetHasThrow()
    {
        return hasThrow;
    }


}

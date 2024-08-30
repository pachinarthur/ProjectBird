using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFadeUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVisilibity()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}

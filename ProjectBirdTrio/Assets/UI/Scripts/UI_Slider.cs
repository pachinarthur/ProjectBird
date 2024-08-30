using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slider : MonoBehaviour
{
    [SerializeField] Slider sliderRef = null;
    [SerializeField] Player playerRef = null;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderRef.value = playerRef.Stamina;
    }
}

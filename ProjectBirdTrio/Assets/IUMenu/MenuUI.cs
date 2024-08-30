using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void SetVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

}

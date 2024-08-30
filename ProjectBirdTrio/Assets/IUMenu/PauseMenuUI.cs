using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public void SetVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}

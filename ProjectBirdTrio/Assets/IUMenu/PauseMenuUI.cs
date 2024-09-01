using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuUI : MonoBehaviour
{
    public void SetVisibility(InputAction.CallbackContext context)
    {
        Debug.Log("SetVisibility");
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void SetVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContinueUI : MonoBehaviour
{
    [SerializeField] PauseMenuUI settingUI = null;
    [SerializeField] Button backButtonButton = null;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        backButtonButton.onClick.AddListener(settingUI.SetVisibility);
    }
}

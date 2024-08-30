using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBackMenuUI : MonoBehaviour
{
    [SerializeField] Button backButtonButton = null;
    [SerializeField] string nomScene = "SampleScene";

    void Start()
    {
        Init();
    }

    void Init()
    {
        backButtonButton.onClick.AddListener(PlayGame);
    }

    void PlayGame()
    {

        SceneManager.LoadScene(nomScene, LoadSceneMode.Single);
    }
}

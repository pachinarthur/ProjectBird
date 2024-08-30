using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPlayUI : MonoBehaviour
{
    [SerializeField] Button playButton = null;
    [SerializeField] string nomScene = "SampleScene";
    [SerializeField] ImageFadeUI imageFadeUI = null;
    [SerializeField] MenuFadeUI menuFadeUI = null;
    [SerializeField] CanvasGroup canvasGroup = null;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {
        menuFadeUI = FindObjectOfType<MenuFadeUI>();
        playButton.onClick.AddListener(PlayGame);
    }

    void PlayGame()
    {
        Debug.Log("PlayGame");
        imageFadeUI.SetVisilibity();
        //SceneManager.LoadScene(nomScene, LoadSceneMode.Single);
        StartCoroutine(menuFadeUI.FadeCanvaGroup(canvasGroup,canvasGroup.alpha,0));
        ;
    }
}

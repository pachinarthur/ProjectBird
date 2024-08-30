using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] Button restartButton = null;
    [SerializeField] Button exitButton = null;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        print("test");
    }
    void QuitGame()
    {
        print("test2");
    }
    
}

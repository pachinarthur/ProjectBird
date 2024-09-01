using UnityEngine;
using UnityEngine.UI;

public class ButtonQuitUI : MonoBehaviour
{
    [SerializeField] Button exitButton = null;
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
        exitButton.onClick.AddListener(QuitGame);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

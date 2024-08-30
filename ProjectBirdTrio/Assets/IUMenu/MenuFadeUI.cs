using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFadeUI : MonoBehaviour
{
    [SerializeField] float fadeDuration = 5f;

    public IEnumerator FadeCanvaGroup(CanvasGroup _canvasGroup,float _start,float _end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log("elapsedTime : " + elapsedTime);
            _canvasGroup.alpha = Mathf.Lerp(_start, _end ,elapsedTime / fadeDuration);
            yield return null;
        }
        _canvasGroup.alpha = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

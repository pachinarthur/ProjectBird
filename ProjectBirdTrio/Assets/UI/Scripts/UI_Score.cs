using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score = null;
    [SerializeField] GarbageManager managerRef = null;
    // Start is called before the first frame update
    void Start()
    {
        managerRef = FindObjectOfType<GarbageManager>();
        score.text = managerRef.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = managerRef.Score.ToString();
    }
}

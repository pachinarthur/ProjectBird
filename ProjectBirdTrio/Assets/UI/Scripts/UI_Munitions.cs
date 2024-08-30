using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UI_Munitions : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI score = null;
[SerializeField] PlayerSeedMechanics seedRef = null;
// Start is called before the first frame update
void Start()
{
        seedRef = FindObjectOfType<PlayerSeedMechanics>();
    score.text = seedRef.PoopMeter.ToString();
}

// Update is called once per frame
void Update()
{
    score.text = seedRef.PoopMeter.ToString();
    }
}

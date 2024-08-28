using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeedMechanics : MonoBehaviour
{
    [SerializeField] float currentTime = 0;
    [SerializeField] float maxTime = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale == Vector3.zero)
        {
            print("coucou");
            currentTime = Timer(currentTime, maxTime);
        }

    }
    float Timer(float _currentTime, float _maxTime)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _maxTime)
        {
            transform.localScale = Vector3.one;
            _currentTime = 0;
        }
        return _currentTime;
    }
}

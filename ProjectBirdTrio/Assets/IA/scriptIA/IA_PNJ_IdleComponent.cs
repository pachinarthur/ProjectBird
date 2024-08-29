using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_PNJ_IdleComponent : MonoBehaviour
{
    public event Action OnTimerElapsed = null;
    [SerializeField] float timeMin = 0.5f, timeMax = 3, waitinTime = 0, currenttime = 0;
    [SerializeField] bool start = false;
    // Start is called before the first frame update
    private void Start()
    {
        OnTimerElapsed += ResetTime;
    }

    private void Update()
    {
        if (start)
        {
            currenttime = UpdateTime(currenttime, waitinTime);
        }
    }

    float UpdateTime(float _time, float _timeMax)
    {
        _time += Time.deltaTime;
        if (_time >= _timeMax)
        {
            OnTimerElapsed?.Invoke();
            return 0;
        }
        return _time;
    }

    public void StartTime()
    {
        start = true;
    }

    public void InitTime()
    {
        waitinTime = UnityEngine.Random.Range(timeMin, timeMax);
    }

    void ResetTime()
    {
        start = false;
        currenttime = 0;
    }
}

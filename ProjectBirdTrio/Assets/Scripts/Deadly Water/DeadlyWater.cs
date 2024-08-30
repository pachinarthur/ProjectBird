using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyWater : MonoBehaviour
{
    [SerializeField] Player playerRef = null;
    [SerializeField] float currentTime = 0, maxTime = 10;
    [SerializeField] bool deathTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(deathTimer == true) currentTime = DeathTimer(currentTime, maxTime);
    }
    private void OnTriggerEnter(Collider _other)
    {
        playerRef = _other.GetComponent<Player>();
        if(playerRef != null)
        {
            deathTimer = true;
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        playerRef = null;
        deathTimer = false;
        currentTime = 0;
    }
    float DeathTimer(float _current, float _max)
    {
        _current += Time.deltaTime;
        if (_current > _max)
            {
            Destroy(playerRef.gameObject);
            playerRef = null;
            deathTimer = false;
            }

        return _current;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_PNJ_PatrolComponent : MonoBehaviour
{
    public event Action<Vector3> OnPatrolLocationFound;
    [SerializeField] Vector3 targetLocation = Vector3.zero;
    [SerializeField] float range = 10;
    [SerializeField] IA_PNJ_Brain brain = null;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
       brain = GetComponent<IA_PNJ_Brain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindRandomLocationInRange()
    {
        Vector2 _pos = UnityEngine.Random.insideUnitCircle;
        targetLocation = transform.position + new Vector3(_pos.x, 0, _pos.y) * range;
        bool isInZone = brain.Zone.IsPositionInsideZone(targetLocation);
        if (!isInZone)
        {
            FindRandomLocationInRange();
        }
        OnPatrolLocationFound?.Invoke(targetLocation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetLocation, .5f);
        Gizmos.color = Color.white;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IA_PNJ_MovementComponent : MonoBehaviour
{
    public event Action OnTargetReached = null;
    [SerializeField] float moveSpeed = 10, rotateSpeed = 50;
    [SerializeField] bool canMove = false, moveZone =false;
    [SerializeField] Vector3 patrolLocation = Vector3.zero;
    [SerializeField] Vector3 zoneLocation = Vector3.zero;

    public bool IsAtLocation
    {
        get
        {
            Vector3 _otherPos = (moveZone) ? zoneLocation : patrolLocation;
            return Vector3.Distance(transform.position, _otherPos) < .5f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
        RotateTo();
    }

    void MoveTo()
    {
        if (!canMove) return;
        Vector3 _otherPos = (moveZone) ? zoneLocation : patrolLocation;
        transform.position = Vector3.MoveTowards(transform.position, _otherPos, Time.deltaTime * moveSpeed);
        if (IsAtLocation)
            OnTargetReached?.Invoke();
    }

    void RotateTo()
    {
        if (!canMove) return;
        Vector3 _look = patrolLocation - transform.position;
        if (_look == Vector3.zero) return;
        Quaternion _rot = Quaternion.LookRotation(_look);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotateSpeed);
    }

    public void SetPatrolLocation(Vector3 _pos)
    {
        patrolLocation = _pos;
        SetCanMove(true);
    }

    public void SetZoneLocation(Vector3 _pos)
    {
        zoneLocation = _pos;
        SetCanMove(true);
    }

    public void SetCanMove(bool _value)
    {
        canMove = _value;
    }

    public void SetMoveZone(bool _val)
    {
        moveZone = _val; 
    }

    public bool GetMoveZone()
    {
        return moveZone;
    }

   public void GoToZone()
    {
        //if (_zone == null) return;
        //SetPatrolLocation(_zone.transform.position);
    }

}

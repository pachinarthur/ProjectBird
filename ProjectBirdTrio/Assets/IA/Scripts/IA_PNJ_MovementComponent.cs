using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IA_PNJ_MovementComponent : MonoBehaviour
{
    public event Action OnTargetReached = null;
    [SerializeField] float moveSpeed = 10, rotateSpeed = 50;
    [SerializeField] bool canMove = false;
    [SerializeField] Vector3 patrolLocation = Vector3.zero;

    public bool IsAtLocation
    {
        get
        {
            return Vector3.Distance(transform.position, patrolLocation) < .5f;
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
        transform.position = Vector3.MoveTowards(transform.position, patrolLocation, Time.deltaTime * moveSpeed);
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
        Debug.Log("canMove");
    }

    public void SetCanMove(bool _value)
    {
        Debug.Log("CanMove");
        canMove = _value;
    }
}

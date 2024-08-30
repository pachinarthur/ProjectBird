using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Cinemachine.CinemachineTargetGroup;
using static UnityEngine.GraphicsBuffer;

public class IA_PNJ_MovementComponent : MonoBehaviour
{
    public event Action OnTargetReached = null;
    [SerializeField] float moveSpeed = 10, rotateSpeed = 50;
    [SerializeField] bool canMove = false, moveZone =false;
    [SerializeField] Vector3 patrolLocation = Vector3.zero;
    [SerializeField] Vector3 zoneLocation = Vector3.zero;

    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] List<Vector3> path = new List<Vector3>();
    [SerializeField] int pathIndex = 0;

    public bool IsAtLocation
    {
        get
        {
            if ( path.Count < 1) return true;
            return Vector3.Distance(transform.position, path[pathIndex]) < .5f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Init()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
        RotateTo();
    }



    void UpdatePath()
    {
        Vector3 _target = (moveZone) ? zoneLocation : patrolLocation;
        if (!agent) return;
        NavMeshPath _path = new NavMeshPath();
        if (!agent.CalculatePath(_target, _path)) return;
        path.Clear();
        path = _path.corners.ToList();
        pathIndex = 0;
    }

    void UpdatePathIndex()
    {
        if (path.Count < 1) return;
        if (pathIndex + 1 >= path.Count) return;
        pathIndex++;
    }

    void MoveTo()
    {
        if (!canMove || path.Count < 1) return;
        transform.position = Vector3.MoveTowards(transform.position, path[pathIndex], Time.deltaTime * moveSpeed);
        if (IsAtLocation)
        {
            if (pathIndex >= path.Count - 1)
            {
                OnTargetReached?.Invoke();
                return;
            }
            UpdatePathIndex();
        }
    }

    void RotateTo()
    {
        if (!canMove || path.Count < 1 || IsAtLocation) return;
        Vector3 _look = path[pathIndex] - transform.position;
        if (_look == Vector3.zero) return;
        Quaternion _rot = Quaternion.LookRotation(_look);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotateSpeed);
    }

    public void SetPatrolLocation(Vector3 _pos)
    {
        patrolLocation = _pos;
        SetCanMove(true);
        UpdatePath();
    }

    public void SetZoneLocation(Vector3 _pos)
    {
        zoneLocation = _pos;
        SetCanMove(true);
        UpdatePath();
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

    private void OnDrawGizmos()
    {
        if (path.Count < 1) return;
        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(path[i], 0.5f);
            if (i + 1 >= path.Count - 1) return;
            Gizmos.DrawLine(path[i], path[i + 1]);

        }
    }

}

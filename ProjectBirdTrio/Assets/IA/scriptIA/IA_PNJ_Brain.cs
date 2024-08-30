using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(IA_PNJ_MovementComponent), typeof(IA_PNJ_PatrolComponent))]
[RequireComponent(typeof(IA_PNJ_ThrowComponent),typeof(IA_PNJ_IdleComponent),typeof(IA_PNJ_ZoneComponent))]
public class IA_PNJ_Brain : MonoBehaviour
{
    public static readonly int IDLE_DONE = Animator.StringToHash("idleDone");
    public static readonly int THROW_DONE = Animator.StringToHash("throwDone");
    public static readonly int PATROL_DONE = Animator.StringToHash("patrolDone");
    public static readonly int GOZONE_DONE = Animator.StringToHash("GoZoneDone");
    [SerializeField] Animator fsm = null;
    [SerializeField] IA_PNJ_MovementComponent movement = null;
    [SerializeField] IA_PNJ_PatrolComponent patrol = null;
    [SerializeField] IA_PNJ_IdleComponent idle = null;
    [SerializeField] IA_PNJ_ThrowComponent throwComponent = null;
    [SerializeField] IA_PNJ_ZoneComponent zone = null;
    [SerializeField] Color debugColor = Color.white;
    IA_PNJ_BaseBehaviour[] behaviours = new IA_PNJ_BaseBehaviour[] { };



    public IA_PNJ_ThrowComponent ThrowComponent => throwComponent;
    public IA_PNJ_PatrolComponent Patrol => patrol;
    public IA_PNJ_MovementComponent Movement => movement;
    public IA_PNJ_IdleComponent Idle => idle;   
    public IA_PNJ_ZoneComponent Zone => zone;
    public Animator FSM => fsm;

    public bool isValid => fsm && idle && movement && throwComponent && patrol && zone;


    // Start is called before the first frame update
    void Start()
    {
        InitBehaviours();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        
        movement = GetComponent<IA_PNJ_MovementComponent>();
        patrol = GetComponent<IA_PNJ_PatrolComponent>();
        throwComponent = GetComponent<IA_PNJ_ThrowComponent>();
        idle = GetComponent<IA_PNJ_IdleComponent>();
        zone = GetComponent<IA_PNJ_ZoneComponent>();
        if (!isValid) return;

        patrol.OnPatrolLocationFound += movement.SetPatrolLocation;

        movement.OnTargetReached += () =>
        {
            if (movement.GetMoveZone())
            {
                fsm.SetBool(GOZONE_DONE, true);
                fsm.SetBool(IDLE_DONE, false);
                movement.SetCanMove(false);
                movement.SetMoveZone(false);
            }
            else
            {
                fsm.SetBool(PATROL_DONE, true);
                fsm.SetBool(IDLE_DONE, false);
            }

        };

        idle.OnTimeThrow += () =>
        {
            if (throwComponent.GetHasThrow()) return;
            fsm.SetBool(IDLE_DONE, true);
            throwComponent.SetHasThrow(true);
            Debug.Log("Throw");
        };

        throwComponent.OnTrashThrown += () =>
        {
            fsm.SetBool(THROW_DONE, true);
            Debug.Log("Trash Thrown");
        };

        zone.OnZoneFound += (_zone) =>
        {
            movement.SetZoneLocation(new Vector3(_zone.transform.position.x,transform.position.y,_zone.transform.position.z));
        };

        idle.OnTimerElapsed += () =>
        {
            fsm.SetBool(IDLE_DONE, true);
            fsm.SetBool(PATROL_DONE, false);
            fsm.SetBool(THROW_DONE, false);
            Debug.Log("Idle Done");

        };

        

    }

    void InitBehaviours()
    {
        
        fsm = GetComponent<Animator>();
        fsm.enabled = true;
        behaviours = fsm.GetBehaviours<IA_PNJ_BaseBehaviour>();
        int _size = behaviours.Length;
        for (int i = 0; i < _size; i++)
        {
            behaviours[i].Init(this);
        }
    }

    public void SetColor(Color _color)
    {
        debugColor = _color;
    }


    private void OnDrawGizmos()
    {
       
    }
}

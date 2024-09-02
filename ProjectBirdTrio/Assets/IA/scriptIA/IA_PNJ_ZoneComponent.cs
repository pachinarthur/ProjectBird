using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IA_PNJ_ZoneComponent : MonoBehaviour
{
    public event Action<Zone_ZoneBase> OnZoneFound = null;
    public List<Zone_ZoneBase> zoneIslands = new List<Zone_ZoneBase>();
    [SerializeField] Zone_ZoneBase zoneToGo = null;
    public Zone_ZoneBase ZoneToGo => zoneToGo;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
       zoneIslands = FindObjectsOfType<Zone_ZoneBase>().ToList();
    }

    public void GetRandomZone()
    {
        if (zoneIslands.Count == 0) return;
        int index = UnityEngine.Random.Range(0, zoneIslands.Count);
        zoneToGo = zoneIslands[index];
        if (IsZoneFull(zoneToGo)) return;
        SetZoneToGo(zoneToGo);
        OnZoneFound?.Invoke(zoneToGo);
    }

    public void SetZoneToGo(Zone_ZoneBase zone)
    {
        zoneToGo = zone;
    }

    public Zone_ZoneBase GetZoneToGo()
    {
        return zoneToGo;
    }

    public bool IsZoneFull(Zone_ZoneBase _zone)
    {
        if (_zone == null) return false;
        return _zone.IsFull();
    }

    public bool IsPositionInsideZone(Vector3 position)
    {
        return zoneToGo.GetComponent<Collider>().bounds.Contains(position);
    }



}

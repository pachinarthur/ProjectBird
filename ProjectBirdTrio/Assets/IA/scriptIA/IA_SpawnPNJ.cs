using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_SpawnPNJ : MonoBehaviour
{
    [SerializeField] float timespawn = 10;
    [SerializeField] List<GameObject> pnjToSpawn = null;
    [SerializeField] GameObject pnjSelectedToSpawn = null;
    [SerializeField] Vector3 LocToSpawn = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnPnj), 1, timespawn);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SelectPNJToSpawn()
    {
        int _rand = Random.Range(0, pnjToSpawn.Count);
        pnjSelectedToSpawn = pnjToSpawn[_rand];
    }

    void SpawnPnj()
    {
        SelectPNJToSpawn();
        GameObject _pnj = Instantiate(pnjSelectedToSpawn);
        _pnj.gameObject.transform.position = LocToSpawn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorTentCreation : MonoBehaviour
{
    [SerializeField] List<Mesh> tent = null;
    [SerializeField] Mesh currentMesh = null;
    [SerializeField] MeshFilter meshFilter = null;
    [SerializeField] MeshRenderer meshRenderer = null;
    [SerializeField] Material materialTentColor = null;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        currentMesh = tent[Random.Range(0, tent.Count)];
        meshFilter.mesh = currentMesh;
        meshRenderer.material = materialTentColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

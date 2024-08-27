using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomPlacementScript : MonoBehaviour
{
    [SerializeField] Color color = Color.white;
    [SerializeField] float debugSize = 0.1f, minRotationRange = -360, maxRotationRange = 350, minSize = 1, maxSize = 1;
    [SerializeField] List<Mesh> ItemToPlace = null;
    [SerializeField] Mesh currentMesh = null;
    [SerializeField] MeshFilter meshFilter = null;
    [SerializeField] MeshRenderer meshRenderer = null;
    [SerializeField] Material materialTentColor = null;
    // Start is called before the first frame update
    void Start()
    {
        init();
        ChooseBetweenTrees();
        RandomTreeRotation();
        RandomSize();
    }
    void init()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void ChooseBetweenTrees()
    {
        currentMesh = ItemToPlace[Random.Range(0, ItemToPlace.Count)];
        meshFilter.mesh = currentMesh;
    }
    void RandomTreeRotation()
    {
        Vector3 _rotation = transform.eulerAngles;
        _rotation.y = _rotation.y + Random.Range(-360, 360);
        transform.eulerAngles = _rotation;
        print(gameObject.transform.rotation);
    }
    void RandomSize()
    {
        Vector3 _scale;
        _scale.x = Random.Range(minSize, maxSize);
        _scale.y = _scale.x;
        _scale.z = _scale.x;
        transform.localScale = _scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(gameObject.transform.position, debugSize);
    }
}

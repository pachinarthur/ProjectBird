using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoopBird : MonoBehaviour
{
    [SerializeField] float godown = 0.5f;
    [SerializeField] GameObject testground = null;
    [SerializeField] float spawnYvalue = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileGoDown();
    }
    void ProjectileGoDown()
    {
        Vector3 _goDown = transform.position;
        _goDown.y -= godown;
        transform.position = _goDown;
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 6)
        {
            Instantiate(testground, transform.position + new Vector3(0, spawnYvalue, 0), Quaternion.identity);
            Destroy(gameObject);
            print("egg");
        }
    }
}

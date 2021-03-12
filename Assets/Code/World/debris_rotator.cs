using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debris_rotator : MonoBehaviour
{
    public GameObject target;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target) transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}

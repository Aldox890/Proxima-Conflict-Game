using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public GameObject target;
    public float speed = 5;
    public Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(target.transform.position, axis, speed * Time.deltaTime);
    }
}

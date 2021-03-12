using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint_Movement : MonoBehaviour
{
    public GameObject next;
    public float speed = 1f;
    public float rotationSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = Vector3.MoveTowards(transform.position, next.transform.position, speed * Time.deltaTime);
        Vector3 targetDirection = next.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if(Vector3.Distance(transform.position, next.transform.position) > speed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        
        //if(Vector3.Distance(transform.position, next.transform.position)< 5f)
        else
        {
            next = next.GetComponent<waypoint>().next;
        }
    }   
}

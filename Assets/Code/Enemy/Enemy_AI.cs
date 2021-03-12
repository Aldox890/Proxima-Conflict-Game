using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Collections.Specialized;

public class Enemy_AI : NetworkBehaviour
{
    [SerializeField] float rayCastOffset = 1.5f;
    [SerializeField] float detectionDistance = 5f;

    public GameObject test;
    public GameObject radarObject;
    public GameObject target;
    public float rotationalDamp = 5f;
    float movementSpeed = 5f;
    public float velocity = 10;
    public float forwardOffset = 1f;
    public float stopDistance = 5f;
    public float backDistance = 2f;
    public GameObject aimRotator;

    public GameObject projectile;
    public float timer = 0;

    private Radar radar;

    bool start = false;

    private float posY;
    // Start is called before the first frame update
    void Start()
    {
        //if (base.isServer) GetComponent<Rigidbody>().isKinematic = true;
        posY = transform.position.y;
        radar = radarObject.GetComponent<Radar>();
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        timer += Time.deltaTime;
        if (radar.target != null)
        {
            newPathfind();
            newLookAt();
            //pathfind();
            //if (aimRotator != null) aimRotator.transform.rotation = Quaternion.LookRotation(aimRotator.transform.position - radar.target.transform.position);
        }

    }

    void newPathfind()
    {
        RaycastHit hit;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;

        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance) && hit.transform.gameObject.layer == 13)
        {
            GetComponent<Rigidbody>().AddForce(transform.right * velocity * Time.deltaTime * 50f);
            Debug.Log("Right");
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance) && hit.transform.gameObject.layer == 13)
        {
            GetComponent<Rigidbody>().AddForce(transform.right * velocity * Time.deltaTime * -50f);
            Debug.Log("Left");
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, stopDistance))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, backDistance))
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * velocity * Time.deltaTime * -50f);
            }
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * velocity * Time.deltaTime * 50f);
        }

        if (GetComponent<Rigidbody>().velocity.magnitude > velocity)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * velocity;
        }
    }

    void newLookAt()
    {
        Quaternion targetRotation = Quaternion.LookRotation(radar.target.transform.position - transform.position + radar.target.GetComponent<Rigidbody>().velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationalDamp);
    }

    void turn()
    {
        Vector3 pos = radar.target.transform.position - transform.position + radar.target.GetComponent<Rigidbody>().velocity;
        pos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
        //transform.RotateAround(radar.target.transform.position, Vector3.up, velocity * Time.deltaTime);
    }

    void Shoot()
    {
        RaycastHit hit;
        float distance = Vector3.Distance(transform.position, radar.target.transform.position);
        if (distance <= 10f)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                if (timer >= 0.4f) {
                    GameObject proj = Instantiate(projectile, new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z), Quaternion.LookRotation(transform.forward)); ;
                    proj.transform.rotation = transform.rotation;
                    proj.GetComponent<Projectile_Physics>().shooter = gameObject;

                    proj = Instantiate(projectile, new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z), Quaternion.LookRotation(transform.forward)); ;
                    proj.transform.rotation = transform.rotation;
                    proj.GetComponent<Projectile_Physics>().shooter = gameObject;

                    timer = 0;
                }
            }
        }
    }

    void Move()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * velocity);
        if (GetComponent<Rigidbody>().velocity.magnitude > velocity)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * velocity;
        }
        
    }

    void pathfind()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 forward = transform.position + transform.forward * forwardOffset;

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);

        Move();

        if (Physics.Raycast(transform.position, (transform.position - radar.target.transform.position) * -1, out hit))
        { 
            
            if (hit.transform.gameObject == radar.target && Vector3.Distance(transform.position, radar.target.transform.position) < 10f)
            {
                Debug.Log(Vector3.Distance(transform.position, target.transform.position));
                GetComponent<Rigidbody>().AddForce(transform.right * velocity);
                turn();
            }
            if(hit.transform.gameObject != radar.target)
            {
                Debug.Log("test");
                GetComponent<Rigidbody>().AddForce(transform.right * velocity);
                turn();
            }
            //else
            //{
                        
                if (Physics.Raycast(left, transform.forward, out hit, detectionDistance) && hit.transform.gameObject.layer == 13)
                {
                    GetComponent<Rigidbody>().AddForce(transform.right * velocity);
                    raycastOffset += Vector3.up;
                }
                else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance) && hit.transform.gameObject.layer == 13)
                {
                    GetComponent<Rigidbody>().AddForce(transform.right * -velocity);
                    raycastOffset -= Vector3.up;
                }

                if (raycastOffset != Vector3.zero)
                {
                    Vector3 m_EulerAngleVelocity = raycastOffset;
                    Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
                    transform.rotation = GetComponent<Rigidbody>().rotation * deltaRotation;
                }
                else turn();
            //}
        }

    }
}

using UnityEngine;
using Mirror;
using System;

public class Ship_Movem : NetworkBehaviour
{
    // Start is called before the first frame update

    public float velocity = 10;
    public float rotationSpeed = 5;
    public float tiltAngle = 10f;
    public float topSpeed = 30f;

    public GameObject shipModel;
    private GameObject controller;

    private Rigidbody rigidbody;

    void Start()
    {

        if (base.hasAuthority)
        {
            controller = GameObject.Find("GameController");
            controller.GetComponent<GameController>().PlayerShip = gameObject;
            controller.GetComponent<GameController>().StartCameraFollow();
        }
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * -6.0f, ForceMode.Impulse);
    }

    [Command]
    public void CmdLookAt(Quaternion rotation)
    {
        rigidbody.MoveRotation(rotation);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
        //RpcLookAt(rotation);
    }

    [ClientRpc]
    public void RpcLookAt(Quaternion rotation)
    {
        if (base.hasAuthority) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }


    public void findLookAt()
    {
        if(Camera.main != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane hPlane = new Plane(transform.up, transform.position);
            float distance = 700;

            Quaternion rotationLookAt = Quaternion.LookRotation(transform.position - ray.GetPoint(distance));

            Vector3 newAngle = rotationLookAt.eulerAngles;
            Vector3 oldAngle = transform.eulerAngles;

            Quaternion destRotation = Quaternion.Euler(new Vector3(oldAngle.x, newAngle.y, oldAngle.z));

            if (hPlane.Raycast(ray, out distance))
            {
                var tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
                var target = Quaternion.Euler(0, 0, tiltAroundZ);
                shipModel.transform.localRotation = Quaternion.Slerp(shipModel.transform.localRotation, target, Time.deltaTime * 5f);

                rigidbody.MoveRotation(destRotation);
                //transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, 5f * Time.deltaTime);
                CmdLookAt(destRotation);
            }
        }
    }
    float t = 0;

    void Update()
    {
        if (!base.hasAuthority) return;
        findLookAt();

        if (!base.hasAuthority) return;
        float vr = Input.GetAxis("Vertical");
        float hr = Input.GetAxis("Horizontal");
        /*
        rigidbody.AddForce(transform.forward * vr * velocity);
        rigidbody.AddForce(transform.right * hr * -velocity);

        if (rigidbody.velocity.magnitude > topSpeed) {
            rigidbody.velocity = rigidbody.velocity.normalized * topSpeed;
        }
        */
        CmdMove(hr, vr);
    }

    [Command]
    public void CmdMove(float hr, float vr)
    {
        if (base.hasAuthority && !base.isServer) return;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * vr * velocity * Time.deltaTime * 50f);
        rigidbody.AddForce(transform.right * hr * -velocity * Time.deltaTime * 50f);

        /*
        if (rigidbody.velocity.magnitude > topSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * topSpeed;
        }
        */
    }
}

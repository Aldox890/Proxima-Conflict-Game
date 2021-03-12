using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Teleport : NetworkBehaviour
{
    private GameObject rootObj;
    private Rigidbody rb;
    private bool isLocal = false;
    bool newSkill = true;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.SendMessage("destroySkill");
        newSkill = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("left shift"))
        {
            CmdTeleport();
        }
    }

    [Command]
    void CmdTeleport()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * -1, out hit, 10f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            transform.position = hit.point + (transform.TransformDirection(Vector3.forward) * 2f);
        }
        else
        {
            transform.position = transform.position + (transform.TransformDirection(Vector3.forward) * -10f);
        }
    }

    public void destroySkill()
    {
        if(!newSkill) Destroy(this);
    }
}

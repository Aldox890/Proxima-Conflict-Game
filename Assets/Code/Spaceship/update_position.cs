using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class update_position : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (base.isServer) InvokeRepeating("updatePos", 0.01f, 0.01f);
    }

    [Server]
    void updatePos()
    {
        RpcsetPosition(transform.position.x, transform.position.z, transform.rotation);
    }

    float x;
    float z;
    Quaternion rotation;
    [ClientRpc]
    void RpcsetPosition(float xp, float zp, Quaternion r)
    {
        x = xp;
        z = zp;
        rotation = r;
        Vector3 velocity = Vector3.zero;
        //rigidbody.AddForce(new Vector3(x, transform.position.y, z));
        //transform.rotation = r;
    }

    float t = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!base.isServer)
        {
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, transform.position.y, z), Time.deltaTime * 10f);
            t += Time.deltaTime / 0.01f;
            transform.rotation = rotation;
            //transform.position = new Vector3(x, 0, z);
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, transform.position.y, z), t);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Radar : NetworkBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    [Server]
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.transform.root.gameObject.layer == 8)
        {
            if(target == null) target = c.gameObject.transform.root.gameObject;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Looter : NetworkBehaviour
{
    public Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Server]
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == 15) inv.newLoot(c);
    }
}

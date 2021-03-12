using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Entity_Spawner : NetworkBehaviour
{
    public GameObject entity;
    public float timer = 10f;
    
    private GameObject obj = null;
    private float death = 0;

    void Start()
    {
        death = -timer;
    }

    private bool isDead = false;
    // Update is called once per frame
    [Server]
    void Update()
    {
        if(obj == null && !isDead)
        {
            death = Time.time;
            isDead = true;
        }

        if(Time.time > death + timer && isDead)
        {
            obj = Instantiate(entity, transform.position, entity.transform.rotation);
            NetworkServer.Spawn(obj);
            isDead = false;
        }
    }
}

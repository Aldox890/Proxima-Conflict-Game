using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hotspot_Generator : NetworkBehaviour
{
    public int size = 10;
    public int nEnemy = 5;

    public GameObject[] spawnedEntity;

    // Start is called before the first frame update
    [Server]
    void Start()
    {

        spawnedEntity = new GameObject[nEnemy];
        Object[] enemies = Resources.LoadAll("Rand_Enemies");
        int nProps = enemies.Length;

        int rangeX = size;
        int rangeZ = size;
        int rangeY = 0;


        for (int i = 0; i < nEnemy; i++)
        {
            Vector3 position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), Random.Range(transform.position.y - rangeY, 0), Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));
            GameObject obj = enemies[Random.Range(0, nProps - 1)] as GameObject;
            //Debug.Log(obj.name);
            spawnedEntity[i] = Instantiate(obj, position, transform.rotation);
            NetworkServer.Spawn(spawnedEntity[i]);
        }
    }

    public void deactivate()
    {
        foreach (GameObject go in spawnedEntity)
        {
            if (go != null) go.SetActive(false);
        }
    }

    public void activate()
    {
        
        foreach (GameObject go in spawnedEntity)
        {
            if (go != null) go.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

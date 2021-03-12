using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Chunk_Generation : NetworkBehaviour
{
    public int nSpawn = 50;
    public int nEnemy = 20;
    public int nHotspot = 1;

    public int size = 200;

    public GameObject content;

    private Object[] props;
    private int nProps;

    private bool done = false;

    private int rangeX;
    private int rangeY;
    private int rangeZ;
    // Start is called before the first frame update
    [Server]
    void Start()
    {
        rangeX = size;
        rangeZ = size;
        rangeY = 0;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(rangeX * 2 + 100, 5, rangeZ * 2 + 100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnAll()
    {
        props = Resources.LoadAll("Rand_Props");
        nProps = props.Length;

        for (int i = 0; i < nSpawn; i++)
        {
            Vector3 position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), Random.Range(transform.position.y - rangeY, 0), Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));
            GameObject obj = props[Random.Range(0, nProps - 1)] as GameObject;
            //Debug.Log(obj.name);
            obj = Instantiate(obj, position, transform.rotation);
            obj.transform.parent = content.transform;
            NetworkServer.Spawn(obj);
        }

        Object[] enemies = Resources.LoadAll("Rand_Enemies");
        nProps = enemies.Length;
        for (int i = 0; i < nEnemy; i++)
        {
            Vector3 position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), Random.Range(transform.position.y - rangeY, 0), Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));
            GameObject obj = enemies[Random.Range(0, nProps - 1)] as GameObject;
            //Debug.Log(obj.name);
            obj = Instantiate(obj, position, transform.rotation);
            obj.transform.parent = content.transform;
            NetworkServer.Spawn(obj);
        }

        done = true;
    }

    int pCount = 0;

    [Server]
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.root.gameObject.layer == 8)
        {
            if (done)
            {
                pCount++;

                if(pCount > 0)
                {
                    content.SetActive(true);
                }
            }
            else spawnAll();
        }
    }

    [Server]
    void OnTriggerExit(Collider c)
    {
        if (c.transform.root.gameObject.layer == 8)
        {
            pCount--;

            if (pCount <= 0)
            {
                pCount = 0;
                content.SetActive(false);
            }
        }
        else spawnAll();
    }
}

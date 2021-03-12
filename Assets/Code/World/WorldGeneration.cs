using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WorldGeneration : NetworkBehaviour
{
    public int nSpawn = 50;
    public int nEnemy = 20;
    public int nHotspot = 1;

    public int size = 200;

    public GameObject hotspotGenerator;

    private Object[] props;
    private int nProps;

    private GameObject[] spawnedProps;
    private GameObject[] spawnedEntity;
    private GameObject[] spawnedHotspot;
    
    public bool done = false;

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

        if (numPlayers >0) {
            spawnAll();
        }


    }

    private void spawnAll()
    {
        spawnedProps = new GameObject[nSpawn];
        spawnedEntity = new GameObject[nEnemy];
        spawnedHotspot = new GameObject[nHotspot];
        props = Resources.LoadAll("Rand_Props");
        nProps = props.Length;

        for (int i = 0; i < nSpawn; i++)
        {
            Vector3 position;
            do
            {
              position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), Random.Range(transform.position.y - rangeY, 0), Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));
            } while (Physics.CheckSphere(position, 5f));

            GameObject obj = props[Random.Range(0, nProps -1)] as GameObject;
            //Debug.Log(obj.name);
            spawnedProps[i] = Instantiate(obj, position, transform.rotation);
            NetworkServer.Spawn(spawnedProps[i]);
        }

        Object[] enemies = Resources.LoadAll("Rand_Enemies");
        nProps = enemies.Length;
        for (int i = 0; i < nEnemy; i++)
        {
            Vector3 position;
            do
            {
                position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), Random.Range(transform.position.y - rangeY, 0), Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));
            } while (Physics.CheckSphere(position, 2f));

            GameObject obj = enemies[Random.Range(0, nProps - 1)] as GameObject;
            //Debug.Log(obj.name);
            spawnedEntity[i] = Instantiate(obj, position, transform.rotation);
            NetworkServer.Spawn(spawnedEntity[i]);
        }
        
        for (int i = 0; i < nHotspot; i++)
        {
            Vector3 position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), Random.Range(transform.position.y - rangeY, 0), Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));
            spawnedHotspot[i] = Instantiate(hotspotGenerator, position, transform.rotation);
            //NetworkServer.Spawn(spawnedEntity[i]);
        }
        
        done = true;
    }

    private int numPlayers;
    [Server]
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.root.gameObject.layer == 8)
        {
            numPlayers++;
            if (done)
            {
                foreach (GameObject go in spawnedProps)
                {
                    if (go != null) go.SetActive(true);
                }

                foreach (GameObject go in spawnedEntity)
                {
                    if (go != null) go.SetActive(true);
                }

                foreach (GameObject go in spawnedHotspot)
                {
                    if (go != null)
                    {
                        go.SetActive(true);
                        go.GetComponent<Hotspot_Generator>().activate();
                    }
                }
            }
            else spawnAll();
        }
    }

    [Server]
    void OnTriggerExit(Collider c)
    {
        if (c.transform.root.gameObject.layer == 8) {
            numPlayers--;
            if (numPlayers <= 0)
            {
                foreach (GameObject go in spawnedProps)
                {
                    if (go != null) go.SetActive(false);
                }

                foreach (GameObject go in spawnedEntity)
                {
                    if (go != null) go.SetActive(false);
                }

                foreach (GameObject go in spawnedHotspot)
                {
                    if (go != null)
                    {
                        go.GetComponent<Hotspot_Generator>().deactivate();
                        go.SetActive(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Color c = Color.red;
        if (done) c = Color.yellow;
        if (numPlayers > 0) c = Color.green;
        Vector3 start = new Vector3(transform.position.x - size, 0, transform.position.z - size);
        Vector3 dest = new Vector3(transform.position.x - size, 0, transform.position.z + size);
        Debug.DrawLine(start, dest, c);

        start = new Vector3(transform.position.x - size, 0, transform.position.z - size);
        dest = new Vector3(transform.position.x + size, 0, transform.position.z - size);
        Debug.DrawLine(start, dest, c);

        start = new Vector3(transform.position.x + size, 0, transform.position.z - size);
        dest = new Vector3(transform.position.x + size, 0, transform.position.z + size);
        Debug.DrawLine(start, dest, c);

        start = new Vector3(transform.position.x + size, 0, transform.position.z + size);
        dest = new Vector3(transform.position.x - size, 0, transform.position.z + size);
        Debug.DrawLine(start, dest, c);
        */

    }
}

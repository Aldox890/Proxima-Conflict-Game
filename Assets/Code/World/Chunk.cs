using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Chunk : NetworkBehaviour
{
    public int nSpawn = 50;

    public int size = 200;

    public int nEnemy = 5;
    public float respawnTime = 1f;

    private Object[] props;
    private int nProps;

    private int rangeX;
    private int rangeY;
    private int rangeZ;

    public GameObject childObj;
    void Start()
    {
        enemyList = new List<GameObject>();

        rangeX = size;
        rangeZ = size;
        rangeY = 0;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(rangeX * 2 + 100, 5, rangeZ * 2 + 100);

        if (base.isServer) InvokeRepeating("spawnEnemy", 5f, respawnTime);

        //spawnAll();
    }

    private List<GameObject> enemyList;

    void spawnEnemy()
    {
        enemyList.RemoveAll(item => item == null);

        if (childObj.active && enemyList.Count < nEnemy) {
            Object[] enemies = Resources.LoadAll("Rand_Enemies");
            Vector3 position;
            position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), 0, Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));

            GameObject obj = enemies[Random.Range(0, nProps - 1)] as GameObject;
            GameObject temp = Instantiate(obj, position, transform.rotation);
            enemyList.Add(temp);
            NetworkServer.Spawn(temp);
        }
    }

    void spawnAll()
    {

        props = Resources.LoadAll("Rand_Props");
        nProps = props.Length;
        GameObject tempObj;

        for (int i = 0; i < nSpawn; i++)
        {
            Vector3 position;
            do
            {
                position = new Vector3(Random.Range(transform.position.x - rangeX, transform.position.x + rangeX + 1), Random.Range(transform.position.y - rangeY, 0), Random.Range(transform.position.z - rangeZ, transform.position.z + rangeZ + 1));
            } while (Physics.CheckSphere(position, 5f));

            GameObject obj = props[Random.Range(0, nProps - 1)] as GameObject;
            //Debug.Log(obj.name);
            tempObj = Instantiate(obj, position, transform.rotation);
            tempObj.transform.rotation = Random.rotation;
            tempObj.transform.parent = childObj.transform;
        }
        childObj.SetActive(false);
    }

    int nPlayers = 0;

    void Update()
    {
    }

    void OnTriggerEnter(Collider c)
    {

        if (c.transform.parent.parent.parent.gameObject.layer == 8)
        {
            if (base.isServer) {
                nPlayers++;
                childObj.SetActive(true);
            }
            else if(c.transform.parent.parent.parent.gameObject.GetComponent<Entity>().isLocalPlayer == true)
            {
                childObj.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.transform.parent.parent.parent.gameObject.layer == 8)
        {
            if (base.isServer)
            {
                nPlayers--;
                if(nPlayers <= 0)
                {
                    childObj.SetActive(false);
                    nPlayers = 0;
                }
            }
            else if (c.transform.parent.parent.parent.gameObject.GetComponent<Entity>().isLocalPlayer == true)
            {
                childObj.SetActive(false);
            }
        }
    }
}

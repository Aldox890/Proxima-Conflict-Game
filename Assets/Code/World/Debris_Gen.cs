using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris_Gen : MonoBehaviour
{
    public int nProps = 5;
    public int offset = 10;
    public string folder = "";

    public int type = 0;

    public int radius = 5;
    public Vector3 cube;

    Object[] props;
    // Start is called before the first frame update
    void Start()
    {
        props = Resources.LoadAll("Background_Props" + folder);
        if (type == 0) spawnSphere();
        else if (type == 1) spawnCube();
    }

    void spawnCube()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - cube.y - offset, transform.position.z);

        for (int i = 0; i < nProps; i++)
        {
            float x = Random.Range(-cube.x / 2f, cube.x / 2f);
            float y = Random.Range(-cube.y / 2f, cube.y / 2f);
            float z = Random.Range(-cube.z / 2f, cube.z / 2f);

            Vector3 position = new Vector3(transform.position.x + x , transform.position.y + y, transform.position.z + z);
            Quaternion rotation = Random.rotation;
            int r = Random.Range(0, props.Length);
            GameObject prop = (GameObject)Instantiate(props[r], position, rotation);
            prop.GetComponent<debris_rotator>().target = gameObject;
            prop.GetComponent<debris_rotator>().speed = Random.Range(-1, 1);
        }
    }

    void spawnSphere()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - radius - offset, transform.position.z);

        for (int i = 0; i < nProps; i++)
        {
            Vector3 position = Random.insideUnitSphere * radius;
            position = new Vector3(position.x + transform.position.x, position.y + transform.position.y, position.z + transform.position.z);
            Quaternion rotation = Random.rotation;
            int r = Random.Range(0, props.Length);
            GameObject prop = (GameObject)Instantiate(props[r], position, rotation);
            prop.GetComponent<debris_rotator>().target = gameObject;
            prop.GetComponent<debris_rotator>().speed = Random.Range(-1, 1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

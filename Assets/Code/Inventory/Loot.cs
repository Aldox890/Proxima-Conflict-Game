using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public string type;
    public int quantity;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 20f);
    }
}

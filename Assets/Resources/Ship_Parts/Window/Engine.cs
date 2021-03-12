using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Engine : NetworkBehaviour
{
    public string name = "null";

    public int type = 0;
    public int ability = 0;
    // Start is called before the first frame update
    private GameObject rootObj;
    private Rigidbody rb;
    private bool isLocal = false;
    void Start()
    {
        if(rootObj = transform.parent.parent.gameObject)
        {
            rb = rootObj.GetComponent<Rigidbody>();
            isLocal = rootObj.GetComponent<Ship_Controller>().isLocalPlayer;

            if (isLocal)
            {
                switch (ability)
                {
                    case 1:
                        rootObj.gameObject.AddComponent<Sprint>();
                        break;
                    case 2:
                        rootObj.gameObject.AddComponent<Teleport>();
                        break;
                }
            }
        }
    }
}

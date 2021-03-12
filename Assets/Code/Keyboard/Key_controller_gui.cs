using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_controller_gui : MonoBehaviour
{
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
        }
    }
}

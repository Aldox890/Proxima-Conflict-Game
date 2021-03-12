using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Object test;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SendMessage("destroyStrings");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

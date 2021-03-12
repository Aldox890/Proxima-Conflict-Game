using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    private GameObject rootObj;
    private Rigidbody rb;
    private bool isLocal = false;
    bool newSkill = true;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.SendMessage("destroySkill");
        newSkill = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("left shift"))
        {
            sprint();
        }
    }

    void sprint()
    {
        if (rb != null)
        {
            rb.AddForce(transform.forward * -10f, ForceMode.Impulse);
        }
    }

    public void destroySkill()
    {
        if(!newSkill) Destroy(this);
    }
}

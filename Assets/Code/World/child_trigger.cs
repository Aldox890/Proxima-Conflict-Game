using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider c)
    {
        gameObject.GetComponentInParent<Projectile_Physics>().OnTriggerEnter(c);
    }
}

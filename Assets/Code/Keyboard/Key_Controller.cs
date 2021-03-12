using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Key_Controller : NetworkBehaviour
{

    public GameObject aimRotator;
    // Start is called before the first frame update
    void Start()
    {
        if (!base.hasAuthority) { Destroy(this); }
    }

    // Update is called once per frame
    void Update()
    {
        aim();
        /*
        if (Input.GetKey("d")){

            if (Input.GetKeyDown("left shift"))
            {
                GetComponent<Rigidbody>().AddForce(transform.right * -10f, ForceMode.Impulse);
            }
        }
        else if (Input.GetKey("a"))
        {

            if (Input.GetKeyDown("left shift"))
            {
                GetComponent<Rigidbody>().AddForce(transform.right * 10f, ForceMode.Impulse);
            }
        }
        */
        if (Input.GetKeyDown("1"))
        {
            GetComponent<Shoot_System>().changeProjectile(0);
        }
        if (Input.GetKeyDown("2"))
        {
            GetComponent<Shoot_System>().changeProjectile(1);
        }
    }

    public void aim()
    {
        if(Camera.main != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane hPlane = new Plane(transform.up, transform.position);
            float distance = 700;

            Quaternion rotationLookAt = Quaternion.LookRotation(ray.GetPoint(distance) - aimRotator.transform.position);

            Vector3 newAngle = rotationLookAt.eulerAngles;
            Vector3 oldAngle = transform.eulerAngles;


            if (hPlane.Raycast(ray, out distance))
            {
                aimRotator.transform.rotation = Quaternion.Euler(new Vector3(oldAngle.x, newAngle.y, oldAngle.z));
            }
        }
    }
}

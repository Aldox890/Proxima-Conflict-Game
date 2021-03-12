using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Cannon_Turrets : NetworkBehaviour
{
    public GameObject radar;
    public GameObject projectile;

    public bool isShooter = false;

    public float shootSpeed = 1f;

    public float offsetHr = 2f;
    public float offsetVr = 0.3f;

    public GameObject output1;
    public GameObject output2;

    private float range;

    // Start is called before the first frame update
    void Start()
    {
        range = radar.GetComponent<SphereCollider>().radius;

        //if (base.isServer) { InvokeRepeating("updateRotation", 0.01f, 0.05f); }
    }

    public void updateRotation()
    {
        RpcUpdateRotation(transform.rotation);
    }

    [ClientRpc]
    public void RpcUpdateRotation(Quaternion rot)
    {
        if (!base.isServer) transform.rotation = rot;
        //if(!base.isServer) Quaternion.Slerp(transform.rotation, rot, Time.deltaTime/0.05f);
    }

    [ClientRpc]
    public void RpcSetTarget(GameObject target)
    {
        radar.GetComponent<Radar>().target = target;
    }

    private float timer = 0f;

    //[Server]
    void Update()
    {
        if(radar.GetComponent<Radar>().target != null)
        {
            if (base.isServer)
            {
                timer += Time.deltaTime;
                GameObject target = radar.GetComponent<Radar>().target;
                RpcSetTarget(target);

                if (Vector3.Distance(target.transform.position, transform.position) > range + 5f)
                {
                    radar.GetComponent<Radar>().target = null;
                    return;
                };


                transform.LookAt(new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z));

                if (timer >= shootSpeed) {
                    Vector3 pos1,pos2;
                    if (output1 != null) pos1 = output1.transform.position;
                    else {
                        pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        pos1 = pos1 + transform.right * -offsetVr + transform.forward * offsetHr;
                     }

                    if (output2 != null) pos2 = output2.transform.position;
                    else
                    {
                        pos2 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        pos2 = pos2 +transform.right * offsetVr + transform.forward * offsetHr;
                    }

                    //GameObject proj = Instantiate(projectile, pos1, Quaternion.LookRotation(transform.forward));
                    //GameObject proj2 = Instantiate(projectile, pos2, Quaternion.LookRotation(transform.forward));
                    RpcShoot(pos1, pos2);
                    /*
                    if (isShooter)
                    {
                        proj.GetComponent<Projectile_Physics>().shooter = gameObject;
                        proj2.GetComponent<Projectile_Physics>().shooter = gameObject;
                    }
                    else
                    {
                        proj.GetComponent<Projectile_Physics>().shooter = transform.parent.parent.parent.gameObject;
                        proj2.GetComponent<Projectile_Physics>().shooter = transform.parent.parent.parent.gameObject;
                    }

                    if (projectile.GetComponent<Projectile_Physics>().hasParent)
                    {
                        proj.transform.parent = transform;
                        proj2.transform.parent = transform;
                    }
                    */
                    timer = 0f;
                }
            }
            else
            {
                GameObject target = radar.GetComponent<Radar>().target;
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            }
        }
    }

    [ClientRpc]
    public void RpcShoot(Vector3 pos1, Vector3 pos2)
    {
        GameObject proj = Instantiate(projectile, pos1, Quaternion.LookRotation(transform.forward));
        GameObject proj2 = Instantiate(projectile, pos2, Quaternion.LookRotation(transform.forward));

        if (isShooter)
        {
            proj.GetComponent<Projectile_Physics>().shooter = gameObject;
            proj2.GetComponent<Projectile_Physics>().shooter = gameObject;
        }
        else
        {
            proj.GetComponent<Projectile_Physics>().shooter = transform.parent.parent.parent.gameObject;
            proj2.GetComponent<Projectile_Physics>().shooter = transform.parent.parent.parent.gameObject;
        }

        if (projectile.GetComponent<Projectile_Physics>().hasParent)
        {
            proj.transform.parent = transform;
            proj2.transform.parent = transform;
        }
    }
}

using System.Collections;
using UnityEngine;
using Mirror;
using System.Security.Cryptography;
using System.Reflection;
using System.ComponentModel;

public class Shoot_System : NetworkBehaviour
{
    [SyncVar]
    public Vector3 target;
    //public GameObject projectile;
    public GameObject aimRotator;
    public GameObject radarObj;

    public GameObject[] weapons;

    private Radar radar;

    public Vector3 shootTarget;

    // Start is called before the first frame update
    void Start()
    {
        weapons = new GameObject[4];
    }

    public int addWeapon(GameObject weapObj)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = weapObj;
                return i;
            }
        }
        return -1;
    }

    public void clearWeapons()
    {
        weapons = new GameObject[4];
    }


    // Update is called once per frame
    void Update()
    {

        /*
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
        */
    }

    public void changeProjectile(int n)
    {
        CmdChangeProjectile(n);
    }

    [Command]
    public void CmdChangeProjectile(int n)
    {
        RpcChangeProjectile(n);
    }

    [ClientRpc]
    public void RpcChangeProjectile(int n)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].GetComponent<Shoot_Controller>().changeProjectile(n);
            }
        }
    }

    [Command]
    public void CmdShoot(Vector3 pos, Quaternion rot, int weapNum)
    {
        RpcShoot(pos, rot, weapNum);
    }

    [ClientRpc]
    public void RpcShoot(Vector3 pos, Quaternion rot, int weapNum)
    {
        int i = weapons[weapNum].GetComponent<Shoot_Controller>().projectileNum;
        if (weapons[weapNum].GetComponent<Shoot_Controller>().projectile[i].GetComponent<Projectile_Physics>().startVfx != null){
            GameObject exp = Instantiate(weapons[weapNum].GetComponent<Shoot_Controller>().projectile[i].GetComponent<Projectile_Physics>().startVfx, new Vector3(weapons[weapNum].transform.position.x, weapons[weapNum].transform.position.y, weapons[weapNum].transform.position.z), Quaternion.LookRotation(transform.forward));
            exp.transform.parent = gameObject.transform;
        }
        GameObject proj = Instantiate(weapons[weapNum].GetComponent<Shoot_Controller>().projectile[i], new Vector3(weapons[weapNum].transform.position.x, weapons[weapNum].transform.position.y, weapons[weapNum].transform.position.z), Quaternion.LookRotation(transform.forward));
        proj.transform.rotation = rot;
        proj.GetComponent<Projectile_Physics>().shooter = gameObject;

        if (weapons[weapNum].GetComponent<Shoot_Controller>().projectile[i].GetComponent<Projectile_Physics>().hasParent) proj.transform.parent = gameObject.transform;
        //proj.GetComponent<Rigidbody>().velocity = transform.root.GetComponent<Rigidbody>().velocity;

    }


    public void botShoot(Vector3 pos, Quaternion rot, int weapNum)
    {
        radar = radarObj.GetComponent<Radar>();
        RaycastHit hit;
        if (radar.target != null) {

            float distance = Vector3.Distance(aimRotator.transform.position, radar.target.transform.position);
            RpcShoot(pos, rot, weapNum);

            /*
            if (Physics.Raycast(aimRotator.transform.position, -aimRotator.transform.forward, out hit, distance)) 
            {
                if (hit.transform.gameObject == radar.target) {
                    
                    GameObject proj = Instantiate(projectile, new Vector3(pos.x, pos.y, pos.z), Quaternion.LookRotation(transform.forward));
                    proj.transform.rotation = rot;
                    proj.GetComponent<Projectile_Physics>().shooter = gameObject;
                    

                    RpcShoot( pos, rot,weapNum);

                }
            }
            */
        }
    }
}

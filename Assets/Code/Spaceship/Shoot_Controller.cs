using System.Collections;
using UnityEngine;

public class Shoot_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPlayer = false;
    public bool isLocalPlayer = false;
    public GameObject[] projectile;
    public float[] reloadTimeArr;

    public int projectileNum = 0;
    
    private Shoot_System shootSystem;
    public float minAngle=150;
    public float maxAngle=210;
    public float reloadTime = 1;
    private float timer = 100;

    private int weapNum = -1;


    void Start()
    {
        if (gameObject.transform.parent.parent.GetComponent<Ship_Controller>()) { 
            isPlayer = gameObject.transform.parent.parent.GetComponent<Ship_Controller>().isPlayer;
            isLocalPlayer = gameObject.transform.parent.parent.GetComponent<Ship_Controller>().isLocalPlayer;
        }

        shootSystem = gameObject.transform.parent.parent.GetComponent<Shoot_System>();
        if(shootSystem!=null) weapNum = shootSystem.addWeapon(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        /*
        Vector3 newAngle = shootSystem.aimRotator.transform.localRotation.eulerAngles;
        Vector3 oldAngle = transform.localRotation.eulerAngles;
        if (checkAngle(newAngle.y, minAngle, maxAngle))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(oldAngle.x, newAngle.y, oldAngle.z));
        }
        */
        if (!isPlayer)
        {
            botShoot();
        }
        else if (isLocalPlayer && Input.GetMouseButtonDown(1))
        {
            shoot();
        }
        
    }

    public void changeProjectile(int n)
    {
        projectileNum = n;
    }

    public void botShoot()
    {
        Radar radar = shootSystem.radarObj.GetComponent<Radar>();
        RaycastHit hit;
        if (radar.target != null)
        {
            float distance = Vector3.Distance(transform.position, radar.target.transform.position);
            shoot();
            /*
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
            {
                if (hit.transform.root.gameObject == radar.target)
                {
                    shoot();
                }
            }
            */
        }
    }


    bool checkAngle(float n, float a, float b)
    {
        n = (360 + (n % 360)) % 360;
        a = (3600000 + a) % 360;
        b = (3600000 + b) % 360;

        if (a < b) return a <= n && n <= b;
        return a <= n || n <= b;
    }



    void shoot()
    {
        if (shootSystem != null)
        {
            if (timer >= reloadTimeArr[projectileNum]) {

                if (isPlayer) shootSystem.CmdShoot(transform.position, transform.rotation, weapNum);
                else shootSystem.botShoot(transform.position, transform.rotation, weapNum);
                timer = 0;
            }
        }
    }
}





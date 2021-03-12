using UnityEngine;
using Mirror;

public class Projectile_Physics : NetworkBehaviour
{
    float spawnTime;
    public float duration = 2f;

    public float force = 4f;
    public float damage = 5f;
    public GameObject shooter;

    public bool hasParent = false;
    public bool destroyOnHit = true;

    public GameObject startVfx;
    public GameObject enemyDestory;
    public GameObject standardDestory;
    public GameObject shieldDestroy;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        if(!hasParent) GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(transform.forward * 200f * Time.deltaTime);
        if (duration > 0 && Time.time - spawnTime > duration)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider c)
    {

        GameObject parent;
        if (c.isTrigger  || (c.transform.parent && c.transform.parent.parent && c.transform.parent.parent.parent))
        {
            if (c.isTrigger || c.transform.parent.parent.parent.gameObject == shooter)
            {
                return;
            }
            Debug.Log(c.transform.parent.parent.parent.name);

            parent = c.transform.parent.parent.parent.gameObject;

            if (parent.GetComponent<Entity>())
            {
                parent.GetComponent<Entity>().takeDamage(this);
            }

            if (parent.GetComponent<Barrier>() != null)
            {
                //GameObject ps = Resources.Load("vfx/PlasmExplosion") as GameObject;
                /*
                if (parent.GetComponent<Ship_Controller>().isLocalPlayer)
                {
                    CameraController cc;
                    if (cc = Camera.main.GetComponent<CameraController>())
                    {
                        cc.shake(0.2f, 0.4f);
                    }
                }*/
                if (shieldDestroy != null)
                {
                    GameObject ps = Instantiate(shieldDestroy, transform.position, transform.rotation);
                    ps.transform.parent = c.gameObject.transform;
                }
                if (destroyOnHit) Destroy(gameObject);
            }
            else if (parent.GetComponent<Entity>())
            {
                if (enemyDestory != null) Instantiate(enemyDestory, transform.position, transform.rotation);
                if (destroyOnHit) Destroy(gameObject);
            }
            else
            {
                //GameObject ps = Resources.Load("vfx/LaserExplosion") as GameObject;
                if (standardDestory != null) Instantiate(standardDestory, transform.position, transform.rotation);
                if (destroyOnHit) Destroy(gameObject);
            }
        }
        else
        {
            //GameObject ps = Resources.Load("vfx/LaserExplosion") as GameObject;
            if (standardDestory != null) Instantiate(standardDestory, transform.position, transform.rotation);
            if (destroyOnHit) Destroy(gameObject);
        }

        /*
        if (c.isTrigger ||c.transform.parent.parent.gameObject == shooter)
        {
            return;
        }
        if (c.transform.root.GetComponent<Entity>())
        {
            c.transform.root.GetComponent<Entity>().takeDamage(this);
        }

        if (c.transform.parent.gameObject.GetComponent<Barrier>()!= null) {
            //GameObject ps = Resources.Load("vfx/PlasmExplosion") as GameObject;
            if (c.transform.root.gameObject.GetComponent<Ship_Controller>().isLocalPlayer)
            {
                CameraController cc;
                if (cc = Camera.main.GetComponent<CameraController>())
                {
                    cc.shake(0.2f, 0.4f);
                }
            }
            if (shieldDestroy != null)
            {
                GameObject ps = Instantiate(shieldDestroy, transform.position + (transform.forward * 0.8f), transform.rotation);
                ps.transform.parent = c.gameObject.transform;
            }
            if (destroyOnHit) Destroy(gameObject);
        }
        else if(c.transform.root.gameObject.layer == 13 )
        {
            //GameObject ps = Resources.Load("vfx/LaserExplosion") as GameObject;
            if (standardDestory != null) Instantiate(standardDestory, transform.position, transform.rotation);
            if (destroyOnHit) Destroy(gameObject);
        }
        else
        {
            //GameObject ps = Resources.Load("vfx/LaserExplosion") as GameObject;
            if (c.transform.root.gameObject.GetComponent<Entity>().isLocalPlayer)
            {
                CameraController cc;
                if(cc = Camera.main.GetComponent<CameraController>())
                {
                    cc.shake(0.2f, 0.4f);
                }
            }
            if(enemyDestory != null) Instantiate(enemyDestory, transform.position, transform.rotation);
            if (destroyOnHit) Destroy(gameObject);
        }
                */
    }
}

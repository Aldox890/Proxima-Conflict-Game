using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Entity : NetworkBehaviour
{
    public bool isPlayer = true;
    public bool isLocalPlayer = false;
    public GameObject lootbag;

    [SyncVar]
    public float maxHealth;
    [SyncVar]
    public float maxShield;
    [SyncVar(hook = "setHealtbar")]
    public float health = 100;
    [SyncVar(hook = "setShieldbar")]
    public float shield = 100;
    [SyncVar]
    public float speed = 100;
    [SyncVar]
    public float turnSpeed = 100;
    [SyncVar]
    public float inventory = 100;

    public GameObject shieldObj;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        if (shield > 0)
        {
            shieldObj = Instantiate(shieldObj, transform.position, transform.rotation);
            shieldObj.transform.parent = gameObject.transform;
        }
        isLocalPlayer = base.hasAuthority;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealtbar(float oldval, float newval)
    {
        if (isLocalPlayer)
        {
            GameObject healthbar = GameObject.Find("HealthScroll");
            healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(25 * newval / maxHealth, 2.5f);
        }
    }

    public void setShieldbar(float oldval, float newval)
    {
        if (isLocalPlayer)
        {
            GameObject healthbar = GameObject.Find("ShieldScroll");
            healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(25 * newval / maxShield, 2.5f);
        }
    }

    [ClientRpc]
    public void RpcShieldDeath()
    {
        Vector3 rotation = transform.eulerAngles;
        GameObject temp = Instantiate(shieldObj.GetComponent<Barrier>().deathVFX, transform.position, Quaternion.Euler(0f, rotation.y + 90f, 0f));
        temp.transform.parent = gameObject.transform;
        Destroy(shieldObj);
    }

    [ClientRpc]
    public void RpcDeath()
    {
        //GameObject ps = Resources.Load<GameObject>("vfx/ShipExplosion");
        GameObject ps = Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(gameObject);
    }

    [Server]
    public void newSpawnLoot()
    {
        Inventory inv;
        if(inv = gameObject.GetComponent<Inventory>())
        {
            GameObject loot = Instantiate(lootbag, transform.position, transform.rotation);
            Inventory lootInv = loot.GetComponent<Inventory>();
            lootInv.inventory = inv.inventory;
            loot.GetComponent<Inventory>().nElem = inv.nElem;
            NetworkServer.Spawn(loot);
        }
    }

    [Server]
    public void spawnLoot()
    {
        Inventory inv;
        if (inv = gameObject.GetComponent<Inventory>())
        {
            int i = 0;
            while (i < inv.size) {
                if (inv.inventory[i] != null) {
                    Debug.Log("inventoryTest");

                    GameObject loot = Instantiate(lootbag, transform.position, transform.rotation);
                    loot.GetComponent<Loot>().type = inv.inventory[i].type;
                    loot.GetComponent<Loot>().quantity = inv.inventory[i].quantity;
                    NetworkServer.Spawn(loot);
                }
                i++;
            }
        }
        else
        {
            GameObject loot = Instantiate(lootbag, transform.position, transform.rotation);

            //loot.GetComponent<Loot>().item.type = "metal";
            loot.GetComponent<Loot>().type = "Iron";
            loot.GetComponent<Loot>().quantity = 500;

            NetworkServer.Spawn(loot);
        }
    }

    [Server]
    public void takeDamage(Projectile_Physics projectile)
    {
        lock (gameObject)
        {
            if (shield > 0)
            {
                shield = shield - projectile.damage;
                if (shield <= 0)
                {
                    RpcShieldDeath();
                }
            }
            else if (health > 0)
            {
                //Debug.Log(health);
                health = health - projectile.damage;
                if (health <= 0)
                {
                    Collider [] colChildren = gameObject.GetComponentsInChildren<Collider>();
                    foreach (Collider col in colChildren)
                    {
                        col.enabled = false;
                    }

                    newSpawnLoot();
                    RpcDeath();
                }
            }
        }
    }

    [Server]
    public void takeDamage(float damage)
    {
        lock (gameObject)
        {
            if (shield > 0)
            {
                shield = shield - damage;
                if (shield <= 0)
                {
                    RpcShieldDeath();
                }
            }
            else if (health > 0)
            {
                //Debug.Log(health);
                health = health - damage;
                if (health <= 0)
                {
                    Collider[] colChildren = gameObject.GetComponentsInChildren<Collider>();
                    foreach (Collider col in colChildren)
                    {
                        col.enabled = false;
                    }

                    newSpawnLoot();
                    RpcDeath();
                }
            }
        }
    }

}

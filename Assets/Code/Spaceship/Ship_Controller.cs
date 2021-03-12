using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Ship_Controller : NetworkBehaviour
{
    public bool random = false;

    public GameObject shipModel;
    public bool isPlayer = true;
    public bool isLocalPlayer = false;

    [SyncVar(hook = "ChangeHull")]
    public int hullID;
    [SyncVar(hook = "ChangeArmor")]
    public int armorID;
    [SyncVar(hook = "ChangeArmor")]
    public int engineID;

    [SyncVar(hook = "ChangeW1")]
    public int weapon1ID;
    [SyncVar(hook = "ChangeW2")]
    public int weapon2ID;
    [SyncVar(hook = "ChangeW3")]
    public int weapon3ID;
    [SyncVar(hook = "ChangeW4")]
    public int weapon4ID;

    public float enterHangarTimer;

    private Entity entity;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        entity = gameObject.GetComponent<Entity>();
        enterHangarTimer = 0;
        isLocalPlayer = base.hasAuthority;
        if (base.isServer) InvokeRepeating("updatePos", 0.01f, 0.01f);
        //gameObject.GetComponent<Rigidbody>().isKinematic = !base.isServer;
        if (random) randomizeShip();
    }

    [Server]
    void updatePos()
    {
        RpcsetPosition(transform.position, transform.rotation, rigidbody.velocity);
    }

    float x;
    float z;
    Vector3 newPos;
    Quaternion rotation;
    Vector3 newVelocity;
    Vector3 dir;
    [ClientRpc]
    void RpcsetPosition(Vector3 pos, Quaternion r, Vector3 velocity)
    {
        newPos = pos;
        newVelocity = velocity;
        rotation = r;
    }

    // Update is called once per frame
    void Update()
    {
        enterHangarTimer += Time.deltaTime;
        /*
        if (Input.GetKeyDown("space"))
        {
            if (base.hasAuthority) {
                CmdChangeArmor(Random.Range(1, 10));
                CmdChangeWeapons(1, Random.Range(1, 3));
                CmdChangeWeapons(2, Random.Range(1, 3));
                CmdChangeWeapons(3, Random.Range(1, 3));
                CmdChangeWeapons(4, Random.Range(1, 3));
            }
        }
        */
    }
    float t = 0;
    void FixedUpdate()
    {
        if (!base.isServer) {
            //rigidbody.MoveRotation(rotation);
            rigidbody.velocity = newVelocity;
            //Debug.Log(newVelocity.magnitude);
            if (Vector3.Distance(transform.position, newPos) > 2f) {
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime/0.1f);
                dir = Vector3.Normalize(transform.position - newPos);
            }
        }
    }

    [Server]
    public void randomizeShip()
    {
        int randHull = Random.Range(1, 1);
        hullID = randHull;

        Hull hullData = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + randHull).GetComponent<Hull>();

        armorID = hullData.allowedArmor[Random.Range(0, hullData.allowedArmor.Length)];
        engineID = hullData.allowedEngine[Random.Range(0, hullData.allowedEngine.Length)];
        weapon1ID = Random.Range(3, 4);
        weapon2ID = Random.Range(3, 4);
        weapon3ID = Random.Range(3, 4);
        weapon4ID = Random.Range(3, 4);
    }

    public void setSpaceship(Hangar_Builder builder)
    {
        //Ship_Controller sc = gameController.GetComponent<GameController>().PlayerShip.GetComponent<Ship_Controller>();
        CmdChangeHull(builder.hullID);
        CmdChangeArmor(builder.armorID);
        CmdChangeEngine(builder.engineID);
        CmdChangeWeapons(1, builder.weapon1ID);
        CmdChangeWeapons(2, builder.weapon2ID);
        CmdChangeWeapons(3, builder.weapon3ID);
        CmdChangeWeapons(4, builder.weapon4ID);
    }

    [ClientRpc]
    private void RpcActivate()
    {
        gameObject.SetActive(true);
    }

    [Command]
    private void CmdActivate()
    {
        RpcActivate();
    }

    public void activate()
    {
        CmdActivate();
    }

    [ClientRpc]
    private void RpcDeactivate()
    {
        gameObject.SetActive(false);
    }

    [Command]
    private void CmdDeactivate()
    {
        RpcDeactivate();
    }

    public void deactivate()
    {
        CmdDeactivate();
    }

    [Command]
    void CmdChangeArmor(int armor)
    {
        armorID = armor;
    }

    [Command]
    void CmdChangeEngine(int engine)
    {
        engineID = engine;
    }

    [Command]
    void CmdChangeWeapons(int weap, int weapID)
    {
        if (weap == 1) weapon1ID = weapID;
        else if (weap == 2) weapon2ID = weapID;
        else if (weap == 3) weapon3ID = weapID;
        else if (weap == 4) weapon4ID = weapID;
    }

    [Command]
    void CmdChangeHull(int hull)
    {
        Hull hullData = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + hullID).GetComponent<Hull>();
        hullID = hull;
        entity.health = hullData.healthBase;
        entity.speed = hullData.speedBase;
        entity.turnSpeed = hullData.turnSpeedBase;
        entity.inventory = hullData.inventoryBase;
        shipModel.GetComponent<Ship_Builder>().changeArmor();
    }

    void ChangeArmor(int oldval,int newval)
    {
        //int typeID = Resources.Load<GameObject>("Ship_Parts/Armor/Armor " + armorID).GetComponent<Armor>().type;
        bool rs = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + hullID).GetComponent<Hull>().isAllowed(armorID);
        if (!rs) { armorID = 1; }

        shipModel.GetComponent<Ship_Builder>().changeArmor();
    }

    void ChangeHull(int oldval, int newval)
    {
        shipModel.GetComponent<Ship_Builder>().changeArmor();
    }

    void ChangeW1(int oldval, int newval)
    {
        shipModel.GetComponent<Ship_Builder>().changeArmor();
    }

    void ChangeW2(int oldval, int newval)
    {
        shipModel.GetComponent<Ship_Builder>().changeArmor();
    }

    void ChangeW3(int oldval, int newval)
    {
        shipModel.GetComponent<Ship_Builder>().changeArmor();
    }

    void ChangeW4(int oldval, int newval)
    {
        shipModel.GetComponent<Ship_Builder>().changeArmor();
    }

    /*
    [Server]
    void OnTriggerEnter(Collider c)
    {

    //Debug.Log(c.name);
    Debug.Log(gameObject.name);
    Projectile_Physics pf;
    if (c.isTrigger && c.transform.parent && c.transform.parent.GetComponent<Projectile_Physics>()) {
        pf = c.transform.parent.GetComponent<Projectile_Physics>();
        if (pf.shooter != gameObject)
        {
            lock (gameObject)
            {
                Debug.Log(health);
                health = health - pf.damage;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    }
    */
}

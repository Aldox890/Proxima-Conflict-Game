using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar_Builder : MonoBehaviour
{
    public bool createAtStart = false;
    public bool randomValues = false;

    Ship_Controller sc;

    public int hullID;
    public int armorID;
    public int engineID;
    public int weapon1ID;
    public int weapon2ID;
    public int weapon3ID;
    public int weapon4ID;
      

    GameObject hull;
    GameObject armor;
    GameObject engine;

    GameObject weapon1;
    GameObject weapon2;
    GameObject weapon3;
    GameObject weapon4;

    // Start is called before the first frame update
    void Start()
    {
        if (randomValues) randomizeShip();
        if(createAtStart) changeArmor();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeHull(int n)
    {
        hullID = n;

        Hull hullData = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + hullID).GetComponent<Hull>();

        armorID = hullData.allowedArmor[0];
        engineID = hullData.allowedEngine[0];
        weapon1ID = 1;
        weapon2ID = 1;
        weapon3ID = 1;
        weapon4ID = 1;
        changeArmor();
    }

    public void changeWings(int n)
    {
        armorID = n;
        changeArmor();
    }

    public void changeEngine(int n)
    {
        engineID = n;
        changeArmor();
    }

    public void randomizeShip()
    {
        int rand = Random.Range(1, 7);
        hullID = rand;

        Hull hullData = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + hullID).GetComponent<Hull>();

        armorID = hullData.allowedArmor[Random.Range(0, hullData.allowedArmor.Length)];
        engineID = hullData.allowedEngine[Random.Range(0, hullData.allowedEngine.Length)];
        weapon1ID = Random.Range(1, 2);
        weapon2ID = Random.Range(1, 2);
        weapon3ID = Random.Range(1, 2);
        weapon4ID = Random.Range(1, 2);
        changeArmor();
    }

    public void getSpaceshipData()
    {
        Ship_Controller sc = GameObject.Find("GameController").GetComponent<GameController>().PlayerShip.GetComponent<Ship_Controller>();

        hullID = sc.hullID;
        armorID = sc.armorID;
        engineID = sc.engineID;
        weapon1ID = sc.weapon1ID;
        weapon2ID = sc.weapon2ID;
        weapon3ID = sc.weapon3ID;
        weapon4ID = sc.weapon4ID;

    }

    public void changeArmor()
    {
        if (hull != null) Destroy(hull);
        hull = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + hullID);

        hull = Instantiate(hull, transform.position, transform.rotation);
        hull.transform.parent = gameObject.transform;

        if (engine != null) Destroy(engine);
        engine = Resources.Load<GameObject>("Ship_Parts/Window/Window " + engineID);
        engine = Instantiate(engine, transform.position, transform.rotation);

        engine.transform.parent = gameObject.transform;

        if (weapon1 != null) Destroy(weapon1);
        if (weapon2 != null) Destroy(weapon2);
        if (weapon1 != null) Destroy(weapon3);
        if (weapon3 != null) Destroy(weapon4);
        if (armor != null) Destroy(armor);

        armor = Resources.Load<GameObject>("Ship_Parts/Armor/Armor " + armorID);
        armor = Instantiate(armor, transform.position, transform.rotation);
        armor.transform.parent = gameObject.transform;

        if (armor.GetComponent<Armor>().weapon1location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon1location.transform;
            weapon1 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + weapon1ID);
            weapon1 = Instantiate(weapon1, location.position, location.rotation);
            //weapon1.transform.rotation *=  Quaternion.Euler(0, 180, 0);
            weapon1.transform.parent = gameObject.transform;

        }
        if (armor.GetComponent<Armor>().weapon2location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon2location.transform;
            weapon2 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + weapon2ID);
            weapon2 = Instantiate(weapon2, location.position, location.rotation);
            //weapon2.transform.rotation *=  Quaternion.Euler(0, 180, 0);
            weapon2.transform.parent = gameObject.transform;
        }
        if (armor.GetComponent<Armor>().weapon3location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon3location.transform;
            weapon3 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + weapon3ID);
            weapon3 = Instantiate(weapon3, location.position, location.rotation);
            //weapon3.transform.rotation *=  Quaternion.Euler(0, 180, 0);
            weapon3.transform.parent = gameObject.transform;
        }
        if (armor.GetComponent<Armor>().weapon4location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon4location.transform;
            weapon4 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + weapon4ID);
            weapon4 = Instantiate(weapon4, location.position, location.rotation);
            //weapon4.transform.rotation *=  Quaternion.Euler(0, 180, 0);
            weapon4.transform.parent = gameObject.transform;
        }
    }

}

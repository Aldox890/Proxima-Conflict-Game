using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Builder : MonoBehaviour
{

    Ship_Controller sc;

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
        changeArmor();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeArmor()
    {
        transform.root.GetComponent<Shoot_System>().weapons = new GameObject[4];
        if (hull != null) Destroy(hull);
        sc = transform.parent.GetComponent<Ship_Controller>();
        hull = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + sc.hullID);

        hull = Instantiate(hull, transform.position, transform.rotation);
        hull.transform.parent = gameObject.transform;

        if (engine != null) Destroy(engine);
        engine = Resources.Load<GameObject>("Ship_Parts/Window/Window " + sc.engineID);
        engine = Instantiate(engine, transform.position, transform.rotation);

        engine.transform.parent = gameObject.transform;

        if (weapon1 != null) Destroy(weapon1);
        if (weapon2 != null) Destroy(weapon2);
        if (weapon1 != null) Destroy(weapon3);
        if (weapon3 != null) Destroy(weapon4);
        if (armor != null) Destroy(armor);

        armor = Resources.Load<GameObject>("Ship_Parts/Armor/Armor " + sc.armorID);
        armor = Instantiate(armor, transform.position, transform.rotation);
        armor.transform.parent = gameObject.transform;

        if (armor.GetComponent<Armor>().weapon1location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon1location.transform;
            weapon1 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + sc.weapon1ID);
            weapon1 = Instantiate(weapon1, location.position, location.rotation);
            weapon1.GetComponent<Shoot_Controller>().isPlayer = sc.isPlayer;
            weapon1.transform.parent = gameObject.transform;

        }
        if (armor.GetComponent<Armor>().weapon2location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon2location.transform;
            weapon2 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + sc.weapon2ID);
            weapon2 = Instantiate(weapon2, location.position, location.rotation);
            weapon2.GetComponent<Shoot_Controller>().isPlayer = sc.isPlayer;
            weapon2.transform.parent = gameObject.transform;
        }
        if (armor.GetComponent<Armor>().weapon3location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon3location.transform;
            weapon3 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + sc.weapon3ID);
            weapon3 = Instantiate(weapon3, location.position, location.rotation);
            weapon3.GetComponent<Shoot_Controller>().isPlayer = sc.isPlayer;
            weapon3.transform.parent = gameObject.transform;
        }
        if (armor.GetComponent<Armor>().weapon4location != null)
        {
            Transform location = armor.GetComponent<Armor>().weapon4location.transform;
            weapon4 = Resources.Load<GameObject>("Ship_Parts/Weapon/Weapon " + sc.weapon4ID);
            weapon4 = Instantiate(weapon4, location.position, location.rotation);
            weapon4.GetComponent<Shoot_Controller>().isPlayer = sc.isPlayer;
            weapon4.transform.parent = gameObject.transform;
        }
    }

}

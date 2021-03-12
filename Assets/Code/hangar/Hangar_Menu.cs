using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar_Menu : MonoBehaviour
{
    public GameObject scroll;
    public GameObject cell;

    public int type = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (type == 0)
        {
            Hull hullData;
            int i = 1;
            while (hullData = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + i).GetComponent<Hull>())
            {
                GameObject tempCell = Instantiate(cell, scroll.transform);
                tempCell.transform.position = new Vector3(tempCell.transform.position.x, tempCell.transform.position.y - ((i - 1) * 70), tempCell.transform.position.z);
                tempCell.transform.localScale = new Vector3(1, 1, 1);

                tempCell.transform.GetComponent<ItemData>().num = i;
                tempCell.transform.GetComponent<ItemData>().itemType = 0;

                i++;
            }
        }
        else if (type == 1)
        {
            Armor armor;
            int i = 1;
            while (armor = Resources.Load<GameObject>("Ship_Parts/Armor/Armor " + i).GetComponent<Armor>())
            {
                GameObject tempCell = Instantiate(cell, scroll.transform);
                tempCell.transform.position = new Vector3(tempCell.transform.position.x, tempCell.transform.position.y - ((i - 1) * 70), tempCell.transform.position.z);
                tempCell.transform.localScale = new Vector3(1, 1, 1);

                tempCell.transform.GetComponent<ItemData>().num = i;
                tempCell.transform.GetComponent<ItemData>().itemType = 1;

                i++;
            }
        }
        else if (type == 2)
        {
            Engine engine;
            int i = 1;
            while (engine = Resources.Load<GameObject>("Ship_Parts/Window/window " + i).GetComponent<Engine>())
            {
                GameObject tempCell = Instantiate(cell, scroll.transform);
                tempCell.transform.position = new Vector3(tempCell.transform.position.x, tempCell.transform.position.y - ((i - 1) * 70), tempCell.transform.position.z);
                tempCell.transform.localScale = new Vector3(1, 1, 1);

                tempCell.transform.GetComponent<ItemData>().num = i;
                tempCell.transform.GetComponent<ItemData>().itemType = 2;

                i++;
            }
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

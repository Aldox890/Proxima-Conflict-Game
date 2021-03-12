using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData : MonoBehaviour
{
    public int num;
    public int itemType;

    private GameObject hangarController;
    // Start is called before the first frame update
    void Start()
    {
        hangarController = GameObject.Find("HangarController");
        Button button = gameObject.GetComponent<Button>();
        GameObject nameObj = gameObject.transform.Find("Name").gameObject;
        string name = "null0";

        if (itemType == 0)
        {
            button.onClick.AddListener(delegate { setHull(num); });
            name = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + num).GetComponent<Hull>().name;
        }
        else if (itemType == 1)
        {
            button.onClick.AddListener(delegate { setWings(num); });
            name = Resources.Load<GameObject>("Ship_Parts/Armor/Armor " + num).GetComponent<Armor>().name;
        }
        else if (itemType == 2)
        {
            button.onClick.AddListener(delegate { setEngine(num); });
            name = Resources.Load<GameObject>("Ship_Parts/Window/window " + num).GetComponent<Engine>().name;
        }
        nameObj.GetComponent<Text>().text = name;
    }

    public void setHull(int i)
    {
        hangarController.GetComponent<hangar_controller>().changeHull(i);
    }

    public void setWings(int i)
    {
        hangarController.GetComponent<hangar_controller>().changeWings(i);
    }

    public void setEngine(int i)
    {
        hangarController.GetComponent<hangar_controller>().changeEngine(i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loot_gui : MonoBehaviour
{

    private UnityEngine.UI.Text txt;
    private Inventory inv;

    public GameObject container;

    // Start is called before the first frame update
    void Start()
    {
        //txt = gameObject.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        refresh();
        /*
        if (inv != null)
        {
            if (inv.nElem > 0)
            {
                txt.text = inv.inventory[0].quantity.ToString();
            }
        }
        else if (GameObject.Find("GameController").GetComponent<GameController>().PlayerShip != null)
        {
            inv = GameObject.Find("GameController").GetComponent<GameController>().PlayerShip.GetComponent<Inventory>();
        }
        */
    }

    public void clear()
    {
        foreach (Transform child in container.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }



    public void refresh()
    {
        Inventory inv;
        if (inv = GameObject.Find("GameController").GetComponent<GameController>().PlayerShip.GetComponent<Inventory>()) {
            if (inv.needsRefresh == true) {
                clear();

                int i = 0;
                while (inv.size > i)
                {
                    if(inv.inventory[i] != null) {
                        GameObject guiItem = (GameObject)Resources.Load("Items/" + inv.inventory[i].type);
                        item_gui item = guiItem.GetComponent<item_gui>();
                        item.slot = i;
                        item.owner = GameObject.Find("GameController").GetComponent<GameController>().PlayerShip;
                        item.setQuantity(inv.inventory[i]);
                        GameObject guiTemp = GameObject.Instantiate(guiItem);
                        guiTemp.transform.SetParent(container.transform,false);
                    }
                    else
                    {
                        GameObject guiItem = (GameObject)Resources.Load("Items/Empty");
                        GameObject guiTemp = GameObject.Instantiate(guiItem);
                        guiTemp.transform.SetParent(container.transform, false);
                    }
                    i++;
                }
                inv.needsRefresh = false;
            }
        }
    }
}

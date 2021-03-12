using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_slot : MonoBehaviour
{
    public int slot;
    public Inventory inv;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        inv = GameObject.Find("GameController").GetComponent<GameController>().PlayerShip.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        refresh();
    }

    private GameObject guiSlot;
    string type = "";
    int quantity = -1;

    public void onClickDestroy()
    {
        quantity = -1;
        type = "";
        Debug.Log("DropItem");
        GameObject.Find("GameController").GetComponent<GameController>().PlayerShip.GetComponent<Inventory>().CmddropItem(slot);
    }

    public void refresh()
    {
                if (inv.inventory[slot] != null)
                {
                    if (inv.inventory[slot].type != type || inv.inventory[slot].quantity != quantity)
                    {
                        type = inv.inventory[slot].type;
                        quantity = inv.inventory[slot].quantity;
                        GameObject.Destroy(guiSlot);
                    
                        GameObject guiItem = (GameObject)Resources.Load("Items/_" + inv.inventory[slot].type);
                        guiSlot = GameObject.Instantiate(guiItem);
                    
                        text.GetComponent<UnityEngine.UI.Text>().text = inv.inventory[slot].quantity.ToString();
                        text.SetActive(true);
                    
                        guiSlot.transform.SetParent(gameObject.transform, false);
                    }
                }
                else if(text.active)
                {
                    text.SetActive(false);
                    GameObject.Destroy(guiSlot);
                }
            //}
    }
}

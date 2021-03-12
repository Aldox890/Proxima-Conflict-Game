using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_gui : MonoBehaviour
{
    Item item;
    public int slot;
    public GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setQuantity(Item i)
    {
        item = i;
        gameObject.transform.Find("qtText").GetComponent<UnityEngine.UI.Text>().text = item.quantity.ToString();
    }

    public void onClickDestroy()
    {
        Debug.Log("DropItem");
        owner.GetComponent<Inventory>().CmddropItem(slot);
    }
}

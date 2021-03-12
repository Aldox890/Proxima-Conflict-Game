using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Inventory : NetworkBehaviour
{
    public bool needsRefresh = true;

    public Item[] inventory;
    public int size = 10;
    [SyncVar]
    public int nElem = 0;

    public bool isRandom = false;

    public GameObject lootbag;

    void Awake()
    {
        inventory = new Item[size];
    }
    // Start is called before the first frame update
    void Start()
    {
        if (base.isServer && isRandom) {
            random();
        }
    }

    public void random()
    {
        int i = Random.Range(0, 3);
        if (i == 0) { addItem("iron", Random.Range(20, 100)); }
        else if(i == 1) { addItem("ruby", Random.Range(20, 100)); }
        else if (i == 2) { addItem("cobalt", Random.Range(20, 100)); }
    }

    public void addItem(string t, int q)
    {
        inventory[nElem] = new Item();
        inventory[nElem].type = t;
        inventory[nElem].quantity = q;
        RpcSetItem(0, inventory[nElem].type, inventory[nElem].quantity);
        nElem++;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void CmdgetItem(int i)
    {
        RpcSetItem(i, inventory[i].type, inventory[i].quantity);
    }

    [Command]
    public void CmddropItem(int i)
    {
        if(inventory[i] !=null) {

            GameObject loot = Instantiate(lootbag, transform.position + transform.forward * -5f, transform.rotation);
            Inventory lootInv = loot.GetComponent<Inventory>();
            lootInv.inventory[0] = inventory[i];
            loot.GetComponent<Inventory>().nElem = 1;
            NetworkServer.Spawn(loot);

            inventory[i] = null;
            RpcdropItem(i);
            nElem--;
        }
    }

    [ClientRpc]
    public void RpcdropItem(int i)
    {
        inventory[i] = null;
        nElem--;
        needsRefresh = true;
    }

    [ClientRpc]
    public void RpcSetItem(int i,string type, int quantity)
    {
            inventory[i] = new Item();
            inventory[i].type = type;
            inventory[i].quantity = quantity;
            needsRefresh = true;
    }

    
    [Server]
    public void newLoot(Collider c)
    {
        Inventory inv;
        if(inv = c.gameObject.GetComponent<Inventory>())
        {

            for(int k = 0; k < inv.size; k++)
            {
                if(inv.inventory[k] != null) {
                    int fit = -1;

                    for (int i = 0; i < size; i++)
                    {
                        if (fit == -1 && inventory[i] == null)
                        {
                            fit = i;
                        }
                        else if (inventory[i] != null &&inventory[i].type == inv.inventory[k].type)
                        {
                            fit = i;
                            break;
                        }


                        /*
                        if (i<nElem && inventory[i].type == inv.inventory[k].type)
                        {
                            inventory[i].quantity += inv.inventory[k].quantity;
                            RpcSetItem(i, inventory[i].type, inventory[i].quantity);
                            CmdgetItem(i);
                            break;
                        }
                        else if (i == nElem)
                        {
                            inventory[i] = inv.inventory[k];
                            RpcSetItem(i, inventory[i].type, inventory[i].quantity);
                            nElem++;
                            break;
                        }
                        */
                    }
                    if (inventory[fit] == null)
                    {
                        inventory[fit] = inv.inventory[k];
                        RpcSetItem(fit, inventory[fit].type, inventory[fit].quantity);
                        nElem++;
                        break;
                    }
                    else if (inventory[fit].type == inv.inventory[k].type)
                    {
                        inventory[fit].quantity += inv.inventory[k].quantity;
                        RpcSetItem(fit, inventory[fit].type, inventory[fit].quantity);
                        CmdgetItem(fit);
                        break;
                    }


                }
            }
        }
        Destroy(c.gameObject);
    }

    [Server]
    public void loot(Collider c) //edit this when adding removeItem
    {
        Loot loot;
        if (loot = c.gameObject.GetComponent<Loot>())
        {
            int fit = -1;
            for(int i = 0; i < size; i++)
            {
                if(inventory[i].type == loot.type)
                {
                    fit = i;
                    break;
                }
                else if (fit == -1 && inventory[i] == null)
                {
                    fit = i;
                }
                /*
                if (i == nElem)
                {
                    inventory[i] = new Item();
                    inventory[i].type = loot.type;
                    inventory[i].quantity = loot.quantity;
                    Debug.Log(" 1 added:  " + inventory[i].quantity + " to: " + i);
                    RpcSetItem(i, inventory[i].type, inventory[i].quantity);
                    Destroy(c.gameObject);
                    nElem++;
                    CmdgetItem(i);
                    break;
                }
                else if (inventory[i].type == loot.type)
                {
                    inventory[i].quantity += loot.quantity;
                    RpcSetItem( i, inventory[i].type, inventory[i].quantity);
                    Destroy(c.gameObject);
                    CmdgetItem(i);
                    break;
                }
                */

            }

            if (inventory[fit] == null)
            {
                inventory[fit] = new Item();
                inventory[fit].type = loot.type;
                inventory[fit].quantity = loot.quantity;
                Debug.Log(" 1 added:  " + inventory[fit].quantity + " to: " + fit);
                RpcSetItem(fit, inventory[fit].type, inventory[fit].quantity);
                Destroy(c.gameObject);
                nElem++;
                CmdgetItem(fit);
            }
            else if (inventory[fit].type == loot.type)
            {
                inventory[fit].quantity += loot.quantity;
                RpcSetItem(fit, inventory[fit].type, inventory[fit].quantity);
                Destroy(c.gameObject);
                CmdgetItem(fit);
            }
        }

    }

}

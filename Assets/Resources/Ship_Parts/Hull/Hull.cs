    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hull : NetworkBehaviour
{
    public string name = "null";

    public int[] allowedArmor = new int[3] {1,2,3};
    public int[] allowedEngine = new int[3] {1,2,3};

    public int healthBase;
    public int turnSpeedBase;
    public int speedBase;
    public int inventoryBase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Boolean isAllowed(int armorid)
    {
        foreach(int i in allowedArmor)
        {
            if (i == armorid) return true;
        }
        return false;
    }

    public Boolean isAllowedEngine(int armorid)
    {
        foreach (int i in allowedArmor)
        {
            if (i == armorid) return true;
        }
        return false;
    }


}

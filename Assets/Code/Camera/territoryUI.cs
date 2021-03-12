using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class territoryUI : MonoBehaviour
{
    public GameObject territory;
    // Start is called before the first frame update
    void Start()
    {
        updateColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateColor()
    {
        int n = territory.GetComponent<territory>().faction;

        switch (n)
        {
            case 0:
                gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 225, 25);
                break;
            case 1:
                gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 0, 0, 25);
                break;
            case 2:
                gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 0, 255, 25);
                break;
        }
    }
}

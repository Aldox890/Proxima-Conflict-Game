using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hangar_Entrance : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject hangar;
    public GameObject canvas;
    public GameObject hangarController;
    
    private GameObject light;
    private GameObject darkRect;
    private GameObject gameGui;
    public GameObject hangarGui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        darkRect = GameObject.Find("DarkRect");
        gameGui = GameObject.Find("GameGui");
        light = GameObject.Find("Light");
        //hangarGui = GameObject.Find("HangarGui");
    }

    bool hit = false;

    public IEnumerator OnTriggerEnter(Collider c)
    {
        if (c.transform.root.gameObject.GetComponent<Ship_Controller>()!= null && c.transform.root.gameObject.GetComponent<Ship_Controller>().isLocalPlayer && c.transform.gameObject.layer == 8)
        {
            if(c.transform.root.gameObject.GetComponent<Ship_Controller>().enterHangarTimer > 5f) {

                c.transform.root.gameObject.GetComponent<Ship_Controller>().enterHangarTimer = 0;
                yield return StartCoroutine("Fade");
                mainCamera.SetActive(false);
                yield return new WaitForSeconds(1f);
                c.transform.root.gameObject.GetComponent<Ship_Controller>().deactivate();
                gameGui.SetActive(false);
                light.SetActive(false);
                hangarController.GetComponent<hangar_controller>().hangar = hangar;
                hangarController.GetComponent<hangar_controller>().setSpaceship();
                hangarGui.SetActive(true);
                hangar.GetComponent<hangar>().hangarCamera.SetActive(true);
                hangar.GetComponent<hangar>().hangarObj = gameObject;
                yield return StartCoroutine("Show");
            }
        }
    }

    IEnumerator Fade()
    {
        for (float ft = 0f; ft <= 1; ft += 0.01f)
        {
            Color c_Color = darkRect.GetComponent<Image>().color;

            Color col = c_Color;
            col.a = ft;
            darkRect.GetComponent<Image>().color = col;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Show()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.01f)
        {
            Color c_Color = darkRect.GetComponent<Image>().color;

            Color col = c_Color;
            col.a = ft;
            darkRect.GetComponent<Image>().color = col;
            yield return new WaitForSeconds(0.01f);
        }
    }
}

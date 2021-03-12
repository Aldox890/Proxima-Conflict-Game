using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hangar_controller : MonoBehaviour
{
    public GameObject gameController;

    public GameObject mainCamera;
    public GameObject hangar;
    public GameObject canvas;

    public GameObject light;
    private GameObject darkRect;
    public GameObject gameGui;
    public GameObject hangarGui;

    private Hangar_Builder builder;

    // Start is called before the first frame update
    void Start()
    {
        darkRect = GameObject.Find("DarkRect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeHull(int n)
    {
        builder.changeHull(n);
    }
    public void changeWings(int n)
    {
        builder.changeWings(n);
    }
    public void changeEngine(int n)
    {
        builder.changeEngine(n);
    }

    public void randomizeShip()
    {
        int rand = Random.Range(1, 3);
        builder.hullID = rand;

        Hull hullData = Resources.Load<GameObject>("Ship_Parts/Hull/Hull " + builder.hullID).GetComponent<Hull>();

        builder.armorID = hullData.allowedArmor[Random.Range(0, hullData.allowedArmor.Length)];
        builder.engineID = hullData.allowedEngine[Random.Range(0, hullData.allowedEngine.Length)];
        builder.weapon1ID = Random.Range(1, 2);
        builder.weapon2ID = Random.Range(1, 2);
        builder.weapon3ID = Random.Range(1, 2);
        builder.weapon4ID = Random.Range(1, 2);
        builder.changeArmor();
    }

    public void setSpaceship()
    {
        builder = hangar.GetComponent<hangar>().spaceship.GetComponent<Hangar_Builder>();
        builder.getSpaceshipData();
        builder.changeArmor();
    }

    public void wrapperExitHangar() {
        StartCoroutine(exitHangar());
    }

    private void setOutputSpaceship()
    {
        Ship_Controller sc = gameController.GetComponent<GameController>().PlayerShip.GetComponent<Ship_Controller>();
        sc.setSpaceship(builder);
        /*
        sc.hullID = builder.hullID;
        sc.armorID = builder.armorID;
        sc.engineID = builder.engineID;
        sc.weapon1ID = builder.weapon1ID;
        sc.weapon2ID = builder.weapon2ID;
        sc.weapon3ID = builder.weapon3ID;
        sc.weapon4ID = builder.weapon4ID;
        */
    }

    public IEnumerator exitHangar()
    {

        yield return StartCoroutine("Fade");
        hangar.GetComponent<hangar>().hangarCamera.SetActive(false);
        gameGui.SetActive(true);
        hangarGui.SetActive(false);
        mainCamera.SetActive(true);
        light.SetActive(true);

        gameController.GetComponent<GameController>().PlayerShip.GetComponent<Ship_Controller>().activate();
        setOutputSpaceship();
        gameController.GetComponent<GameController>().PlayerShip.GetComponent<Ship_Controller>().enterHangarTimer = 0f;



        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("Show");
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

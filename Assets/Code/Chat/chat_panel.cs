using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chat_panel : MonoBehaviour
{
    // Start is called before the first frame update
    private chat chatScript;
    public GameObject input;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendMsg()
    {
        chatScript = GameObject.Find("GameController").GetComponent<GameController>().PlayerShip.GetComponent<chat>();
        chatScript.sendMsg();
        input.GetComponent<UnityEngine.UI.InputField>().text = "";
    }
}

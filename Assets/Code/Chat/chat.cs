using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class chat : NetworkBehaviour
{
    public GameObject text;
    public GameObject chatObj;
    // Start is called before the first frame update
    void Start()
    {
        chatObj = GameObject.Find("Chat");
        text = GameObject.Find("inputTxt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendMsg()
    {
        if(text.GetComponent<UnityEngine.UI.Text>().text != "") {
            CmdSendMessage(text.GetComponent<UnityEngine.UI.Text>().text);
        }
    }

    [Command]
    public void CmdSendMessage(string msg)
    {
        RpcSendMessage(msg);
        //string txt = chatObj.GetComponent<UnityEngine.UI.Text>().text;
        //chatObj.GetComponent<UnityEngine.UI.Text>().text = txt + text + "\n";
    }

    [ClientRpc]
    void RpcSendMessage(string msg)
    {
        string txt = chatObj.GetComponent<UnityEngine.UI.Text>().text;
        chatObj.GetComponent<UnityEngine.UI.Text>().text = txt + msg + "\n";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Test : MonoBehaviour
{

    [SerializeField] SocketIOComponent socket;

    private void Start()
    {
        socket.On("open", OnOpen);
        socket.On("connected", OnServerConnected);
        socket.Connect();
    }

    private void OnOpen(SocketIOEvent e)
    {
        Debug.Log("Open" + e.data);
        socket.Emit("enterServer");
    }
    private void OnServerConnected(SocketIOEvent e)
    {
        //var a = JsonUtility.FromJson<ConnectionData>(e.data.ToString());
        //Debug.Log("Id1: " + e.data.GetField("id"));
        //Debug.Log("Id: " + a.id);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading;
using WebSocketSharp;

public class WebSocket : MonoBehaviour
{
    private string IP = "127.0.0.1";
    private string PORT = "6000";
    private string SERVICE_NAME = "/Echo";
    public WebSocketSharp.WebSocket m_Socket = null;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            m_Socket = new WebSocketSharp.WebSocket("ws://127.0.0.1:6000/Echo");
            m_Socket.OnMessage += Recv;
            m_Socket.OnClose += CloseConnect;
            Connect();
        }
        catch(Exception e) {
            Debug.Log(e.ToString());
        }
    }

    public void Connect()
    {
        try
        {
            if(m_Socket == null || !m_Socket.IsAlive) 
            {
                Debug.Log("Client Connected");
                m_Socket.Connect();
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void CloseConnect(object sender, CloseEventArgs e)
    {
        DisconnectServer();
    }

    public void DisconnectServer()
    {
        try
        {
            if (m_Socket == null) return;

            if (m_Socket.IsAlive) m_Socket.Close();
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    [ContextMenu("Send Message")]
    public void SendMessage()
    {
        string msg = "Hello World";
        if (!m_Socket.IsAlive) return;
        try 
        {
            Debug.Log("Send: " + msg);
            m_Socket.Send(msg);
        }
        catch(Exception)
        {
            throw;
        }
    }

    public void Recv(object sender, MessageEventArgs e)
    {
        Debug.Log("Recv: " + e.Data);
    }

    private void OnApplicationQuit()
    {
        DisconnectServer();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.SceneManagement;


public class UDPSocket : MonoBehaviour
{

    private Thread receiveThread;
	private UdpClient client;
	private int port;
    private GameObject Manager;
    private string action;

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        port = 5065; 
		InitUDP(); 
    }


    private void InitUDP()
	{
		print ("UDP Initialized");

		receiveThread = new Thread (new ThreadStart(ReceiveData)); 
		receiveThread.IsBackground = true;
		receiveThread.Start ();

	}

	//	Receive Data and set to action
	private void ReceiveData()
	{
		client = new UdpClient (port);
		while (true)
		{

				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port);
				byte[] data = client.Receive(ref anyIP);
				string text = Encoding.UTF8.GetString(data);
                action = text;
                Debug.Log(text);
		}
	}

    void Update(){
        if (action != "")
        {
        	Manager.GetComponent<UDPHandller>().action = action;    
			action = "";
		}
	}

}
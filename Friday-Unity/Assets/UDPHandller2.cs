using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class UDPHandller2 : MonoBehaviour
{

    Thread receiveThread; //1
	UdpClient client; //2
	int port; //3

    public Text Display;
    private string disp;
    private bool ready;

    // Start is called before the first frame update
    void Start()
    {
        port = 5065; 
        ready = false;
		InitUDP(); 
    }


    private void InitUDP()
	{
		print ("UDP Initialized");

		receiveThread = new Thread (new ThreadStart(ReceiveData)); //1 
		receiveThread.IsBackground = true; //2
		receiveThread.Start (); //3

	}


	// 4. Receive Data
	private void ReceiveData()
	{
		client = new UdpClient (port); //1
		while (true) //2
		{
			// try
			// {
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("192.168.100.2"), port); //3
				byte[] data = client.Receive(ref anyIP); //4

				string text = Encoding.UTF8.GetString(data); //5
				print (">> " + text);
                disp = text;
                ready = true;
				Debug.Log("hi");
			// } catch(Exception e)
			// {
			// 	print (e.ToString()); //7
			// }
		}
	}

    public void Update(){
        if (ready)
        {
            Display.text = disp;
            ready = false;
        }
    }

}

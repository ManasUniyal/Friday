using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.SceneManagement;


public class tempBase : MonoBehaviour
{
    
    public bool isActive;
    private GameObject Manager;
	private string action;

    void Start()
    {
        isActive = false;
        Manager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (isActive)
        {
            action = Manager.GetComponent<UDPHandller>().action;
			Manager.GetComponent<UDPHandller>().action = "";
            if (action == "")
            {
                return;
            }
            else if (action == "1")
            {
            
            }
            else if (action == "2")
            {
            
            }
            else if (action == "3")
            {
            
            }
            else if (action == "4")
            {
            
            }
            else if (action == "NEXT")
            {
            
            }
            else if (action == "HOME")
            {
                Manager.GetComponent<CommonAction>().Home();
				Debug.Log(action);
			}
            else if (action == "PREVIOUS")
            {
            
            }
            else if (action == "SPECIAL")
            {
            
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    
    public bool isActive = true;
    private GameObject Manager;
	private string action;
    public GameObject Menu;

    void Start()
    {
        // Debug.Log("Hiiiiiiiiiiiiiiiiiiiiiiiiiii");
        // Debug.Log(System.DateTime.Now.ToString());
        Manager = GameObject.Find("GameManager");
    }

    void FixedUpdate()
    {
        Debug.Log("Hiiiiiiiiiiiiii");
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
                isActive = false;
                Menu.SetActive(true);
                Menu.GetComponent<MenuHandller>().Activate();
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

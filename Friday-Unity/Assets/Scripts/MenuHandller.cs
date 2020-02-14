using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHandller : MonoBehaviour
{
    
    public bool isActive;
    private string action;
    private GameObject Manager;
    public TextMeshProUGUI []txts;
    string [,]MenuItems = { { "Phone Call" , "Dictionary" , "News" , "Gallery" }, { "E" , "F" , "G" , "H" }, { "I" , "J", "K" , "L" } };

    void Start()
    {
        isActive = true;
        Manager = GameObject.Find("GameManager");
        Debug.Log("MenuStart");
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            action = Manager.GetComponent<UDPHandller>().action;
			Manager.GetComponent<UDPHandller>().action = null;
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
                Manager.GetComponent<Base>().isActive = true;
                gameObject.SetActive(false);
            
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

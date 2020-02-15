using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking; 

public class VideoHandller : MonoBehaviour
{
    
    public bool isActive;
    private string action;
    private GameObject Manager;
    
    //  Options
    
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("MenuStart");
        Activate();
    }

    public void Activate(){
        isActive = true;
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
            else if (action == "HOME")
            {

                isActive = false;
                Manager.GetComponent<Base>().isActive = true;
                gameObject.SetActive(false);
            
            }
            else if (action == "NEXT")
            {
                
                
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

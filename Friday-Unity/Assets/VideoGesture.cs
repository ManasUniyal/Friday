using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoGesture : MonoBehaviour
{
 
    private string action;
    private GameObject Manager;
    public GameObject videoPlayer;
    
    void Start()
    {
        Manager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if(Manager.GetComponent<UDPHandller>().vid){

        
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

                Manager.GetComponent<Base>().isActive = true;
                Manager.GetComponent<UDPHandller>().vid = false;
                videoPlayer.SetActive(false);

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

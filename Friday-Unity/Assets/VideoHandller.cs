using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking; 
using UnityEngine.Video;

public class VideoHandller : MonoBehaviour
{
    
    public bool isActive;
    private string action;
    private GameObject Manager;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI searchWord;

    //  Options
    
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("MenuStart");
        Activate();
    }

    public void Activate(){  
        isActive = true;
        videoPlayer.url = "http://10.0.0.11:5002/"+searchWord.text;
        videoPlayer.targetCameraAlpha = 0.5F;
        StartCoroutine(scr());
        Debug.Log("Hi");
    }

    IEnumerator scr(){
        videoPlayer.Play();
        yield return new WaitForSeconds(0.2f);
    }

    void Update()
    {
        Debug.Log("hi");
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
                searchWord.text = "";
                Manager.GetComponent<Base>().isActive = true;
                videoPlayer.Pause();
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

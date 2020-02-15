﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;


public class YoutubeHandller : MonoBehaviour
{

    private string action;
    public bool isActive;
    public TextMeshProUGUI meaning;
    public TextMeshProUGUI searchWord;
    private GameObject Manager;
    
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("DictionaryStart");
    }

    public void Activate(){
        isActive = true;
        searchWord.text = "Search Video";
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
            else if (action == "SPECIAL1")
            {

                StartCoroutine(scr(searchWord.text));
                searchWord.text = "";
            
            }
        }
    }


   IEnumerator scr(string searchedhWord){
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.6:7001/APIs/youtube/?word="+searchedhWord))
        {
            
            Debug.Log("Requested dictionary api for " + searchedhWord);
    
            searchWord.text = "Loading ...";
            
			yield return webRequest.SendWebRequest();
         
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log( ": Error: " + webRequest.error);
                meaning.text = webRequest.error;
            }
            else
            {

			    meaning.text = "Your Video has started downloading. You can view it after completion in video player.";
				
		    }
        }

	}

}

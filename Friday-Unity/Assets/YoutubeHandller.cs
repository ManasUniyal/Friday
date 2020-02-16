using System.Collections;
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
                searchWord.text = "";
                Manager.GetComponent<Base>().isActive = true;
                gameObject.SetActive(false);
            
            }
            else if (action == "NEXT")
            {
                
                StartCoroutine(ocr());

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
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.11:7001/APIs/youtube/?word="+searchedhWord))
        {
            
            Debug.Log("Requested dictionary api for " + searchedhWord);
            webRequest.SendWebRequest();
            searchWord.text = "";
            
            meaning.text = "Your Video has started downloading. You can view it after completion in video player.";
				
			yield return new WaitForSeconds(1f);
         
        }

	}

    IEnumerator ocr(){
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.11:7001/APIs/OCR"))
        {
            
            Debug.Log("Requested dictionary api for " );
    
            searchWord.text = "Loading ...";
            
			yield return webRequest.SendWebRequest();
         
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log( ": Error: " + webRequest.error);
    		    searchWord.text = webRequest.error;
	        }
            else
            {

				string []res = webRequest.downloadHandler.text.Split('#');
				Debug.Log(res);
				Debug.Log(res[0]);

			    searchWord.text = res[0];
		    }
        }

	}
    

}

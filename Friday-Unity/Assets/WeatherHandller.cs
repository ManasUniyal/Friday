using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class WeatherHandller : MonoBehaviour
{

    private string action;
    public bool isActive;
    public TextMeshProUGUI word;
    public TextMeshProUGUI meaning;
    public TextMeshProUGUI example;
    public TextMeshProUGUI searchWord;
    private GameObject Manager;
    
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("DictionaryStart");
        Activate();
    }

    public void Activate(){
        isActive = true;
        StartCoroutine(scr());
        searchWord.text = "";        
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
        }
    }


   IEnumerator scr(){
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.6:7001/APIs/weatherReport"))
        {
            
            Debug.Log("Requested Weather api for " );
    
            searchWord.text = "Loading ...";
            
			yield return webRequest.SendWebRequest();
         
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log( ": Error: " + webRequest.error);
                meaning.text = webRequest.error;
            }
            else
            {

				string []res = webRequest.downloadHandler.text.Split('#');
				Debug.Log(res);
				Debug.Log(res[0]);

			    word.text = res[4];
				meaning.text = res[0]+"\n"+res[1];
				example.text = res[2]+"\n"+res[3];
			
		    }
        }

	}

}

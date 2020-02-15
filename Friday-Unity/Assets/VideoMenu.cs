using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;


public class VideoMenu : MonoBehaviour
{
    public bool isActive;
    private string action;
    private GameObject Manager;
    public TextMeshProUGUI []txts;
    private string []menuItems = new string[100];
    private int activePage;
    private int numberOfPages = 3;

    //  Options
    public GameObject Dictionary;
    public GameObject News;
    public GameObject Youtube;

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("VideoStart");
        Activate();
    }

    public void Activate(){
        isActive = true;
        activePage = 0;
        StartCoroutine(Daily());
    }

    private void UpdatePage(){
        for(int i=0;i<4;i++){
            txts[i].text = menuItems[activePage,i];
        }
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
                if(activePage == 0){

                    isActive = false;
                    Dictionary.SetActive(true);
                    Dictionary.GetComponent<DictionaryHandller>().Activate();
                    gameObject.SetActive(false);        
                
                }else if(activePage == 1){

                }else if(activePage == 2){

                }
            }
            else if (action == "3")
            {
                if(activePage == 0){

                    isActive = false;
                    News.SetActive(true);
                    News.GetComponent<NewsHandller>().Activate();
                    gameObject.SetActive(false);        
                
                }else if(activePage == 1){

                }else if(activePage == 2){

                }            
            }
            else if (action == "4")
            {
                if(activePage == 0){

                    isActive = false;
                    Youtube.SetActive(true);
                    Youtube.GetComponent<YoutubeHandller>().Activate();
                    gameObject.SetActive(false);        
                
                }else if(activePage == 1){

                }else if(activePage == 2){

                }
            }
            else if (action == "HOME")
            {

                isActive = false;
                Manager.GetComponent<Base>().isActive = true;
                gameObject.SetActive(false);
            
            }
            else if (action == "NEXT")
            {
                
                activePage++;
                if (activePage >= numberOfPages){
                    activePage = numberOfPages-1;
                }
                UpdatePage();
            }
            else if (action == "PREVIOUS")
            {
            
                activePage--;
                if (activePage < 0){
                    activePage = 0;
                }
                UpdatePage();

            }
            else if (action == "SPECIAL")
            {
                //  Play
            }
        }
    }

    
    IEnumerator Daily(){
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.6:7001/APIs/videos/"))
        {
            
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
                int x = int.Parse(res[0]);
                numberOfPages = (x+3)/4;
                for (int i=0; i < x; i++){
                    menuItems[i] = res[i+1];
                }
			    
		    }
        }
        UpdatePage();
        
	}

}

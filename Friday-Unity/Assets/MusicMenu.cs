using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.Video;
using TMPro;

public class MusicMenu : MonoBehaviour
{
    public bool isActive;
    private string action;
    private GameObject Manager;
    public TextMeshProUGUI []txts;
    private string []menuItems = new string[100];
    private int activePage;
    private int numberOfPages = 3;
    public TextMeshProUGUI searchWord;
    public GameObject Video;
    public string task = "listSongs";

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("PicStart");
        Activate();
    }

    public void Activate(){
        for(int i=0;i<100;i++){
            menuItems[i]=" ";
        }
        isActive = true;
        activePage = 0;
        StartCoroutine(Daily());
    }
    
    private void UpdatePage(){
        for(int i=0;i<4;i++){
            txts[i].text = menuItems[activePage*4+i];
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
                searchWord.text = menuItems[activePage*4+0];
                PlayVideo();
            }
            else if (action == "2")
            {   
                searchWord.text = menuItems[activePage*4+1];
                PlayVideo();
            }
            else if (action == "3")
            {
                searchWord.text = menuItems[activePage*4+2];
                PlayVideo();
            }
            else if (action == "4")
            {
                searchWord.text = menuItems[activePage*4+3];
                PlayVideo();
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
            }
        }
    }

    private void PlayVideo(){
        isActive = false; 
        Video.SetActive(true);
        Video.GetComponent<MusicHandller>().Activate();
        gameObject.SetActive(false);
    }
    
    IEnumerator Daily(){
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.11:7001/"+task))
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

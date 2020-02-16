using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class MenuHandller : MonoBehaviour
{
    
    public bool isActive;
    private string action;
    private GameObject Manager;
    public TextMeshProUGUI []txts;
    public TextMeshProUGUI searchWord;
    private string [,]menuItems = { { "Phone Call" , "Dictionary" , "News" , "Youtube" }, { "Videos" , "Gallery" , "Music" , "Weather" }, { "Camera" , "Alarm", "Q/A" , "PDF Viewer" } };
    private int activePage;
    private int numberOfPages = 3;

    //  Options
    public GameObject Dictionary;
    public GameObject News;
    public GameObject Youtube;
    public GameObject Video;
    public GameObject Gallery;
    public GameObject Music;
    public GameObject Weather;
    public GameObject Alarm;
    public GameObject QA;
    public GameObject Call;
    public GameObject PDF;
    

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("MenuStart");
        Activate();
    }

    public void Activate(){
        isActive = true;
        activePage = 0;
        UpdatePage();
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
                if(activePage == 0){

                    isActive = false;
                    Call.SetActive(true);
                    Call.GetComponent<CallHandller>().Activate();
                    gameObject.SetActive(false);        
                
                }else if(activePage == 1){

                    isActive = false;
                    Video.SetActive(true);
                    Video.GetComponent<VideoMenu>().Activate();
                    gameObject.SetActive(false);        

                }else if(activePage == 2){

                    StartCoroutine(cam());

                }
            }
            else if (action == "2")
            {   
                if(activePage == 0){

                    isActive = false;
                    Dictionary.SetActive(true);
                    Dictionary.GetComponent<DictionaryHandller>().Activate();
                    gameObject.SetActive(false);        
                
                }else if(activePage == 1){

                    isActive = false;
                    Gallery.SetActive(true);
                    Gallery.GetComponent<PicMenu>().Activate();
                    gameObject.SetActive(false);        

                }else if(activePage == 2){
                    
                    isActive = false;
                    Alarm.SetActive(true);
                    Alarm.GetComponent<AlarmHandller>().Activate();
                    gameObject.SetActive(false);
                
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

                    isActive = false;
                    Music.SetActive(true);
                    Music.GetComponent<MusicMenu>().Activate();
                    gameObject.SetActive(false);   

                }else if(activePage == 2){

                    isActive = false;
                    QA.SetActive(true);
                    QA.GetComponent<QAHandller>().Activate();
                    gameObject.SetActive(false);   

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
                    
                    isActive = false;
                    Weather.SetActive(true);
                    Weather.GetComponent<WeatherHandller>().Activate();
                    gameObject.SetActive(false);   

                }else if(activePage == 2){

                    isActive = false;
                    PDF.SetActive(true);
                    PDF.GetComponent<PDFHandller>().Activate();
                    gameObject.SetActive(false);   

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
            
            }
        }
    }

       IEnumerator cam(){
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.11:7001/captureImage/"))
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
			    searchWord.text = "Shot Captured";
		    }
        }

	}


}

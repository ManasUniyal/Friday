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
    private string [,]menuItems = { { "Phone Call" , "Dictionary" , "News" , "Youtube" }, { "Videos" , "Gallery" , "Music" , "H" }, { "I" , "J", "K" , "L" } };
    private int activePage;
    private int numberOfPages = 3;

    //  Options
    public GameObject Dictionary;
    public GameObject News;
    public GameObject Youtube;
    public GameObject Video;

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

                    // isActive = false;
                    // Dictionary.SetActive(true);
                    // Dictionary.GetComponent<DictionaryHandller>().Activate();
                    // gameObject.SetActive(false);        
                
                }else if(activePage == 1){

                    isActive = false;
                    Video.SetActive(true);
                    Video.GetComponent<VideoMenu>().Activate();
                    gameObject.SetActive(false);        

                }else if(activePage == 2){

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
                    Video.SetActive(true);
                    Video.GetComponent<VideoMenu>().Activate();
                    gameObject.SetActive(false);        

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
            
            }
        }
    }

}

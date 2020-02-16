using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class MusicHandller : MonoBehaviour
{

    public bool isActive;
    private string action;
    private GameObject Manager;
    public TextMeshProUGUI searchWord;

    //  Options
    
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("PicStart");
        Activate();
    }

    public void Activate(){  
        isActive = true;
        Debug.Log(searchWord.text);
        StartCoroutine(loadMusic("http://10.0.0.11:5003/"+searchWord.text));    
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
    

    IEnumerator loadMusic(string url)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        WWW music = new WWW(url);
        yield return music;
        AudioClip lamusic = music.GetAudioClip(true, true, AudioType.MPEG);
        audioSource.clip = lamusic;
        audioSource.Play();
    }
}

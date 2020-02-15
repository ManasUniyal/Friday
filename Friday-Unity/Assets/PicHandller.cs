using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;


public class PicHandller : MonoBehaviour
{
    
    public bool isActive;
    private string action;
    private GameObject Manager;
    public RawImage YourRawImage;
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
        StartCoroutine(DownloadImage("http://10.0.0.6:5004/"+searchWord.text));    
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
    

    IEnumerator DownloadImage(string MediaUrl)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError) 
            Debug.Log(request.error);
        else{
            Color currColor = YourRawImage.color;
            currColor.a = 0.6f;
            YourRawImage.color = currColor;
            YourRawImage.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
            
    }

}

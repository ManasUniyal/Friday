using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class NewsHandller : MonoBehaviour
{

    private string action;
    public bool isActive;
    public TextMeshProUGUI word;
    public TextMeshProUGUI description;
    public TextMeshProUGUI searchWord;
    public RawImage YourRawImage;
    private GameObject Manager;

    private int maxItems;
    private int activeItem;
    
    private string []words = new string[10];
    private string []descriptions = new string[10];
    private string []pics = new string[10];

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("DictionaryStart");
    }

    public void Activate(){
        isActive = true;
        searchWord.text = "Daily Hunt";
        StartCoroutine(Daily());
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

                activeItem++;
                if(activeItem >= maxItems){
                    activeItem = maxItems-1;
                    return;
                }
                UpdatePage();

            }
            else if (action == "PREVIOUS")
            {
            
                activeItem--;
                if(activeItem < 0){
                    activeItem = 0;
                    return;
                }
                UpdatePage();

            }
            else if (action == "SPECIAL1")
            {

            
            }
        }
    }

    public void UpdatePage(){
        word.text = words[activeItem];
        description.text = descriptions[activeItem];
        StartCoroutine(DownloadImage(pics[activeItem]));
    }


   IEnumerator Daily(){
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get("http://10.0.0.6:7001/APIs/news/"))
        {
            
            Debug.Log("Requested dictionary api for " + searchWord);

			yield return webRequest.SendWebRequest();
         
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log( ": Error: " + webRequest.error);
                descriptions[0] = webRequest.error;
                words[0] = "";
                pics[0] = "";
            }
            else
            {

				string []res = webRequest.downloadHandler.text.Split('#');
				Debug.Log(res);
				Debug.Log(res[0]);

                maxItems = int.Parse(res[0]);

                for (int i=0; i<maxItems; i++){
                    words[i]=res[i*3+1];
                    descriptions[i]=res[i*3+2];
                    pics[i]=res[i*3+3];
                    Debug.Log(pics[i]);
                }
			    
		    }
        }

        UpdatePage();

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

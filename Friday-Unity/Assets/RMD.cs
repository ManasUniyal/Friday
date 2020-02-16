using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class RMD : MonoBehaviour
{
    private string action;
    public TextMeshProUGUI searchWord;
    private GameObject Manager;
    
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Debug.Log("DictionaryStart");
    }

    void FixedUpdate()
    {
            action = Manager.GetComponent<UDPHandller>().action;
			if (action != "" && action != null )
            {
                if (action[0] == 0)
                {
                Manager.GetComponent<UDPHandller>().action = null;
                string []res = action.Split('#');
				searchWord.text = res[1];
                
                }
            }       
    }

}

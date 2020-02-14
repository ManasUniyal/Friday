using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAction : MonoBehaviour
{
    public bool isActive;
    private GameObject Manager;

    void Start()
    {
        isActive = true;
        Manager = GameObject.Find("GameManager");
    }

    public GameObject MainMenu;

    public void Home(){
        if (MainMenu.activeSelf)
        {
            MainMenu.SetActive(false);    
        }else
        {
            MainMenu.SetActive(true);        
        }
        return;
    }
    
}

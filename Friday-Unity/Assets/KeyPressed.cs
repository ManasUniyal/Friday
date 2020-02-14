using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class KeyPressed : MonoBehaviour, IVirtualButtonEventHandler
{

    private GameObject button;
    private Text txt;
    private Text display;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler(this);   
        txt = GetComponentsInChildren<Text> () [0];
        display = GameObject.FindGameObjectsWithTag("Display")[0].GetComponent<Text>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb){
        Debug.Log(txt.text);
        display.text = display.text + txt.text;
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb){

    }

}

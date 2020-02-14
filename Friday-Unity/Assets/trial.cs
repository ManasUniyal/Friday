using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class trial : MonoBehaviour , IVirtualButtonEventHandler
{

    public GameObject butt1;

    // Start is called before the first frame update
    void Start()
    {
        butt1.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler(this);   
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb){
            Debug.Log("Pressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb){

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class TimeUpdate : MonoBehaviour
{
    
    public TextMeshProUGUI Date;
    public TextMeshProUGUI Time;
    private string []st = new string[10];
    private string st2;

    void Update()
    {
        st2 = System.DateTime.Now.ToString();
        st = st2.Split(' ');
        Date.text = st[0];
        Time.text = st[1];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.UI;

public class LocalIP : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = GetLocalIPv4();
        foreach (var item in GetLocalIPv4_Arry())
        {
            Debug.Log(item.ToString());
        }
    }

    public string GetLocalIPv4()
    {
        return Dns.GetHostAddresses(Dns.GetHostName())[Dns.GetHostAddresses(Dns.GetHostName()).Length-1].ToString();
    }
    public IPAddress[] GetLocalIPv4_Arry()
    {
        return Dns.GetHostAddresses(Dns.GetHostName());
    }
}

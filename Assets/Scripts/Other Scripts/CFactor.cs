using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;



public class CFactor : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void OnSelected(string id);

    [DllImport("__Internal")]
    public static extern string Get(string key);

    [DllImport("__Internal")]
    public static extern void Loaded();

    public static T Get<T>(string key)
    {
        return JsonConvert.DeserializeObject<T>(CFactor.Get(key));
    }

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public static bool CheckIfMobile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsMobile();
#else
        return true;
#endif
    }

    void Awake()
    {
        Application.targetFrameRate = 30;



    }

    void Start()
    {

    }
}

public class ComponentDataObject
{
    public string id;
    public string type;
    public string title;
    public string description;
    public int? timer;
    public string componentId;
    public string nextComponentId;
    public string icon;
    public string url;
    public string startupData;
    public string dataURL;
    public string data;
    public string audioURL;
    public string assetsURL;
    public string state;
    public float? score;
    public int? status = 0;
    public bool? active;
    public string createdAt;
    public string updatedAt;
    public string analytics;
}
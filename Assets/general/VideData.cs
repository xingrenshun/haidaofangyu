using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class VideData : MonoBehaviour
{
    public string vide1;
    public string vide2;
    public string vide3;
    public string vide4;
    public string vide5;
    public string vide6;
    public string vide7;
    public string vide8;
    public string vide9;
    public string vide10;
    public string vide11;
    public string vide12;
    public string tableScreen1;
    public string tableScreen2;
    public string tableScreen3;
    public string tableScreen4;
    public string tableScreen5;
    public string tableScreen6;

    public static VideData Instance { get; private set; }

    private VideData VidesData;

    // 替换为你的广告ID
    //private string adUnitId = "ca-app-pub-xxxxxx/xxxxxxxxxx";

    void Awake()
    {
        // 如果实例已存在，则销毁当前实例
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  // 保证该对象在场景切换时不会销毁
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetVide() 
    {
        int randomInt = Random.Range(1, 13);
        string ADStr = "";
        switch (randomInt)
        {
            case 1:
                ADStr = vide1;
                break;
            case 2:
                ADStr = vide2;
                break;
            case 3:
                ADStr = vide3;
                break;
            case 4:
                ADStr = vide4;
                break;
            case 5:
                ADStr = vide5;
                break;
            case 6:
                ADStr = vide6;
                break;
            case 7:
                ADStr = vide7;
                break;
            case 8:
                ADStr = vide8;
                break;
            case 9:
                ADStr = vide9;
                break;
            case 10:
                ADStr = vide10;
                break;
            case 11:
                ADStr = vide11;
                break;
            case 12:
                ADStr = vide12;
                break;
            default:
                ADStr = vide1;
                break;
        }

        return ADStr;
    }

    public string GetTableScreenVide()
    {
        int randomInt = Random.Range(1, 7);
        string TableScreenADStr = "";
        switch (randomInt)
        {
            case 1:
                TableScreenADStr = tableScreen1;
                break;
            case 2:
                TableScreenADStr = tableScreen2;
                break;
            case 3:
                TableScreenADStr = tableScreen3;
                break;
            case 4:
                TableScreenADStr = tableScreen4;
                break;
            case 5:
                TableScreenADStr = tableScreen5;
                break;
            case 6:
                TableScreenADStr = tableScreen6;
                break;
            default:
                TableScreenADStr = tableScreen1;
                break;
        }
        return TableScreenADStr;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTSDK;
using TTSDK.UNBridgeLib.LitJson;

public class cebian : MonoBehaviour
{
    //前往按钮
    public Button btn_Go;

    // 缓存委托，防止 IL2CPP/WebGL 下被 GC 或元数据丢失
    private System.Action onNavigateSuccess;
    private System.Action onNavigateComplete;
    private System.Action<int, string> onNavigateError;

    private void Start()
    {
        //检查是否支持侧边栏功能
        //CheckSideBarSupport();
        btn_Go.onClick.AddListener(NavigateToSideBar);

        onNavigateSuccess = OnNavigateSuccess;
        onNavigateComplete = OnNavigateComplete;
        onNavigateError = OnNavigateError;
    }

    /// <summary>
    /// 跳转到侧边栏
    /// </summary>
    public void NavigateToSideBar()
    {
        try
        {
            var data = new JsonData
            {
                ["scene"] = "sidebar",
            };
            TT.NavigateToScene(data, onNavigateSuccess, onNavigateComplete, onNavigateError);
        }
        catch (System.Exception e)
        {
            Debug.LogError("跳转到侧边栏异常: " + e.Message);
        }
    }

    private void OnNavigateSuccess()
    {
        Debug.Log("navigate to scene success");
    }

    private void OnNavigateComplete()
    {
        Debug.Log("navigate to scene complete");
    }

    private void OnNavigateError(int errCode, string errMsg)
    {
        Debug.Log($"navigate to scene error, errCode:{errCode}, errMsg:{errMsg}");
    }
}

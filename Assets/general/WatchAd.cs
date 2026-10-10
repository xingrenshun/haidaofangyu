using System;
using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;

public class WatchAd : MonoBehaviour
{
    public static WatchAd Instance { get; private set; }

    private WatchAd WatchAds;

    // 缓存外部传入回调与 SDK 事件委托，防止 IL2CPP/WebGL 下目标丢失
    private Action<bool> currentOnAdCompleted;
    private RewardedAdClosedDelegate onRewardClose;
    private AdErrorDelegate onRewardError;

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

        onRewardClose = OnRewardClose;
        onRewardError = OnRewardError;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WatchAdAndPerformAction(Action<bool> onAdCompleted)
    {
        currentOnAdCompleted = onAdCompleted;

        string videoAdid = VideData.Instance.GetVide();
        TTSDK.TTRewardedVideoAd r = AdMgr.Instance.ShowVideoAdNew(videoAdid);
        if (r == null)
        {
            Debug.LogError("激励视频广告实例为空");
            currentOnAdCompleted?.Invoke(false);
            return;
        }

        // 创建激励视频关闭回调
        r.OnClose += onRewardClose;

        // 创建报错回调
        r.OnError += onRewardError;

        // 展示广告（新版本必须调用，不然无法展示广告）
        r.Show();
    }

    private void OnRewardClose(bool isAdCompleted, int val)
    {
        Debug.Log("关闭广告 ，是否已经看完：" + isAdCompleted);
        currentOnAdCompleted?.Invoke(isAdCompleted);
    }

    private void OnRewardError(int code, string errorMessage)
    {
        Debug.Log("code: " + code + " 错误信息: " + errorMessage);
        currentOnAdCompleted?.Invoke(false);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

// ���ƽ̨����ö��
public enum AdPlatformType
{
    [InspectorName("�������")]
    TickTok,
    [InspectorName("TopOn���")]
    TopOn,
    [InspectorName("΢�Ź��")]
    WebChat,
}


/// <summary>
/// ������
/// </summary>
public class AdManager : MonoBehaviour
{
   // public GameObject aaa;
    public static AdManager Instance;
    [Header("ƽ̨")]
    public AdPlatformType adPlatformType = AdPlatformType.TickTok;
    [Header("С��ϷID")]
    public string AppId;
    [Header("�������λ")]
    public List<string> videoAdIdList = new();
    [Header("�������λ")]

    public List<string> interAdIdList = new();
    private IAdPlatform platFormIns = null;
    private float lastAdClickTime;

     void Awake()
    {
        Debug.Log("[AdManager] Awake 被调用");
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("[AdManager] 检测到重复实例，销毁当前对象");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // 延迟一帧初始化，确保 Unity / WebGL / 抖音环境已就绪
        StartCoroutine(DelayInitPlatform());
    }

    private System.Collections.IEnumerator DelayInitPlatform()
    {
        yield return null; // 等一帧
        Debug.Log($"[AdManager] Start 被调用，adPlatformType={adPlatformType}, AppId={AppId}");
        this.InitPlatform();
        this.lastAdClickTime = Time.time;
    }

    private void InitPlatform()
    {
        if (this.platFormIns != null)
            return;

        if (this.AppId == "")
        {
            Debug.LogWarning("[AdManager] 请填写APPID");
        }

        if (this.videoAdIdList == null || this.videoAdIdList.Count == 0)
        {
            Debug.LogWarning("[AdManager] 视频广告位ID列表为空, 将使用默认广告位");
        }

        if (this.interAdIdList == null || this.interAdIdList.Count == 0)
        {
            Debug.LogWarning("[AdManager] 插屏广告位ID列表为空, 将使用默认广告位");
        }

        try
        {
            switch (adPlatformType)
            {
                case AdPlatformType.TickTok:
                    var tiktok = new TikTokPlatform(this.AppId, this.GetRandVideoAdId(), this.GetRandScreenAdId());
                    platFormIns = tiktok; // 先赋值，即使 Init 失败也保留平台实例
                    tiktok.Init();
                    Debug.Log("[AdManager] 抖音广告平台初始化完成");
                    break;
                case AdPlatformType.TopOn:
                    Debug.LogWarning("[AdManager] TopOn广告平台尚未实现");
                    break;
                default:
                    Debug.LogWarning($"[AdManager] 暂不支持的 adPlatformType: {adPlatformType}");
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[AdManager] 初始化广告平台异常：{e}");
        }

        if (this.platFormIns == null)
        {
            Debug.LogError($"[AdManager] 广告平台初始化失败：{this.adPlatformType}");
        }
    }

    private void EnsurePlatformInitialized()
    {
        if (this.platFormIns == null)
        {
            Debug.Log("[AdManager] 广告平台未初始化，尝试延迟初始化");
            this.InitPlatform();
        }
        else
        {
            Debug.Log("[AdManager] 广告平台已初始化");
        }
    }

    /// <summary>
    /// ���Ų������
    /// </summary>
    public void ShowInterstitialAd()
    {
        this.EnsurePlatformInitialized();
        this.platFormIns?.ShowInterstitialAd(this.GetRandScreenAdId());
    }

    /// <summary>
    /// 跳转场景
    /// </summary>
    /// <param name="onSuccess">成功回调</param>
    /// <param name="onFail">失败回调</param>
    public void NavigateToScene(Action onSuccess = null, Action<string> onFail = null)
    {
        this.EnsurePlatformInitialized();
        if (this.platFormIns == null)
        {
            Debug.LogError("广告平台实例未初始化，无法跳转场景");
            onFail?.Invoke("广告平台实例未初始化");
            return;
        }
        
        try
        {
            this.platFormIns.NavigateToScene(onSuccess, onFail);
        }
        catch (Exception e)
        {
            Debug.LogError($"跳转场景时发生异常：{e.Message}");
            onFail?.Invoke($"跳转场景时发生异常：{e.Message}");
        }
    }
    public void ShareGame(Action<bool> callBack = null)
    {
        this.EnsurePlatformInitialized();
        this.platFormIns?.ShareGame(callBack);
    }

    /// <summary>
    /// ������Ƶ���
    /// </summary>
    /// <param name="onSuccess">�ɹ��ص�</param>
    /// <param name="onFail">ʧ�ܻص�</param>
    public void ShowRewardedVideoAd(Action onSuccess, Action<string> onFail)
    {
        if (!this.CheckTimeLimit())
            return;

        this.EnsurePlatformInitialized();
        if (this.platFormIns == null)
        {
            Debug.LogError("广告平台实例未初始化，无法显示广告");
            onFail?.Invoke("广告平台实例未初始化");
            return;
        }
        
        try
        {
            this.platFormIns.ShowRewardedVideoAd(this.GetRandVideoAdId(), onSuccess, onFail);
        }
        catch (Exception e)
        {
            Debug.LogError($"显示广告时发生异常：{e.Message}");
            onFail?.Invoke($"显示广告时发生异常：{e.Message}");
        }
    }


    // ��ȡ����������ID
    private string GetRandVideoAdId()
    {
        if (this.videoAdIdList.Count == 0)
        {
            Debug.LogWarning("videoAdIdList is empty, using default ad id");
            return "f32bf9970ah83mq1kg"; // 默认广告ID，防止空字符串
        }
        int index = UnityEngine.Random.Range(0, this.videoAdIdList.Count);
        return this.videoAdIdList[index];
    }

    // ��ȡ����Ĳ������ID
    private string GetRandScreenAdId()
    {
        if (this.interAdIdList.Count == 0)
        {
            Debug.LogWarning("interAdIdList is empty, using default ad id");
            return "dk217ga5e5b48uo4gh"; // 默认广告ID，防止空字符串
        }
        int index = UnityEngine.Random.Range(0, this.interAdIdList.Count);
        return this.interAdIdList[index];
    }

    // ����x���ڹ��ֻ�������һ��
    private bool CheckTimeLimit()
    {
        if (Time.time - lastAdClickTime < 0.5f)
        {
            Debug.LogWarning("广告点击过于频繁，请稍后再试。");
            return false;
        }
        lastAdClickTime = Time.time;
        return true;
    }
}

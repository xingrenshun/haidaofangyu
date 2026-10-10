using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHealth : BaseHealth {

    // 缓存广告回调委托，避免 IL2CPP/WebGL 下 lambda 生成的显示类委托目标丢失
    private Action onRewardSuccess;
    private Action<string> onRewardFail;
    private int pendingCoinAmount;

	public override void TakeDamage (int _damage)
	{
		CurrentHealth = GameManager.Instance.MyCurrentHealth;

		if (CurrentHealth > 0) 
		{
			AudioManager.Instance.PlayEnemyAudio ();
			
			CurrentHealth -= _damage;
			UIManager.Instance.SetTextMyHealth (CurrentHealth);
		}

		if (CurrentHealth <= 0) 
		{
			CurrentHealth = 0;
			GameManager.Instance.IsPause = true;
			GameManager.Instance.IsFight = true;
			GameManager.Instance.IsDefeat = true;
			//you lost
			UIManager.Instance.ShowPanelDefeat();
		}

		GameManager.Instance.MyCurrentHealth = CurrentHealth;
	}

    public void TouchWatchAds()
    {
        Time.timeScale = 0;
        onRewardSuccess = OnHealthAdSuccess;
        onRewardFail = OnHealthAdFail;
        AdManager.Instance.ShowRewardedVideoAd(onRewardSuccess, onRewardFail);
    }

    private void OnHealthAdSuccess()
    {
        // 只有看完广告才执行以下操作
        CurrentHealth = GameManager.Instance.MyCurrentHealth;

        if (CurrentHealth < 100)
        {
            AudioManager.Instance.PlayEnemyAudio();

            CurrentHealth += 10;
            if (CurrentHealth > 100)
            {
                CurrentHealth = 100;
            }
            UIManager.Instance.SetTextMyHealth(CurrentHealth);
        }
        GameManager.Instance.MyCurrentHealth = CurrentHealth;
        MaidianNew.MaidianBackCallback(" ");
        Time.timeScale = 1;
    }

    private void OnHealthAdFail(string error)
    {
        Debug.LogError($"广告播放失败：{error}");
        Time.timeScale = 1;
    }

    // 增加解锁海盗的金币数量
    public void AddCoinUnlockPirates(int amount)
    {
        pendingCoinAmount = amount;
        onRewardSuccess = OnCoinAdSuccess;
        onRewardFail = OnCoinAdFail;
        AdManager.Instance.ShowRewardedVideoAd(onRewardSuccess, onRewardFail);
        //// 增加金币数量
        //GameManager.Instance.CoinUnlockPirates += amount;
        //// 保存到PlayerPrefs
        //PlayerPrefs.SetInt("CoinUnlockPirates", GameManager.Instance.CoinUnlockPirates);
        //// 更新UI显示
        //UIManager.Instance.SetTextCoinUnLockPirates(GameManager.Instance.CoinUnlockPirates);
        //// 播放金币音效
        //AudioManager.Instance.PlayCoinAudio();
        //// 输出日志
        //Debug.Log($"已增加 {amount} 个解锁海盗金币，当前总数：{GameManager.Instance.CoinUnlockPirates}");
    }

    private void OnCoinAdSuccess()
    {
        // 增加金币数量
        GameManager.Instance.CoinUnlockPirates += pendingCoinAmount;
        // 保存到PlayerPrefs
        PlayerPrefs.SetInt("CoinUnlockPirates", GameManager.Instance.CoinUnlockPirates);
        // 更新UI显示
        UIManager.Instance.SetTextCoinUnLockPirates(GameManager.Instance.CoinUnlockPirates);
        // 输出日志
        Debug.Log($"已增加 {pendingCoinAmount} 个解锁海盗金币，当前总数：{GameManager.Instance.CoinUnlockPirates}");
    }

    private void OnCoinAdFail(string error)
    {
        Debug.LogError($"广告播放失败：{error}");
    }
}

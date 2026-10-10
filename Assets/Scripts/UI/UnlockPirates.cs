using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnlockPirates : MonoBehaviour , IPointerClickHandler  {

	public bool isUnlock;

	public int Price;

	public GameObject CoinObject;
	public GameObject TextCoin;
	public GameObject TextRecruited;

	void Update()
	{
		if (PlayerPrefs.GetInt (this.gameObject.name) == 1) 
		{
			isUnlock = true;
			this.gameObject.transform.GetChild (0).GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			CoinObject.SetActive (false);
			TextCoin.SetActive (false);
			TextRecruited.SetActive (true);
		}
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		if (PlayerPrefs.GetInt ("CoinUnlockPirates") >= Price && !isUnlock) 
		{
			AudioManager.Instance.PlayBuyAudio ();

			//save unlock character
			isUnlock = true;
			PlayerPrefs.SetInt(this.gameObject.name , 1);
			GameManager.Instance.CoinUnlockPirates -= Price;
			PlayerPrefs.SetInt ("CoinUnlockPirates", GameManager.Instance.CoinUnlockPirates);
			UIManager.Instance.SetTextCoinUnLockPirates (GameManager.Instance.CoinUnlockPirates);

			if (PlayerPrefs.GetInt ("FirtsPlay") == 0) 
			{
				UIManager.Instance.Knife3.SetActive (false);
				UIManager.Instance.PanelTutorialClickMap.SetActive (true);
			}
		}
	}

	#endregion
}

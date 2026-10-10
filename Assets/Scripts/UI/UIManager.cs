using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public static UIManager instance = null;
	public static UIManager Instance { get { return instance; } }

	public LevelManager levelManager;

	public GameObject PanelSelectLevel;
	public GameObject PanelUnlockCharacter;
	public GameObject PanelGamePause;
	public GameObject PanelVictory;
	public GameObject PanelEffectFadeInFadeOut;
	public GameObject PanelDefeat;
	public GameObject ButtonSettingAudio;
	public GameObject ButtonSettingAudioGameplay;

	public GameObject PanelTutorialUnlockCharacter;
	public GameObject Knife3;
	public GameObject PanelTutorialClickMap;
	public GameObject Knife2;
	public GameObject PanelTutorial;
	public GameObject PanelTutorial2;
	public GameObject EnemyTutorial;


	public GameObject[] CoinsVictory;

	public Text TextMyHealth;
	public Text TextCurrentEnemy;
	public Text TextCoinsBuyPirates;
	public Text TextCoinUnlockPiraves;

	public int CurrentLevel;


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		// 强制设置为横屏模式
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;

		StartCoroutine (PlayEffectFadeIn ());

		if (PlayerPrefs.GetInt ("FirtsPlay") == 0) 
		{
			PanelTutorialUnlockCharacter.SetActive (true);
			GameManager.Instance.CoinUnlockPirates = 2;
			PlayerPrefs.SetInt ("CoinUnlockPirates", GameManager.Instance.CoinUnlockPirates);
			SetTextCoinUnLockPirates (GameManager.Instance.CoinUnlockPirates);
		}
	}

	void Update()
	{
		UpdateAudioSetting ();
	}

	void UpdateAudioSetting()
	{
		if (PlayerPrefs.GetInt ("Sound") == 0) 
		{
			AudioManager.Instance.AudioSource.mute = false;
			ButtonSettingAudio.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			ButtonSettingAudioGameplay.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		} 
		else 
		{
			AudioManager.Instance.AudioSource.mute = true;
			ButtonSettingAudio.GetComponent<Image> ().color = new Color (1, 1, 1, 0.3f);
			ButtonSettingAudioGameplay.GetComponent<Image> ().color = new Color (1, 1, 1, 0.3f);
		}
	}

	//on click button unlock character
	public void OnClickButtonUnlockCharacter()
	{
		AudioManager.Instance.PlayClickAudio ();

		PanelSelectLevel.SetActive (false);
		PanelUnlockCharacter.SetActive (true);

		SetTextCoinUnLockPirates (GameManager.Instance.CoinUnlockPirates);

		if (PlayerPrefs.GetInt ("FirtsPlay") == 0) 
		{
			PanelTutorialUnlockCharacter.SetActive (false);
			Knife3.SetActive (true);
		}
	}

	//on click button map
	public void OnClickButtonMap()
	{
		AudioManager.Instance.PlayClickMap();

		PanelSelectLevel.SetActive (true);
		PanelUnlockCharacter.SetActive (false);

		if (PlayerPrefs.GetInt ("FirtsPlay") == 0) 
		{
			Knife2.SetActive (true);
			PanelTutorialClickMap.SetActive (false);
		}
	}

	public void OnClickButtonPauseGame()
	{
		AudioManager.Instance.PlayClickAudio ();

		PanelGamePause.SetActive (true);
		GameManager.Instance.IsPause = true;
		Time.timeScale = 0f;
		//AdsControl.Instance.showAds ();
	}

	public void OnClickButtonRusume()
	{
		AudioManager.Instance.PlayClickAudio ();

		PanelGamePause.SetActive (false);
		GameManager.Instance.IsPause = false;
		Time.timeScale = 1f;
	}

	public void OnClickButtonHome()
	{
		AudioManager.Instance.PlayClickAudio ();
		AudioManager.Instance.PlayMenuMusic ();

		SceneManager.LoadScene ("GamePlay");
		Time.timeScale = 1f;
	}

	public void OnClickButtonRetry()
	{
		AudioManager.Instance.PlayClickAudio ();

		GameObject[] Pirates = GameObject.FindGameObjectsWithTag ("Pirates");

		for (int i = 0; i < Pirates.Length; i++) 
		{
			Destroy (Pirates [i]);
		}

		GameObject[] Enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < Enemy.Length; i++) 
		{
			Destroy (Enemy [i]);
		}

		GameManager.Instance.MyCurrentHealth = 100;

		PanelGamePause.SetActive (false);
		Time.timeScale = 1f;
		GameManager.Instance.IsPause = false;
	}

	public void SetTextMyHealth(int _myCurrentHealth)
	{
		TextMyHealth.text = _myCurrentHealth.ToString ();
	}

	public void OnClickButtonLevel(int _level)
	{
		AudioManager.Instance.PlayClickAudio ();
		AudioManager.Instance.NoPlayMusic ();

		CurrentLevel = _level - 1;

		StartCoroutine (PlayEffectFadeOut ());
		StartCoroutine (PlayEffectFadeIn ());
		StartCoroutine (CoDelayTimePlayMusic ());

		GameManager.Instance.timer = 0f;
		GameManager.Instance.isVictory = false;
		GameManager.Instance.IsFight = true;
		GameManager.Instance.IsPause = false;
		PanelSelectLevel.SetActive (false);

		//select level && spawnenemy

		if (PlayerPrefs.GetInt ("FirtsPlay") != 0) 
		{
			levelManager.SelectLevel (CurrentLevel);
		}


		levelManager.TimeSpawnAllEnemysInLevel (CurrentLevel);

		//set enemy number
		SetTextEnemy (levelManager.CountEnemysInLevel);
		GameManager.Instance.CurrentEnemy = levelManager.CountEnemysInLevel;

		//set current health = 100
		GameManager.Instance.MyCurrentHealth = GameManager.Instance.MyMaxHealth;
		SetTextMyHealth (GameManager.Instance.MyCurrentHealth);

		HideCoinsVictory ();

		//set coin begin
		GameManager.Instance.CoinBuyPirates = GameManager.Instance.CoinsBeginLevel[CurrentLevel];
		SetTextCoinBuyPirates (GameManager.Instance.CoinBuyPirates);

		GameManager.Instance.IsDefeat = false;

		if (PlayerPrefs.GetInt ("FirtsPlay") == 0) 
		{
			Knife2.SetActive (false);
			PanelTutorial.SetActive (true);
		}

		Time.timeScale = 1f;
	}

	public void SpawnEnemyCurrentLevel()
	{
		levelManager.SelectLevel (CurrentLevel);
	}

	IEnumerator CoDelayTimePlayMusic()
	{
		yield return new WaitForSeconds (0.3f);
		AudioManager.Instance.PlayMidNightAudio ();
		yield return new WaitForSeconds (3f);
		AudioManager.Instance.PlayGamePlayMusic ();
	}

	private void HideCoinsVictory()
	{
		for (int i = 0; i < 5; i++) 
		{
			CoinsVictory [i].SetActive (false);
		}
	}

	public void SetTextEnemy(int _currentEnemy)
	{
		TextCurrentEnemy.text = _currentEnemy.ToString ();
	}

	public void ShowVictory()
	{
		AudioManager.Instance.PlayWinAudio ();
		
		PanelVictory.SetActive (true);
		StartCoroutine (CoSetCoinVictory ());
	}

	private IEnumerator CoSetCoinVictory()
	{
		
		int CountCoins = GameManager.Instance.MyCurrentHealth / 20;

		//save coins
		if (CountCoins > PlayerPrefs.GetInt (CurrentLevel.ToString ())) 
		{
			//set coin unlock piraves
			GameManager.Instance.CoinUnlockPirates += CountCoins - PlayerPrefs.GetInt (CurrentLevel.ToString ());
			PlayerPrefs.SetInt ("CoinUnlockPirates", GameManager.Instance.CoinUnlockPirates);
			SetTextCoinUnLockPirates (GameManager.Instance.CoinUnlockPirates);

			//save coin level
			PlayerPrefs.SetInt (CurrentLevel.ToString (), CountCoins);

		}

		for (int i = 0; i < CountCoins; i++) 
		{
			yield return new WaitForSeconds (1f);
			//CoinsVictory [i].SetActive(true);

			AudioManager.Instance.PlayCoinAudio ();
		}
	}

	public void OnClickButtonNext()
	{
		AudioManager.Instance.PlayMenuMusic ();

		SceneManager.LoadScene ("GamePlay");
		Time.timeScale = 1f;
	}

	public void OnClickButtonRetryGame()
	{
		AudioManager.Instance.PlayClickAudio ();

		//delete pirates
		GameObject[] Pirates = GameObject.FindGameObjectsWithTag ("Pirates");

		for (int i = 0; i < Pirates.Length; i++) 
		{
			Destroy (Pirates [i]);
		}

		//delete enemy
		GameObject[] Enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < Enemy.Length; i++) 
		{
			Destroy (Enemy [i]);
		}

		PanelVictory.SetActive (false);
		PanelDefeat.SetActive (false);

		//spawn enemy
		OnClickButtonLevel (CurrentLevel + 1);
	}

	public void SetTextCoinBuyPirates(int _coins)
	{
		TextCoinsBuyPirates.text = _coins.ToString ();
	}

	public void SetTextCoinUnLockPirates(int _coin)
	{
		if (TextCoinUnlockPiraves != null)
		{
			TextCoinUnlockPiraves.text = _coin.ToString ();
		}
		else
		{
			Debug.LogWarning("TextCoinUnlockPiraves 对象已销毁，无法更新文本显示");
		}
	}

	public IEnumerator PlayEffectFadeIn ()
	{

		Image effectImage = PanelEffectFadeInFadeOut.GetComponent<Image> ();
		PanelEffectFadeInFadeOut.SetActive (true);

		for (float i = 5; i >= 0; i--) {
			effectImage.color = new Color (0, 0, 0, i / 5f);
			yield return new WaitForSeconds (0.1f);
		}

		PanelEffectFadeInFadeOut.SetActive (false);
	}

	public IEnumerator PlayEffectFadeOut ()
	{
		Image effectImage = PanelEffectFadeInFadeOut.GetComponent<Image> ();
		PanelEffectFadeInFadeOut.SetActive (true);

		for (float i = 0; i <= 5; i++) {
			yield return new WaitForSeconds (0.1f);
			effectImage.color = new Color (0, 0, 0, i / 5f);
		}
	}

	public void ShowPanelDefeat()
	{
		AudioManager.Instance.PlayDefeatAudio ();
		
		PanelDefeat.SetActive (true);
	}

	public void OnClickBackToMainMenu()
	{
		AudioManager.Instance.PlayClickAudio ();

		SceneManager.LoadScene ("MainMenu");
		Time.timeScale = 1f;
	}

	public void OnClickButtonChangeSettingAudio()
	{
		AudioManager.Instance.ChangeSettingAudio ();
	}

	public void OnClickNextTutorial()
	{
		Vector3 spawnPos = new Vector3 (10, 0.9f, 0);

		PanelTutorial.SetActive (false);
		GameObject EnemyClone = (GameObject)Instantiate (EnemyTutorial, spawnPos, transform.rotation);

		PanelTutorial2.SetActive (true);
	}

	public void HideAllToturial()
	{
		PanelTutorialUnlockCharacter.SetActive (false);
		Knife3.SetActive (false);
		PanelTutorialClickMap.SetActive (false);
		Knife2.SetActive (false);
		PanelTutorial.SetActive (false);
		PanelTutorial2.SetActive (false);
	}
}

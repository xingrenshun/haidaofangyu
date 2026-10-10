using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public static GameManager Instance { get { return instance; } }

	public LevelManager levelManager;

	public bool IsPause;
	public bool IsFight;
	public bool IsDragCharacter;
	public bool isVictory;
	public bool IsDefeat;

	public string LightWayName;

	public int MyMaxHealth;
	public int MyCurrentHealth;
	public int CurrentEnemy;
	public int CoinBuyPirates;
	public int CoinUnlockPirates;
	public int EnemyDie;


	public int[] CoinsBeginLevel;
	public int[] DropRateItem;

	public float timer;
	private float timeOneSecond;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		//test
		//PlayerPrefs.DeleteAll();
	}

	void Start()
	{
		MyCurrentHealth = MyMaxHealth;
		UIManager.Instance.SetTextMyHealth (MyCurrentHealth);
		CoinUnlockPirates = PlayerPrefs.GetInt ("CoinUnlockPirates");
	}

	void Update()
	{
		if(IsFight && !IsPause)
		{
			timer += Time.deltaTime;
			timeOneSecond += Time.deltaTime;
			AddCoinBuyPirates ();
		}

		Victory ();
	}

	public void Victory()
	{
		if (IsFight && !IsPause && timer >= levelManager.TimeSpawnAllEnemyInCurrentLevel && FindObjectOfType<EnemyHealth>() == null  && !isVictory && !IsDefeat) 
		{
			//win
			timer = 0f;
			isVictory = true;
			Debug.Log ("Victory");
			UIManager.Instance.ShowVictory ();
		//	AdsControl.Instance.showAds ();
		}
	}

	public void AddCoinBuyPirates()
	{
		if (timeOneSecond > 1) 
		{
			timeOneSecond = 0;
			CoinBuyPirates += 2;
			UIManager.Instance.SetTextCoinBuyPirates (CoinBuyPirates);
		}
	}
}

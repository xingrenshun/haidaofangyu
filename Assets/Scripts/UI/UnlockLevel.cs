using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevel : MonoBehaviour {

	public bool IsUnLock;

	public Text TextCoinVictory;

	public string NameLevel;

	void Update()
	{
		SetCoinVictoryInLevel (NameLevel);

		Unlock ();
	}

	void SetCoinVictoryInLevel(string nameLevel)
	{
		TextCoinVictory.text = PlayerPrefs.GetInt (nameLevel).ToString();
	}

	void Unlock()
	{
		int lastlevel = int.Parse (NameLevel) - 1;

		if (PlayerPrefs.GetInt (lastlevel.ToString()) != 0 && !IsUnLock) 
		{
			IsUnLock = true;
			this.transform.GetChild (1).gameObject.SetActive (true);
			this.gameObject.GetComponent<Button> ().enabled = true;
		}

		if (PlayerPrefs.GetInt (NameLevel) != 0) 
		{
			this.transform.GetChild (1).gameObject.SetActive (false);
			this.gameObject.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			this.transform.GetChild (0).gameObject.SetActive (true);
		}
	}
}

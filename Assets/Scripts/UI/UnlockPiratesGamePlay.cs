using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPiratesGamePlay : MonoBehaviour {

	public bool isUnlock;

	public GameObject Pirates;
	public GameObject Banner;
	public GameObject Lock;

	void Update()
	{
		UnlockPirates ();
	}

	void UnlockPirates()
	{
		if (PlayerPrefs.GetInt (this.gameObject.name) == 1) 
		{
			isUnlock = true;
			Pirates.SetActive (true);
			Banner.SetActive (true);
			Lock.SetActive (false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour {

	public GameObject PanelEffectFadeInFadeOut;
	public GameObject ButtonSettingAudio;

	void Start()
	{
		AudioManager.Instance.PlayMenuMusic ();
		// 强制设置为横屏模式
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
	}

	void Update()
	{
		UpdateAudioSetting ();
	}

	public void OnClickButtonPlay()
	{
		AudioManager.Instance.PlayClickAudio ();

		StartCoroutine (PlayEffectFadeOut ());
	}

	public IEnumerator PlayEffectFadeOut()
	{
		Image effectImage = PanelEffectFadeInFadeOut.GetComponent<Image> ();
		PanelEffectFadeInFadeOut.SetActive (true);

		for (float i = 0; i <= 5; i++) 
		{
			yield return new WaitForSeconds (0.1f);
			effectImage.color = new Color (0, 0, 0, i/5f);
		}

		yield return new WaitForSeconds (0.1f);
		SceneManager.LoadScene ("GamePlay");
		Time.timeScale = 1f;
	}


	void UpdateAudioSetting()
	{
		if (PlayerPrefs.GetInt ("Sound") == 0) 
		{
			AudioManager.Instance.AudioSource.mute = false;
			ButtonSettingAudio.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		} 
		else 
		{
			AudioManager.Instance.AudioSource.mute = true;
			ButtonSettingAudio.GetComponent<Image> ().color = new Color (1, 1, 1, 0.3f);
		}
	}

	public void OnClickButtonChangeSettingAudio()
	{
		
		AudioManager.Instance.ChangeSettingAudio ();
		Debug.Log (PlayerPrefs.GetInt ("Sound"));
	}
}

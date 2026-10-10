using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;
	public static AudioManager Instance { get { return instance; } }

	public AudioSource AudioSource;

	public AudioClip GamePlayMusic;
	public AudioClip MenuMusic;
	public AudioClip DrinkAudio;
	public AudioClip SwordAudio;
	public AudioClip HookAudio;
	public AudioClip RoomPlaceAudio;
	public AudioClip BombThrowAudio;
	public AudioClip RumAudio;
	public AudioClip BarrelPlaceAudio;
	public AudioClip PiratAudio;
	public AudioClip ShootAudio;
	public AudioClip VamprimeAudio;
	public AudioClip[] TakePirart;
	public AudioClip WinAudio;
	public AudioClip MidNightAudio;
	public AudioClip EnemyAudio;
	public AudioClip CrabAudio;
	public AudioClip DefeatAudio;
	public AudioClip DeadAudio;
	public AudioClip DeadPeopleAudio;
	public AudioClip DeadCrabAudio;
	public AudioClip CoinAudio;
	public AudioClip GoldAudio;
	public AudioClip BuyAudio;
	public AudioClip BombaAudio;
	public AudioClip BombAudio;
	public AudioClip CarabinAudio;
	public AudioClip ClickAudio;
	public AudioClip ClickMapAudio;


	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
		else 
		{
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	void Update()
	{
		UpdateAudioSetting ();
	}

	public void PlayGamePlayMusic()
	{
		AudioSource.clip = GamePlayMusic;
		AudioSource.Play ();
		AudioSource.loop = true;
	}

	public void NoPlayMusic()
	{
		AudioSource.clip = null;
	}

	public void PlayMenuMusic()
	{
		AudioSource.clip = MenuMusic;
		AudioSource.Play ();
		AudioSource.loop = true;
	}

	public void PlayDrinkAudio()
	{
		AudioSource.PlayOneShot (DrinkAudio);
	}
		
	public void PlaySwordAudio()
	{
		AudioSource.PlayOneShot (SwordAudio);
	}

	public void PlayHookAudio()
	{
		AudioSource.PlayOneShot (HookAudio);
	}

	public void PlayRoomPlaceAudio()
	{
		AudioSource.PlayOneShot (RoomPlaceAudio);
	}

	public void PlayBombThrowAudio()
	{
		AudioSource.PlayOneShot (BombThrowAudio);
	}

	public void PlayRumAudio()
	{
		AudioSource.PlayOneShot (RumAudio);
	}

	public void PlayBarrelPlaceAudio()
	{
		AudioSource.PlayOneShot (BarrelPlaceAudio);
	}

	public void PlayPiratAudio()
	{
		AudioSource.PlayOneShot (PiratAudio);
	}

	public void PlayShootAudio()
	{
		AudioSource.PlayOneShot (ShootAudio);
	}

	public void PlayVamprimeAudio()
	{
		AudioSource.PlayOneShot (VamprimeAudio);
	}

	public void PlayTakePirart(int _i)
	{
		AudioSource.PlayOneShot (TakePirart[_i]);
	}
	public void PlayWinAudio()
	{
		AudioSource.PlayOneShot (WinAudio);
	}

	public void PlayMidNightAudio()
	{
		AudioSource.PlayOneShot (MidNightAudio);
	}

	public void PlayEnemyAudio()
	{
		AudioSource.PlayOneShot (EnemyAudio);
	}

	public void PlayCrabAudio()
	{
		AudioSource.PlayOneShot (CrabAudio);
	}

	public void PlayDefeatAudio()
	{
		AudioSource.PlayOneShot (DefeatAudio);
	}
	public void PlayDeadAudio()
	{
		AudioSource.PlayOneShot (DeadAudio);
	}

	public void PlayDeadPeopleAudio()
	{
		AudioSource.PlayOneShot (DeadPeopleAudio);
	}
	public void PlayDeadCrabAudio()
	{
		AudioSource.PlayOneShot (DeadCrabAudio);
	}

	public void PlayCoinAudio()
	{
		AudioSource.PlayOneShot (CoinAudio);
	}

	public void PlayGoldAudio()
	{
		AudioSource.PlayOneShot (GoldAudio);
	}

	public void PlayBuyAudio()
	{
		AudioSource.PlayOneShot (BuyAudio);
	}

	public void PlayBombaAudio()
	{
		AudioSource.PlayOneShot (BombaAudio);
	}

	public void PlayBombAudio()
	{
		AudioSource.PlayOneShot (BombAudio);

	}

	public void PlayCarabinAudio()
	{
		AudioSource.PlayOneShot (CarabinAudio);
	}

	public void PlayClickAudio()
	{
		AudioSource.PlayOneShot (ClickAudio);
	}

	public void PlayClickMap()
	{
		AudioSource.PlayOneShot (ClickMapAudio);
	}

	public void ChangeSettingAudio()
	{
		if (PlayerPrefs.GetInt ("Sound") != 0) 
		{
			AudioSource.mute = true;
			PlayerPrefs.SetInt ("Sound", 0);
		} 
		else 
		{
			AudioSource.mute = false;
			PlayerPrefs.SetInt ("Sound", 1);
		}
	}

	void UpdateAudioSetting()
	{
		if (PlayerPrefs.GetInt ("Sound") != 0) 
		{
			AudioSource.mute = false;
		} 
		else 
		{
			AudioSource.mute = true;
		}
	}
}

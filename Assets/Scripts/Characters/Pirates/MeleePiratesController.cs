using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePiratesController : MonoBehaviour {

	public float Speed;
	public float CountDown;
	public float VelocityFightOff;

	public int Damage;

	private Animator animator;

	private Rigidbody2D rb;

	private float timer;

	private bool isPower;
	private bool isPowerUp;

	void Start()
	{
		timer = 0;
		isPower = false;
		isPowerUp = false;
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void Update()
	{
		if (GameManager.Instance.IsFight && !GameManager.Instance.IsPause) 
		{
			timer += Time.deltaTime;

			if (!GameManager.Instance.isVictory) {
				Move ();
			}

			PlayAnimationWin ();
		}
	}

	void Move()
	{
		if (!isPower) {
			transform.Translate (Vector3.right * Speed * Time.deltaTime);
		} else {
			if (isPowerUp) 
			{
				transform.Translate (Vector3.right * 2 * Speed * Time.deltaTime);
			}
		}
	}

	void PlayAnimationWin()
	{
		if (GameManager.Instance.isVictory) 
		{
			animator.SetTrigger ("Win");
		}
	}
		
	//attack enemy
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			//take damage enemy
			if (timer >= CountDown) 
			{
				BaseHealth healthEnemy = col.gameObject.GetComponent<BaseHealth> ();

				if (healthEnemy.CurrentHealth > 0) 
				{
					timer = 0f;

					AudioManager.Instance.PlaySwordAudio ();

					//take damage & play animation

					if (!isPowerUp ) 
					{
						animator.Play ("Attack");
					}

					healthEnemy.TakeDamage (Damage);

					//fight off
					if (healthEnemy.CurrentHealth > 0) 
					{
						Rigidbody2D rbEnemy = col.gameObject.GetComponent<Rigidbody2D> ();
						rbEnemy.velocity = new Vector2 (VelocityFightOff, rbEnemy.velocity.y);
						StartCoroutine (CoStopFightOff (rbEnemy));
					}
				}
			}
		}

		if (col.gameObject.tag == "Undead") 
		{
			Destroy (gameObject);
		}


		if (col.gameObject.tag == "Power") 
		{
			AudioManager.Instance.PlayDrinkAudio ();

			isPower = true;
			Destroy (col.gameObject);
			animator.Play ("PowerUp");
			StartCoroutine (CoWaitPowerUp ());
		}

		if (col.gameObject.tag == "Coin") 
		{
			AudioManager.Instance.PlayGoldAudio ();

			GameManager.Instance.CoinBuyPirates += 30;
			UIManager.Instance.SetTextCoinBuyPirates (GameManager.Instance.CoinBuyPirates);
			Destroy (col.gameObject);
		}
	}

	//stop fight off
	IEnumerator CoStopFightOff(Rigidbody2D _rbEnemy)
	{
		yield return new WaitForSeconds (0.7f);
		_rbEnemy.velocity = new Vector2 (0, _rbEnemy.velocity.y);
	}

	IEnumerator CoWaitPowerUp()
	{
		yield return new WaitForSeconds (1f);
		isPowerUp = true;
		Damage = 3 * Damage;
	}
}
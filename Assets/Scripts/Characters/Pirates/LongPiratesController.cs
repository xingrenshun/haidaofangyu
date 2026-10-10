using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPiratesController : MonoBehaviour {

	public float Speed;
	public float CountDown;
	public float VelocityFightOff;

	public int Damage;

	public float distanceShoot;

	public GameObject Bullet;

	public Transform ShootPositionGun1;
	public Transform ShootPositionGun2;

	private Animator animator;

	private Rigidbody2D rb;

	private float timer;

	private bool isPower;
	private bool isPowerUp;
	private bool isAttack;

	private LayerMask layerEnemy;

	void Start()
	{
		timer = CountDown;
		isPower = false;
		isPowerUp = false;
		isAttack = false;
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		layerEnemy = LayerMask.GetMask ("Enemy");
	}

	void Update()
	{
		if (GameManager.Instance.IsFight && !GameManager.Instance.IsPause) 
		{
			timer += Time.deltaTime;

			if (!GameManager.Instance.isVictory && !isAttack) {
				Move ();
			}

			PlayAnimationWin ();

			if (!isPower) 
			{
				Attack ();
			}
		}
	}

	void Move()
	{
		if (!isPower) 
		{
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
			BaseHealth healthEnemy = col.gameObject.GetComponent<BaseHealth> ();

			if (isPower) 
			{
				healthEnemy.Die ();
			}

			if (healthEnemy.CurrentHealth > 0) 
			{
				//fight off
				Rigidbody2D rbEnemy = col.gameObject.GetComponent<Rigidbody2D> ();
				rbEnemy.velocity = new Vector2 (VelocityFightOff, rbEnemy.velocity.y);
				StartCoroutine (CoStopFightOff (rbEnemy));
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

	void Attack()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right, distanceShoot ,layerEnemy);

		if (hit.collider != null) 
		{
			if (hit.collider.gameObject.tag == "Enemy" && timer > CountDown) 
			{
				isAttack = true;
				timer = 0f;
				animator.Play ("Attack");
			} 
		}
		else 
		{
			isAttack = false;
		}
	}

	public void Shoot(int _bunletCount)
	{
		if (_bunletCount == 2) 
		{
			StartCoroutine (CoDelayShootBunlet ());
		}

		if (_bunletCount == 1) 
		{
			GameObject BulletCloneGun1 = (GameObject)Instantiate (Bullet, ShootPositionGun1.position, transform.rotation);
		}

	}

	IEnumerator CoDelayShootBunlet()
	{
		AudioManager.Instance.PlayShootAudio ();
		GameObject BulletCloneGun1 = (GameObject)Instantiate (Bullet, ShootPositionGun1.position, transform.rotation);

		yield return new WaitForSeconds (0.2f);
		AudioManager.Instance.PlayShootAudio ();
		GameObject BulletCloneGun2 = (GameObject)Instantiate (Bullet, ShootPositionGun2.position, transform.rotation);
	}
}

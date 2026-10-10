using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour {

	public float Speed;
	public float CountDown;
	public float VelocityFightOff;

	public int Damage;

	private Animator animator;

	private Rigidbody2D rb;

	private float timer;

	private bool isSpawn;

	void Start()
	{
		timer = 0f;
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void Update()
	{

		if (GameManager.Instance.IsFight && !GameManager.Instance.IsPause) 
		{
			timer += Time.deltaTime;
			Move ();
		}
	}

	void Move()
	{
		transform.Translate (Vector3.left * Speed * Time.deltaTime);
	}

	//attack enemy
	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag == "Pirates") 
		{
			//take damage pirates
			if (timer >= CountDown) 
			{
				BaseHealth healthPirates = col.gameObject.GetComponent<BaseHealth> ();

				if (healthPirates.CurrentHealth > 0) 
				{
					timer = 0f;
					animator.Play ("Attack");
					healthPirates.TakeDamage (Damage);

					if (healthPirates.CurrentHealth > 0) 
					{
						Rigidbody2D rbPirates = col.gameObject.GetComponent<Rigidbody2D> ();
						rbPirates.velocity = new Vector2 (-VelocityFightOff, rbPirates.velocity.y);
						StartCoroutine (CoStopFightOff (rbPirates));
					}
				}
			}
		}

		if (col.gameObject.tag == "MyHealth") 
		{
			if (timer >= CountDown) 
			{
				timer = 0f;
				col.gameObject.GetComponent<BaseHealth> ().TakeDamage (10);
				Destroy (gameObject);
			}
		}

		if (col.gameObject.tag == "Undead"  && !isSpawn && GameManager.Instance.CurrentEnemy > 0)  
		{
			isSpawn = true;
			GameManager.Instance.CurrentEnemy--;
			UIManager.Instance.SetTextEnemy (GameManager.Instance.CurrentEnemy);
		}

		if (col.gameObject.tag == "Bullet") 
		{
			rb.velocity = new Vector2 (1.5f, rb.velocity.y);
			StartCoroutine (CoStopFightOff (rb));
		}
	}

	//stop fight off
	IEnumerator CoStopFightOff(Rigidbody2D _rbPirates)
	{
		yield return new WaitForSeconds (0.5f);
		_rbPirates.velocity = new Vector2 (0, rb.velocity.y);
	}
}

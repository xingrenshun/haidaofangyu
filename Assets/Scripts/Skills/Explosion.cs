using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public int Damage;

	public float VelocityFightOff;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			BaseHealth enemyHealth = col.gameObject.GetComponent<BaseHealth> ();

			if (enemyHealth.CurrentHealth > 0) 
			{
				enemyHealth.TakeDamage (Damage);

				if (enemyHealth.CurrentHealth > 0) 
				{
					Rigidbody2D rbEnemy = col.gameObject.GetComponent<Rigidbody2D> ();
					rbEnemy.velocity = new Vector2 (VelocityFightOff, rbEnemy.velocity.y);
					StartCoroutine (CoStopFightOff (rbEnemy));
				}
			}
		}
	}

	//stop fight off
	IEnumerator CoStopFightOff(Rigidbody2D _rbEnemy)
	{
		yield return new WaitForSeconds (0.7f);
		_rbEnemy.velocity = new Vector2 (0, _rbEnemy.velocity.y);
	}
}

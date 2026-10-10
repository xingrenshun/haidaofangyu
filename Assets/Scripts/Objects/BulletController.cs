using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float Speed;
	public int Damage;

	public void Update()
	{
		transform.Translate (Vector3.right * Speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			BaseHealth healthEnemy = col.gameObject.GetComponent<BaseHealth> ();

			if (healthEnemy.CurrentHealth > 0) 
			{
				TakeDamageEnemy (healthEnemy , col);

				Destroy (gameObject);
			}
		}
	}

	//take damage 
	public virtual void TakeDamageEnemy(BaseHealth _healthEnemy, Collider2D _col)
	{
		_healthEnemy.TakeDamage (Damage);
	}
}

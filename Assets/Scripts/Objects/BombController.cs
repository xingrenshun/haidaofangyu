using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : BulletController {

	public GameObject ExplosionObj;

	public override void TakeDamageEnemy (BaseHealth _healthEnemy, Collider2D _col)
	{
		base.TakeDamageEnemy (_healthEnemy, _col);

		SpawnExplosion (_col);
	}

	void SpawnExplosion(Collider2D _col)
	{
		AudioManager.Instance.PlayBombAudio ();

		GameObject ExplosionClone = (GameObject)Instantiate (ExplosionObj, _col.gameObject.transform.position, transform.rotation);

		Destroy (ExplosionClone, 1f);
	}
}

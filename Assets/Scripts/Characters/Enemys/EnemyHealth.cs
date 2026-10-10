using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth {

	public GameObject[] Items;

	public override void Die ()
	{
		base.Die ();
		this.gameObject.GetComponent<MeleeEnemyController> ().enabled = false;

		AudioManager.Instance.PlayDeadAudio ();

		GameManager.Instance.EnemyDie++;

		DropItem ();
	}

	void DropItem()
	{
		if (GameManager.Instance.EnemyDie >= GameManager.Instance.DropRateItem[UIManager.Instance.CurrentLevel]) 
		{
			GameManager.Instance.EnemyDie = 0;

			GameObject ItemClone = (GameObject)Instantiate (Items [Random.Range (0, 2)], transform.position, transform.rotation);
		}
	}


}

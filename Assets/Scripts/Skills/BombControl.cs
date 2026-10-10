using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour 
{
	public GameObject ExposionFX;

	public float TimeToDestroy;

	private float timer;

	void Start()
	{
		timer = 0f;
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= TimeToDestroy) 
		{
			AudioManager.Instance.PlayCarabinAudio ();

			GameObject ExposionClone = (GameObject)Instantiate (ExposionFX, transform.position, transform.rotation);
			Destroy (ExposionClone, 1f);

			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			GameObject ExposionClone = (GameObject)Instantiate (ExposionFX, transform.position, transform.rotation);

			col.gameObject.GetComponent<BaseHealth> ().Die ();

			AudioManager.Instance.PlayCarabinAudio ();

			Destroy (ExposionClone, 1f);
			Destroy (gameObject);
		}
	}
}

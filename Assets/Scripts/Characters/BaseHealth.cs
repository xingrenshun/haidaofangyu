using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour {

	public int MaxHealth;
	public int CurrentHealth;

	public GameObject HealthSlide;

	protected Animator animator;

	void Start()
	{
		CurrentHealth = MaxHealth;

		animator = GetComponent<Animator> ();
	}

	public virtual void TakeDamage(int _damage)
	{
		if (CurrentHealth > 0) 
		{
			CurrentHealth -= _damage;
			HealthSlide.SetActive (true);

			UpdateHealthSlide (CurrentHealth, MaxHealth);
		}

		if (CurrentHealth <= 0) 
		{
			CurrentHealth = 0;
			Die ();
		}
	}

	void UpdateHealthSlide(int _currentHealth, int _maxHealth)
	{
		HealthSlide.transform.GetChild (0).localScale = new Vector3 ( (float)_currentHealth / (float) _maxHealth, 1, 1);
	}

	public virtual void Die()
	{
		//die
		animator.SetTrigger ("Die");
		Destroy (gameObject, 1f);

		HealthSlide.SetActive (false);
	}
}

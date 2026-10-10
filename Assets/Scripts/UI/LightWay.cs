using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightWay : MonoBehaviour {

	void Update()
	{
		ShowLightWay ();
	}

	public void ShowLightWay()
	{
		if (GameManager.Instance.IsDragCharacter) 
		{
			if ( this.gameObject.name == GameManager.Instance.LightWayName) 
			{
				this.gameObject.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,1);
			} 
			else 
			{
				this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
			}
		} else 
		{
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
		}
	}
}

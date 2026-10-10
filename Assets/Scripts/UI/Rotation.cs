using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	public int Speed;

	private float i;

	void Update () 
	{
		i  += Time.deltaTime;
		transform.rotation = Quaternion.Euler (0, 0, -i * Speed);
	}
}

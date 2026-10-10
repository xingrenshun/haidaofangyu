using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePiratesHealth : BaseHealth {

	public override void Die ()
	{
		base.Die ();

		AudioManager.Instance.PlayDeadPeopleAudio ();

		this.gameObject.GetComponent<MeleePiratesController> ().enabled = false;
	}
}

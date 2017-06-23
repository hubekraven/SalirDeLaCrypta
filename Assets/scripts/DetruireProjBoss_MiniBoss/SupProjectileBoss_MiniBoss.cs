using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupProjectileBoss_MiniBoss : MonoBehaviour
{
	public float pointsDommage =1;

	void OnTriggerEnter2D (Collider2D coll){
		GameObject.Destroy (this.gameObject);
		//Debug.Log (coll.gameObject.transform.parent.name);

		Rigidbody2D rbTouche = coll.gameObject.GetComponent <Rigidbody2D>();
		if (coll.gameObject.transform.parent) {
			if (coll.gameObject.transform.tag == "Player") {
				rbTouche.SendMessageUpwards ("Toucher", pointsDommage, SendMessageOptions.RequireReceiver);
			}

		}
	}
}

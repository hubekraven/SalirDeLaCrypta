using UnityEngine;
using System.Collections;

public class bombe : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D coll){

		if(coll.gameObject.transform.tag == "Player"){

			GameObject.Destroy (this.gameObject);
		}
	}
}

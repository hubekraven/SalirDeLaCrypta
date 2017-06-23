using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	public CircleCollider2D zoneExplosion;



	void explose(){
		zoneExplosion.enabled = true;
	
	}
	void OnTriggerEnter2D(Collider2D coll) {
		/*if (coll.gameObject.tag == "Enemy")
			coll.gameObject.SendMessage("ApplyDamage", 10);
		IF()
		*/
	}

	void finAnimation(){
		GameObject.Destroy (this.gameObject);

	}
}

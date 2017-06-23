using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {


	private Rigidbody2D rb;
	private bool valid = false;
	private AudioSource boom;

	public Effector2D zoneEffector;
	public CircleCollider2D zoneExplosion;

	void Start(){
		boom = GetComponent<AudioSource> ();
	}

	void explose(){
		boom.Play ();
		zoneExplosion.enabled = true;
		zoneEffector.enabled = true;
		valid = true;
	}

	void finAnimation(){
		GameObject.Destroy (this.gameObject);

	}

	void OnTriggerEnter2D (Collider2D coll){

		if (coll && valid) {
			Debug.Log (coll.gameObject.name);
			Rigidbody2D rbTouche = coll.gameObject.GetComponent <Rigidbody2D>();
			rbTouche.SendMessageUpwards ("Toucher", 1, SendMessageOptions.RequireReceiver);
		}
	}
}

using UnityEngine;
using System.Collections;

///Script permet au contact de l'explosion de la bombe de déactive le sprite de de la fissure ainsi que le collider du passage secret 
/// et dactive le sprite du passage de la porte sécrete///
public class ScriptPassageSecret : MonoBehaviour {
	//-- Variables-- //
	private Transform _passageSecret;

	// Use this for initialization
	void Start () {
		_passageSecret = GetComponent <Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//active la porte secrete à la detection de la collision après l'explosion de la bombe
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.name == "bombe_anim_0(Clone)") {
			_passageSecret.GetChild (2).gameObject.SetActive (false);
			_passageSecret.GetChild (1).gameObject.SetActive (true);
			this._passageSecret.GetComponent <BoxCollider2D>().enabled=false;
			Destroy (_passageSecret.GetComponent<Rigidbody2D>());
		}
		
	}
}

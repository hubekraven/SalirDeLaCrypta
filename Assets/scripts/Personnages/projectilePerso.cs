using UnityEngine;
using System.Collections;

public class projectilePerso : MonoBehaviour {

	private AudioSource destruction;

	public float pointsDommage;

	void Start(){
		pointsDommage =1;
		destruction = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D (Collision2D coll){
		destruction.Play ();
		this.transform.localScale = new Vector3 (0,0,0); 
		GameObject.Destroy (this.gameObject, 1);

		//Debug.Log (coll.gameObject.transform.parent.name);

		Rigidbody2D rbTouche = coll.gameObject.GetComponent <Rigidbody2D>();
		if (coll.gameObject.transform.parent) {
			if ((coll.gameObject.transform.parent.name == "mesEnnemis")||(coll.gameObject.layer== 13)) {
				rbTouche.SendMessageUpwards ("Toucher", pointsDommage, SendMessageOptions.RequireReceiver);
			}

		}
	}

}

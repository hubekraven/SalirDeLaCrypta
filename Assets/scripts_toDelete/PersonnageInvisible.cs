using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageInvisible : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	void FixedUpdate () {
		

	}


	void OnTriggerEnter2D (Collider2D coll){


		if(coll.gameObject.tag == "Finish"){

			GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.5f);
			GetComponent<BoxCollider2D> ().enabled = false;
			Invoke ("attendreSecondes",5);

		}

	}



	void attendreSecondes() {



		GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
		GetComponent<BoxCollider2D> ().enabled = true;
		Debug.Log("Alpha normal");

	}



}

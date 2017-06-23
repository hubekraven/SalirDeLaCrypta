using UnityEngine;
using System.Collections;

public class HabiliteTaupe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate () {
		

	}

	void OnTriggerEnter2D (Collider2D coll){

		//Debug.Log (coll.gameObject.name);
		if(coll.gameObject.name == "Perso"){

			Invoke ("attendreSecondes",2); // comme il y a deux collider , 
											//un sur la tete et un autre sur les pieds, j ai mis 2 secondes pour la distruction de l'item habilite
											//pour que la collision soit complete entre l item , la tete et les pieds.

		}

	}



	void attendreSecondes() {


		GameObject.Destroy (this.gameObject);

		Debug.Log("Alpha normal");

	}

}

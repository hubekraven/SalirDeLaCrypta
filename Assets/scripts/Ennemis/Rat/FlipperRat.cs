using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperRat : MonoBehaviour {
	//pour le flip - https://www.youtube.com/watch?v=FHQyPgccD4M
	public bool directionDroite;
	private GameObject heros;
	//trouver la postition du rat
	private Vector2 positionRat;
	private GameObject personnage;
	//private Transform positionRat;
	//trouver la postition du personnage
	private Vector2 positionPerso;

	// Use this for initialization
	void Start () {
		directionDroite = false;
		personnage = GameObject.Find ("Persos");

		foreach (Transform perso in personnage.transform) {
			if (perso.gameObject.activeSelf == true) {
				heros = perso.gameObject;
			}
		}

		//heros = GameObject.FindWithTag("Player");//trouver le personnage
		//position du rat
		positionRat = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(heros){
			//position heros
			positionPerso = heros.transform.position;
		}
		//Debug.Log ("position du heros : " + positionPerso.x);
		//Debug.Log ("position du rat : " + positionRat.x);

		if((positionPerso.x > positionRat.x) && !directionDroite){
			Flipper ();
		}

		if((positionPerso.x < positionRat.x) && directionDroite){ 
			Flipper ();
		}
	}

	void Flipper(){
		if(!directionDroite || directionDroite){
			directionDroite = !directionDroite;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}

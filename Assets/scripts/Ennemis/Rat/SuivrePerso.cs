using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuivrePerso : MonoBehaviour {
	public float DeplVitesse = 2f;
	private Transform playerCible;
	private Transform go;
	private GameObject personnage;
	void Start(){
		personnage = GameObject.Find ("Persos");

		foreach (Transform perso in personnage.transform) {
			if (perso.gameObject.activeSelf == true) {
				go = perso;
			}
		}
	}


	// Update is called once per frame
	void Update () {
		if (playerCible == null) {


			if (go != null) {
				playerCible = go.transform;
			}
		}

		if (playerCible == null)
			return;

		transform.position = Vector2.MoveTowards(transform.position, playerCible.position, DeplVitesse * Time.deltaTime);
	}
}

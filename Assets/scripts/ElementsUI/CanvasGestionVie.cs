using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGestionVie : MonoBehaviour {
	public float nbVie;
	public personnage scriptNahua; //prend le scrip personnage
	public personnage scriptYucan; //prend le scrip personnage
	private personnage playerScript;
	private GameObject coeur;
	private Transform compteurVie;
	private float vieTotal;
	private GameObject perso;
	private Transform player;
	private float vieIniJoueur;

	// Use this for initialization
	void Start () {
		perso = GameObject.Find ("Perso");
		foreach (Transform persoChild in perso.transform) {
			if(persoChild.gameObject.activeSelf == true){
				player = persoChild;
				if(player.name == "Nahua"){
					scriptNahua = player.GetComponent<personnage> () as personnage;
				//	vieTotal = scriptNahua.nbVieMax;
					playerScript = scriptNahua;
					//Debug.Log (vieTotal);
				} else if(player.name == "Yucan"){
					scriptYucan = player.GetComponent<personnage> () as personnage;
					//vieTotal = scriptYucan.nbVieMax;
					playerScript = scriptYucan;
					//Debug.Log (vieTotal);
				}
			}
		}
		Debug.Log (player.name);

		nbVie = playerScript.nbVie;
		compteurVie = GetComponent<Transform> ();
		vieTotal = compteurVie.childCount;
		for(int i = 0; i < nbVie; i++){
			coeur = compteurVie.GetChild (i).gameObject;
			//Debug.Log (coeur);
			coeur.SetActive(true);
			//coeur.SetActive = true;
		}

		vieIniJoueur = playerScript.nbVieMax;
	}
	
	// Update is called once per frame
	void Update () {
		nbVie = playerScript.nbVie;

		updateNbCoeur ();
	}

	void updateNbCoeur(){
		nbVie = playerScript.nbVie;
	//	Debug.Log("nbVie" + nbVie);
		vieTotal = compteurVie.childCount;
	//	Debug.Log("vieTotal" + vieTotal);
		if (nbVie > vieIniJoueur) {
			for (int i = 0; i < nbVie; i++) {
				coeur = compteurVie.GetChild (i).gameObject;
				coeur.SetActive (true);
			}
		} else {
			while(nbVie < vieIniJoueur){
				coeur = compteurVie.GetChild (Mathf.FloorToInt(nbVie)).gameObject;
				coeur.SetActive (false);
				nbVie--;
			}
		}/*else if(nbVie <= vieIniJoueur){
			for(int i = Mathf.FloorToInt(nbVie); i > 0f; i--){
				coeur = compteurVie.GetChild (i).gameObject;
				coeur.SetActive (false);
			}
		}*/
	}
}

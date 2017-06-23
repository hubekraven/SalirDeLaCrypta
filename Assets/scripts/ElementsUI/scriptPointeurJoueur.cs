using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPointeurJoueur : MonoBehaviour {

	private GameObject _Personnage;
	private Transform _perso;
	private Transform _pointeur;
	private float coordX;
	private float coordY;
	private float coordZ;
	private Vector3 nouvellePosition;
	// Use this for initialization
	void Start () {
		
		_pointeur = GetComponent <Transform> ();
		_Personnage = GameObject.Find ("Persos");


		foreach (Transform child in _Personnage.transform) {
			if (child.gameObject.activeSelf == true) {
				_perso = child;
			}
		}
		coordX = _perso.position.x;
		coordY = _perso.position.y;
		coordZ = -20f;

		//_pointeur.parent = _perso;
		//nouvellePosition=new Vector3(coordX,coordY,coordZ);
		//_pointeur.position = nouvellePosition;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

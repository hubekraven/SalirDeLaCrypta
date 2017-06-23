using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class serpentCorps : MonoBehaviour {

	//==Variables==//

	public float vitesse = 0.2f;
	public float _valeur;//variable d'adition sur axe X

	private Transform _corps;
	private Vector2 nouvellePosition;//prochaine position du corps
	private bool dirDroite;
	private float x_depart;//position initial de l'objet

	// Use this for initialization
	void Start () {
		_corps = GetComponent <Transform> ();


		//_facteur = 1.2f;
		dirDroite = true;
	}
	
	// Update is called once per frame
	void Update () {
		deplacement ();
	}

	//gere le deplacement du corps vers la droite et la gauche
	void deplacement(){
		x_depart = _corps.parent.position.x;
		if (dirDroite) {
			nouvellePosition = new Vector2 (x_depart + _valeur, _corps.position.y);
			_corps.position = Vector2.MoveTowards (_corps.position, nouvellePosition, vitesse * Time.deltaTime);
			//Debug.Log ("DISTANCE: "+ Vector2.Distance (_corps.position,nouvellePosition));

			if(Vector2.Distance (_corps.position,nouvellePosition)==0){
				dirDroite = !dirDroite;
			}
		}

		if (!dirDroite) {
			nouvellePosition = new Vector2 (x_depart - _valeur, _corps.position.y);
			_corps.position = Vector2.MoveTowards (_corps.position, nouvellePosition, vitesse * Time.deltaTime);
				if(Vector2.Distance (_corps.position,nouvellePosition)==0){
				dirDroite = !dirDroite;
			}
		}
	}//fin de la fonctionDeplacement
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossSerpent : MonoBehaviour {
	private Transform monTransform;
	private Transform _Salle;
	private float vieRestante;
	public int _nbvie=5;
	private Transform serpent;
	private int Taille;
	private float bouclier=10;
	private bool bouclierActif = true;
	Color couleurBase;
	// Use this for initialization
	void Start () {
		serpent = GetComponent <Transform> ();
		Taille = serpent.parent.childCount;
		vieRestante = _nbvie * Taille;
		Debug.Log ("Serpent :" + vieRestante);
		couleurBase = GetComponent<SpriteRenderer> ().color;

	}

	void changeCouleur(){
		if (bouclierActif)
			//serpent.GetComponent<SpriteRenderer> ().color = new Color (0.0F, 0.4F, 0.4F);
		serpent.GetComponent<SpriteRenderer> ().color = couleurBase;
		else
			//serpent.GetComponent<SpriteRenderer> ().color = couleurBase;
			serpent.GetComponent<SpriteRenderer> ().color = new Color (0.7F, 0.0F, 0.4F);
	}
	// Update is called once per frame
	void Update () {
		if (bouclier <= 0) {
			bouclierActif = false;
			//serpent.GetComponent<SpriteRenderer> ().color = couleurBase;
			serpent.GetComponent<SpriteRenderer> ().color = new Color (0.7F, 0.0F, 0.4F);
		} else {
			bouclierActif = true;
			serpent.GetComponent<SpriteRenderer> ().color = couleurBase;
		}
	}

	void OnCollisionEnter2D(Collision2D other){

	}
	void Toucher (float dmg){
		bouclier -= 1;
		Debug.Log ("Bouclier :" + bouclier);
		if (bouclierActif == false) {
			
			vieRestante -= dmg;
			//serpent.GetComponent<SpriteRenderer> ().color = Color.red;//new Color(0.5F, 0.8F, 0.4F);
			Debug.Log ("Vie RESTANTE :" + vieRestante);
			Transform dernierElement = serpent.parent.GetChild (Taille - 1);
			if ((vieRestante % 5)==0) {
				if (dernierElement.name != "serpentTete") {
					//Debug.Log(dernierElement);
					Destroy (dernierElement.gameObject);
					Taille -= 1;
					bouclier = 10;
					//serpent.GetComponent<SpriteRenderer> ().color = couleurBase;
				} 
				else if (dernierElement.name == "serpentTete") {
					Debug.Log(dernierElement.parent);
					Destroy (transform.gameObject);
					Destroy (transform.parent.gameObject);
					//Destroy (transform.gameObject);
				}
			}

		}

	}

}

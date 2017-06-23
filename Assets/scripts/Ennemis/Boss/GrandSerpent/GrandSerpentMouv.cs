using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

public class GrandSerpentMouv : MonoBehaviour
{
	//==Variables==//
	public ScriptDimensionSalle _dimensionSalleScript;
	public TeteGrandSerpent _teteGrandSerpent;
	public float vitesseVerticale ;//Vitesse appliquee au mouvement du serpent verticalement.

	private Transform monTransform;
	private Rigidbody2D rb2d;
	private Transform serpent;
	private Transform _dernierElement;
	private GameObject _perso;
	private Transform maCible;
	private int _tailleSerpent;
	private float Y_Min;//Valeur minimum du deplacement vertical.
	private float Y_Max;//Valeur maximum du deplacement vertical.
	private float acceleration;
	private float vitesse;
	private float positionCibleX;
	private float positionCibleY;
	private float positionInitialX;//position initial de l'objet
	private float positionInitialY;//position initial de l'objet
	private bool directionHaut;//Direction du serpent du haut vers le bas et vis virsa.
	private bool _changeDirection;
	private bool moveStart = false;//pour verifier initiation du 1er deplacement
	private Vector3 positionFinal; 
	float vitessePrecedente;
	// Use this for initialization
	void Start ()
	{
		_dimensionSalleScript = GetComponent<ScriptDimensionSalle> () as ScriptDimensionSalle;
		directionHaut = false;
		serpent = GetComponent <Transform> ();
		_teteGrandSerpent = GetComponent<TeteGrandSerpent> () /*as TeteGrandSerpent*/;
		//accelerationVitesse = _teteGrandSerpent.acelerationVitesse;
		_tailleSerpent = serpent.childCount;
		rb2d = GetComponent<Rigidbody2D> ();
		_perso = GameObject.Find ("Persos");//recuper le gameobject perso
		positionInitialX = serpent.position.x;
		positionInitialY = serpent.position.y;
		acceleration = 2.0f;
		//vitesseVerticale = 2f;
		//va chercher le personnage active dans perso et fait de lui ma cible
		foreach (Transform child in _perso.transform) {
			if (child.gameObject.activeSelf == true) {
				maCible = child;
			}
		}

		_changeDirection = false;
	}


	// Update is called once per frame
	void Update ()
	{
		Deplacement ();//appel de la fonction de deplacement
	}

	void ChangeVitesse(float nouvVitesse)
	{
		this.acceleration = nouvVitesse;
	}

	//------gestion deplacement
	void Deplacement ()
	{
		
		vitesseVerticale = acceleration;
		Debug.Log ("VITESSE: " + vitesseVerticale);
		Vector2 lepoint = new Vector2 (positionInitialX, _dimensionSalleScript.minY - 9f);

		_dernierElement = serpent.GetChild (0);//recuper la Tete du serpent
		positionCibleX = maCible.position.x;//recupere la position en x du personnage

		if (moveStart == true) {
			
			if ((_dernierElement.position.y <= _dimensionSalleScript.maxY + 20f) && (directionHaut)) {
				positionFinal = new Vector3 (serpent.position.x, _dimensionSalleScript.maxY + 20f, 0f);
				serpent.position = Vector3.MoveTowards (serpent.position, positionFinal, vitesseVerticale * Time.deltaTime);

				if ((Vector3.Distance (positionFinal, serpent.position) <= 1.5f) && (directionHaut)) {
					serpent.position = new Vector3 (positionCibleX, _dimensionSalleScript.minY + 20f, 0f);
					directionHaut = false;
					Tourne ();	
					serpent.gameObject.SetActive (true);
				}
			}  
			if ((_dernierElement.position.y >= _dimensionSalleScript.minY - 20f) && (!directionHaut)) {
				positionFinal = new Vector3 (serpent.position.x, _dimensionSalleScript.minY - 20f, 0f);
				serpent.position = Vector3.MoveTowards (serpent.position, positionFinal, vitesseVerticale * Time.deltaTime);

				if (Vector3.Distance (positionFinal, serpent.position) <= 1.5f) {
					serpent.position = new Vector3 (positionCibleX, _dimensionSalleScript.minY - 20f, 0f);
					directionHaut = true;
					Tourne ();	
					serpent.gameObject.SetActive (true);
				}
			}
		} else {
			positionFinal = new Vector3 (positionInitialX, _dimensionSalleScript.minY - 20f, 0f);
			serpent.position = Vector3.MoveTowards (serpent.position, positionFinal, vitesseVerticale * Time.deltaTime);

			if ((Vector3.Distance (positionFinal, serpent.position) <= 1.2f) && (!directionHaut)) {
				serpent.position = new Vector3 (positionCibleX, _dimensionSalleScript.minY - 20f, 0f);
				directionHaut = true;
				moveStart = true;
				Tourne ();	
				serpent.gameObject.SetActive (true);
			}
				
		}
	}
//FIN de le gestion deplacement

	//debut de la methode pour tourne () change la direction de deplacement du serpent
	void Tourne ()
	{
		serpent.gameObject.SetActive (false);
		Vector3 dir = serpent.localScale;
		dir.y *= -1;
		serpent.localScale = dir;	
	}


	//pose action
	IEnumerator Pause(float vitesse){
		vitessePrecedente = acceleration;
		//Debug.Log ("Vitesse Precedente" + vitessePrecedente);
		acceleration=0f;
		foreach (Collider2D _colliders in serpent.GetComponentsInChildren <Collider2D>()) {

			_colliders.enabled = false;
		}
		//vitesseVerticale = vitessePrecedente;
		//serpent.position = Vector3.MoveTowards (serpent.position, positionFinal, 0 * Time.deltaTime);
		yield return new WaitForSeconds (3f);// temps d'attente
		//serpent.position = Vector3.MoveTowards (serpent.position, positionFinal, vitesseVerticale * Time.deltaTime);
		foreach (Collider2D _colliders in serpent.GetComponentsInChildren <Collider2D>()) {

			_colliders.enabled = true;
		}
		if(vitesseVerticale<=18){
			acceleration = vitessePrecedente *vitesse;	
		}

		//Debug.Log ("Acceleration : " +  vitessePrecedente *vitesse);
	/*	if (acceleration <= 2f) {
			ChangeVitesse(acceleration);	
			//Deplacement (acceleration);
		}*/
	}
//FIN de tourne()
}

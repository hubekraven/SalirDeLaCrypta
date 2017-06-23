using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class ScriptSenseurPassage : MonoBehaviour
{
	//==Variables==//
	private Transform cible;
	private Vector3 newPosition = new Vector3 (0f, 11.5f, 0f);
	private Transform positionSenseur;
	private float positionX;
	private float positionY;


	// Use this for initialization
	void Start ()
	{
		positionSenseur = GetComponent <Transform> ();
		positionX = positionSenseur.position.x;
		positionY = positionSenseur.position.y;
	
	}
	
	// Update is called once per frame
	void Update ()
	{ 
	
	}
	//au contact avec le personnage, applique une translation sur le personnage selon son axe de deplacement
	//Ref. pour Galcer le movement du personnage: http://answers.unity3d.com/questions/747872/freeze-rigidbody-position-in-script.html
	void OnTriggerEnter2D (Collider2D coll)
	{

		if (coll.gameObject.transform.name == "Perso") {
			Debug.Log (positionSenseur.position);
			cible = coll.gameObject.GetComponent<Transform> ();
			float hori = Input.GetAxis ("Horizontal");
			float verti = Input.GetAxis ("Vertical");
			Vector2 vitesse = cible.GetComponent<Rigidbody2D> ().velocity;
			float distance = 1.2f;
			vitesse.Set (0, 0);
			coll.gameObject.SetActive (false);

			if (hori < 0) {
					
				cible.position = new Vector3 ((positionX - distance), (positionY), 0f);

			}

			if (hori > 0) {
				
				cible.position = new Vector3 ((positionX + distance), (positionY), 0f);
			}

			if (verti < 0) {
				
				cible.position = new Vector3 ((positionX), (positionY - distance - 0.3F), 0f);
			}

			if (verti > 0) {
				
				cible.position = new Vector3 ((positionX), (positionY + distance + 0.7F), 0f);
			}
			StartCoroutine (arretMovPerso (coll.gameObject));//appel de la corroutine arretMovPerso
		}
	}
	//ref:https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
	IEnumerator arretMovPerso (GameObject objet)
	{
		yield return new WaitForSeconds (1);
		objet.SetActive (true);
	}
}

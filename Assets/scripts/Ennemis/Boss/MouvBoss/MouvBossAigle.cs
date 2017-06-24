using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvBossAigle : MonoBehaviour
{

	public float vitesse = 2f;
	public bool detecteHeros = false;
	public Transform target;
	public Transform viseHerosDebut;
	public Transform viseHerosFin;
	public float X_minSalle;
	public float X_maxSalle;
	public float Y_minSalle;
	public float Y_maxSalle;
	public float ratioX = 5f;
	//constante
	public float ratioY = 4f;
	//constante
	public float VerticalSpeed = 5f;
	public float Direction = 1f;

	private Transform _pointInstantiation;
	private Vector3 direction = new Vector3 (-1, 0, 0);
	private Transform _salle;
	private Vector2 positionFinal;
	private Transform player;



	void Start ()
	{

		_salle = transform.root;
		_pointInstantiation = _salle.FindChild ("pointInstantiation");


		X_maxSalle = _pointInstantiation.position.x + ratioX;
		X_minSalle = _pointInstantiation.position.x - ratioX;
		Y_maxSalle = _pointInstantiation.position.y + ratioY;
		Y_minSalle = _pointInstantiation.position.y - ratioY;

		target = GameObject.Find ("Persos").transform;// trouve le heros
		float StartY = Random.Range (Y_minSalle, Y_maxSalle);// Recuperer les valeurs random entr Min et Max.
		foreach (Transform perso in target) {

			if (perso.gameObject.activeSelf == true) {

				player = perso;
			}
		}

	}


	// Update is called once per frame
	void Update ()
	{

		transform.Translate (direction * vitesse * Time.deltaTime);// change de direction a chaque fois que l'aigle touche les limites de la salle
		Raycasting (); // Appel de la fonction Raycasting pour detecter le heros
		StartCoroutine ("directionVerticale");
	}

	void OnCollisionEnter2D (Collision2D coll)
	{

		if (coll.gameObject.tag == "collisionsalle") {
			direction.x *= -1;

		}

	}

	void Raycasting ()
	{
		Debug.DrawLine (viseHerosDebut.position, viseHerosFin.position, Color.white);

		detecteHeros = Physics2D.Linecast (viseHerosDebut.position, viseHerosFin.position, 1 << LayerMask.NameToLayer ("perso"));// trouve le heros par son layer

	}

	// code d'inspiration http://gamedev.stackexchange.com/questions/102532/random-y-axis-movement-within-set-limits-c?answertab=active#tab-top
	IEnumerator directionVerticale ()
	{

		if (detecteHeros == true) {
			float new_y = player.position.y;
			float new_x = (Random.Range (X_minSalle, X_maxSalle));
			positionFinal = new Vector2 (new_x, new_y);
			yield return new WaitForSeconds (3);

			//Nouvelle postion avec la vitesse  vers le haut ou vers le bas 
			transform.position = Vector3.MoveTowards (transform.position, positionFinal, 20f * Time.deltaTime);
		}

	}

}
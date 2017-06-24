using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvSerpent : MonoBehaviour
{
	public float vitesse = 2f;
	private bool deplacement = false;
	private float scale = 0.20f;
// c'est le scale initial des serpents dans la scene
	public Transform target;
	//public int rotationSpeed;
	//public int maxdistance;
	//private Transform myTransform;
	//public int moveSpeed;
	//float YOffset = 0f;
	public bool PersoDetecte=false;
	//private Transform Serpent;
	//public Vector3 v3Serpent;
	//private Vector3 _offset;
	//public Transform Personnage;
	//private Vector3 yAxis = new Vector3 (0, 1, 0);// on a besoin de l'axe des z pour la rotation du bouclier autour du personnage
	Transform bar;


	private  Vector3 dir;
	float origX;

	void Awake ()
	{
		//myTransform = transform;
		//bar = GameObject.Find("Perso").transform;

	}


	// Use this for initialization
	void Start ()
	{
		target = GameObject.Find("Perso").transform;
		
	}

	// Update is called once per frame
	void Update ()
	{

		if (deplacement)
			transform.Translate (Vector2.right * vitesse * Time.deltaTime); // Applique la vitesse quand le serpent se deplace a gauche
		

		else
			transform.Translate (Vector2.left * vitesse * Time.deltaTime); // Applique la vitesse quand la serpent se deplace a droite

		// limite des deplacements du serpent
		if (transform.position.x >= 7.5f) {
			transform.localScale = new Vector2 (scale, scale);
			deplacement = false;

		}


		if (transform.position.x <= -3.5f) {
			transform.localScale = new Vector2 (-scale, scale);
			deplacement = true;

		}

		if (PersoDetecte==true) {
			
			PersoDetecte = !PersoDetecte;

		}

		//myTransform.rotation = Quaternion.Slerp (myTransform.rotation,
		//	Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);

		//if (Vector3.Distance (target.position, myTransform.position) > maxdistance) {
			//Move towards target
		//	myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		//}

		//gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position  , Time.deltaTime*vitesse);

		//transform.LookAt(transform.position + new Vector3(0,0,1), target.transform.position - transform.position);

		//Vector3 direction = target.position - transform.position;
		//Quaternion rotation = Quaternion.LookRotation (direction);
		//transform.rotation = rotation;
		//if (Vector3.Distance(target.position, myTransform.position) > maxdistance)
		//{
			// Get a direction vector from us to the target
		//	Vector3 dir = target.position - myTransform.position;

			// Normalize it so that it's a unit direction vector
		//	dir.Normalize();

			// Move ourselves in that direction
		//	myTransform.position += dir * moveSpeed * Time.deltaTime;
		//}

		//transform.position = new Vector3(bar.position.x, transform.position.y, transform.position.z);
		Chase ();



	}

	void Chase () {
		Vector3 vectorToTarget = target.position - transform.position;
		float moveDistance = vitesse * Time.deltaTime;
		if (vectorToTarget.magnitude > moveDistance ) {
			transform.position += vectorToTarget.normalized * moveDistance;
		}
		else {
			transform.position = target.position;
		}
	}
}
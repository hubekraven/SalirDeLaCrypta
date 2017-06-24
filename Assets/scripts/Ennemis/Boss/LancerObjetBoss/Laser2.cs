using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tutoriel lineRenderer https://www.youtube.com/watch?v=ZT8eutqzW5A
public class Laser2 : MonoBehaviour
{

	LineRenderer lineRenderer;
	public Transform positionDebut;
	public Transform positionFin;
	public float pointsDommage = 1;
	public GameObject objectAActivate;

	float timer;
	int tempsAttente = 3;

	void Start ()
	{

		lineRenderer = GetComponent<LineRenderer> ();
		//positionFinExtentionFin = positionFin.localPosition;
		lineRenderer.enabled = true;
		objectAActivate.SetActive (true);
		lineRenderer.useWorldSpace = true;

	}

	// Update is called once per frame
	void Update ()
	{
		
		lineRenderer.SetPosition (0, positionDebut.position);
		lineRenderer.SetPosition (1, positionFin.position);
		
		timer += Time.deltaTime;

		if (timer > tempsAttente) { // si le temps d'attente est inferieur au temps ecoule appel la fonction Attente et desctive le laser pour 2 s
			
			Invoke ("Attente", 2);
			lineRenderer.enabled = false;
			objectAActivate.SetActive (false);
			timer = 0;// intitialiser le timer 
		} 
			
	}

	void Attente ()
	{
		lineRenderer.enabled = true;
		objectAActivate.SetActive (true);
	}

	//  Perte de vie hero quand il touche le laser
	void OnTriggerEnter2D (Collider2D coll)
	{

		Rigidbody2D rbTouche = coll.gameObject.GetComponent <Rigidbody2D> ();
		if (coll.gameObject.transform.parent) {
			if (coll.gameObject.transform.tag == "Player") {

				rbTouche.SendMessageUpwards ("Toucher", pointsDommage, SendMessageOptions.RequireReceiver);
			}

		}
	}

}

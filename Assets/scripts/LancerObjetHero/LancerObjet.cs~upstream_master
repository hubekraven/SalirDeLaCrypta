using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerObjet : MonoBehaviour
{
	public GameObject projectile;
	public Transform pointLancement;
	public float forceTir = 500f;
	public float tempsEntreTir = 3f;

	private float chargeH = 0f;
	private float chargeG = 0f;
	private float chargeB = 0f;
	private float chargeD = 0f;

	public GameObject TransfertProjMomie;
	public GameObject TransfertProjMasque;

	public float tempsTransf = 5f;
	public bool PersoDetecte = false;



	void Start ()
	{

		TransfertProjMomie.GetComponent<Renderer> ().enabled = false;
		TransfertProjMasque.GetComponent<Renderer> ().enabled = false;
	}



	// Update is called once per frame
	void Update ()
	{

		// pour le delai : http://answers.unity3d.com/questions/675839/hold-down-mouse-0-and-every-5-seconds-instantiate.html
		// pour la rotation du projectile http://answers.unity3d.com/questions/630670/rotate-2d-sprite-towards-moving-direction.html

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			lanceProjectile (transform.up, (1 * forceTir), Quaternion.AngleAxis (90, Vector3.forward));
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			chargeH += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			chargeH = 0;
		}
		if (chargeH >= tempsEntreTir) {
			lanceProjectile (transform.up, (1 * forceTir), Quaternion.AngleAxis (90, Vector3.forward));
			chargeH = 0;
		}
		//----------------------
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			lanceProjectile (transform.right, (-1 * forceTir), Quaternion.AngleAxis (180, Vector3.forward));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			chargeG += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			chargeG = 0;
		}
		if (chargeG >= tempsEntreTir) {
			lanceProjectile (transform.right, (-1 * forceTir), Quaternion.AngleAxis (180, Vector3.forward));
			chargeG = 0;
		}
		//----------------------
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			lanceProjectile (transform.up, (-1 * forceTir), Quaternion.AngleAxis (-90, Vector3.forward));
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			chargeB += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			chargeB = 0;
		}
		if (chargeB >= tempsEntreTir) {
			lanceProjectile (transform.up, (-1 * forceTir), Quaternion.AngleAxis (-90, Vector3.forward));
			chargeB = 0;
		}

		//----------------------
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			lanceProjectile (transform.right, (1 * forceTir), Quaternion.AngleAxis (0, Vector3.forward));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			chargeD += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			chargeD = 0;
		}
		if (chargeD >= tempsEntreTir) {
			lanceProjectile (transform.right, (1 * forceTir), Quaternion.AngleAxis (0, Vector3.forward));
			chargeD = 0;
		}



	}

	void lanceProjectile (Vector2 sens, float force, Quaternion quat)
	{

		GameObject proj = Instantiate (projectile, pointLancement.position, quat) as GameObject;
		Rigidbody2D rbProj = proj.GetComponent<Rigidbody2D> ();
		rbProj.AddForce (sens * force);

		//Transfert projectile de la momie apres collision entre le hero et l item qui porte le tag (Finishmomie) genere apres l elimination de la momie
	
		GameObject projTransfertM = Instantiate (TransfertProjMomie, pointLancement.position, quat) as GameObject;
		Rigidbody2D rbProjTransfertM = projTransfertM.GetComponent<Rigidbody2D> ();
		rbProjTransfertM.AddForce (sens * force);
			
		//Transfert projectile de la momie apres collision entre le hero et l item qui porte le tag (FinishMasque) genere apres l elimination de du masque

		GameObject projTransfertMas = Instantiate (TransfertProjMasque, pointLancement.position, quat) as GameObject;
		Rigidbody2D rbProjTransfertMas = projTransfertMas.GetComponent<Rigidbody2D> ();
		rbProjTransfertMas.AddForce (sens * force);


	}

	void OnTriggerEnter2D (Collider2D coll)
	{

		if (coll.gameObject.tag == "Finishmomie") {
			lanceProjectile (transform.right, (1 * forceTir), Quaternion.AngleAxis (0, Vector3.forward));

			Invoke ("recupererProjectile", tempsTransf);
			TransfertProjMomie.GetComponent<Renderer> ().enabled = true;

		}

		if (coll.gameObject.tag == "FinishMasque") {
			lanceProjectile (transform.right, (1 * forceTir), Quaternion.AngleAxis (0, Vector3.forward));

			Invoke ("recupererProjectile", tempsTransf);
			TransfertProjMasque.GetComponent<Renderer> ().enabled = true;

		}

	}


	void recupererProjectile ()
	{

		TransfertProjMomie.GetComponent<Renderer> ().enabled = false;
		TransfertProjMasque.GetComponent<Renderer> ().enabled = false;
	}

}






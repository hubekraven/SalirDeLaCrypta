using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerObjet : MonoBehaviour
{

	private float chargeH = 0f;
	private float chargeG = 0f;
	private float chargeB = 0f;
	private float chargeD = 0f;
	private tete maTete;
	private AudioSource monAudioSource;

	public GameObject projectile;
	public Transform pointLancement;
	public float forceTir = 500f;
	public float tempsEntreTir = 3f;
	public choixPerso parent;
	//private float timeStamp = Mathf.Infinity;

	/*public GameObject TransfertProjMomie;
	public GameObject TransfertProjChampignon;
	public GameObject TransfertProjMasque;*/

	public float tempsTransf = 5f;
	public bool PersoDetecte = false;



	void Start ()
	{
		parent = this.gameObject.GetComponentInParent<choixPerso> ();
		monAudioSource = parent.GetComponent<AudioSource> ();
		//Debug.Log (parent.tireProj);
		maTete = this.gameObject.GetComponent<tete> ();
		/*TransfertProjMomie.GetComponent<Renderer> ().enabled = false;
		TransfertProjChampignon.GetComponent<Renderer> ().enabled = false;
		TransfertProjMasque.GetComponent<Renderer> ().enabled = false;*/
	}




	// Update is called once per frame
	void Update ()
	{
		// pour le delai : http://answers.unity3d.com/questions/675839/hold-down-mouse-0-and-every-5-seconds-instantiate.html
		// pour la rotation du projectile http://answers.unity3d.com/questions/630670/rotate-2d-sprite-towards-moving-direction.html

		if(maTete.teteSprite.sprite.name == "hommeTeteDos-01" || maTete.teteSprite.sprite.name == "femmeTeteDos-01"){
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

		}

		if(maTete.teteSprite.sprite.name == "hommeTeteCoteGauche" || maTete.teteSprite.sprite.name == "femmeTeteCoteGauche"){
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

		}

		if(maTete.teteSprite.sprite.name == "masqueFace-01"){
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

		}

		if(maTete.teteSprite.sprite.name == "hommeTeteCoteDroite" || maTete.teteSprite.sprite.name == "femmeTeteCoteDroite"){
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



	}

	void lanceProjectile (Vector2 sens, float force, Quaternion quat)
	{

		monAudioSource.clip = parent.tireProj;
		monAudioSource.Play ();
		GameObject proj = Instantiate (projectile, pointLancement.position, quat) as GameObject;
		Rigidbody2D rbProj = proj.GetComponent<Rigidbody2D> ();
		rbProj.AddForce (sens * force);
	
		/*GameObject projTransfertM = Instantiate (TransfertProjMomie, pointLancement.position, quat) as GameObject;
		Rigidbody2D rbProjTransfertM = projTransfertM.GetComponent<Rigidbody2D> ();
		rbProjTransfertM.AddForce (sens * force);
			
		GameObject projTransfertCh = Instantiate (TransfertProjChampignon, pointLancement.position, quat) as GameObject;
		Rigidbody2D rbProjTransfertCh = projTransfertCh.GetComponent<Rigidbody2D> ();
		rbProjTransfertCh.AddForce (sens * 0);

		GameObject projTransfertMas = Instantiate (TransfertProjMasque, pointLancement.position, quat) as GameObject;
		Rigidbody2D rbProjTransfertMas = projTransfertMas.GetComponent<Rigidbody2D> ();
		rbProjTransfertMas.AddForce (sens * force);*/


	}

	void OnTriggerEnter2D (Collider2D coll)
	{

		if (coll.gameObject.tag == "Finishmomie") {
			lanceProjectile (transform.right, (1 * forceTir), Quaternion.AngleAxis (0, Vector3.forward));

			/*Invoke ("recupererProjectile", tempsTransf);
			TransfertProjMomie.GetComponent<Renderer> ().enabled = true;*/

		}

		if (coll.gameObject.tag == "FinishChamp") {
			lanceProjectile (transform.right, (1 * forceTir), Quaternion.AngleAxis (0, Vector3.forward));

			/*Invoke ("recupererProjectile", tempsTransf);
			TransfertProjChampignon.GetComponent<Renderer> ().enabled = true;*/


		}


		if (coll.gameObject.tag == "FinishMasque") {
			lanceProjectile (transform.right, (1 * forceTir), Quaternion.AngleAxis (0, Vector3.forward));

			/*Invoke ("recupererProjectile", tempsTransf);
			TransfertProjMasque.GetComponent<Renderer> ().enabled = true;*/


		}

	}


	void recupererProjectile ()
	{

		/*TransfertProjMomie.GetComponent<Renderer> ().enabled = false;
		TransfertProjChampignon.GetComponent<Renderer> ().enabled = false;
		TransfertProjMasque.GetComponent<Renderer> ().enabled = false;*/
	}

}






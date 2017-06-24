using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjMultipleRoue : MonoBehaviour
{

	public GameObject projectileInstantier;


	void OnTriggerEnter2D (Collider2D Other)
	{
		if (Other.gameObject.tag == "collisionsalle") {
			
			GameObject.Destroy (this.gameObject);

			Debug.Log ("ToucherSalleBossRoue");
		}

		//zonemultiple est le triger dont le parent est le boss roue, donc si le projectile initial (couteau) touche le triger
		// le meme projetile sera instantie en random partout dans salle en suivant les diemssions specifiees (40f, 60f)

		if (Other.gameObject.name == "zoneProjMultiple") {

			//Vector3 position = new Vector3(Random.Range(12.00f, 35.0f), -15, Random.Range(0.0f, 0.0f));
			Vector3 position = new Vector3 (Random.Range (40.0f, 60.0f), 19, Random.Range (0.0f, 0.0f));
			Instantiate (projectileInstantier, position, Quaternion.identity);


		}
	}
}

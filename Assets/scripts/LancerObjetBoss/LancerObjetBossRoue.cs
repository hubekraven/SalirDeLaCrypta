using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerObjetBossRoue : MonoBehaviour
{

	public GameObject projectileRoue;
	public Transform pointLancement;
	public float tempsEntreTir;
	private float charge;

	//public GameObject projectileInstantier;


	// Use this for initialization
	void Start ()
	{
		charge = tempsEntreTir;
	}

	// Update is called once per frame
	void Update ()
	{

		GameObject heros = GameObject.Find ("Perso");
		charge -= Time.deltaTime;

		// Si le heros est vivant le projectile du boss roue est instantie
		if (heros != null && charge < 0) {
			
			GameObject proj1 = Instantiate (projectileRoue, pointLancement.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj1 = proj1.GetComponent<Rigidbody2D> ();

			rbProj1.velocity = new Vector2 (0, 0);

		}

	}
}

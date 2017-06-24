using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerObjetBossMort : MonoBehaviour
{
	public GameObject projectileMort;
	public Transform pointLancement;
	public float tempsEntreTir;
	private float charge;

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

		// Si le heros est vivant le projectile du boss mort est instantie
		if (heros != null && charge < 0) {

			GameObject proj2 = Instantiate (projectileMort, pointLancement.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj2 = proj2.GetComponent<Rigidbody2D> ();
			rbProj2.velocity = new Vector2 (0, 0);
		}

	}

}

using UnityEngine;
using System.Collections;

public class LancerObjetMiniBossMasque : MonoBehaviour
{
	
	public GameObject projectileMasque;

	public Transform pointLancement1;
	public Transform pointLancement2;
	public Transform pointLancement3;
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

		GameObject heros = GameObject.Find ("Persos");
		charge -= Time.deltaTime;

		if (heros != null && charge < 0) {
			//instantier projectile masque
			GameObject proj1 = Instantiate (projectileMasque, pointLancement1.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj1 = proj1.GetComponent<Rigidbody2D> ();
			rbProj1.velocity = new Vector2 (0, 0);

			GameObject proj2 = Instantiate (projectileMasque, pointLancement2.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj2 = proj2.GetComponent<Rigidbody2D> ();
			rbProj2.velocity = new Vector2 (-10, 0);

			GameObject proj3 = Instantiate (projectileMasque, pointLancement3.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj3 = proj3.GetComponent<Rigidbody2D> ();
			rbProj3.velocity = new Vector2 (10, 0);

		}

	}

}

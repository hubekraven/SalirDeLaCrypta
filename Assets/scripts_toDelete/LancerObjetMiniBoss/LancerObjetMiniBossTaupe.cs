using UnityEngine;
using System.Collections;

public class LancerObjetMiniBossTaupe : MonoBehaviour
{

	public GameObject projectile1;
	public GameObject projectile2;
	public GameObject projectile3;
	public GameObject projectile4;
	public GameObject projectile5;
	public GameObject projectile6;

	public Transform pointLancement1;
	public Transform pointLancement2;
	public Transform pointLancement3;
	public Transform pointLancement4;
	public Transform pointLancement5;
	public Transform pointLancement6;

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
		//lanceProjectile (transform.up, (-1 * forceTir));
		GameObject heros = GameObject.Find ("Perso");// trouver le heros
		charge -= Time.deltaTime;

		// Si le heros est vivant lance le projectile instantie de six cotes
		if (heros != null && charge < 0) {

			GameObject proj1 = Instantiate (projectile1, pointLancement1.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj1 = proj1.GetComponent<Rigidbody2D> ();
			rbProj1.velocity = new Vector2 (0, 0);

			GameObject proj2 = Instantiate (projectile2, pointLancement2.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj2 = proj2.GetComponent<Rigidbody2D> ();
			rbProj2.velocity = new Vector2 (-3, 0);


			GameObject proj3 = Instantiate (projectile3, pointLancement3.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj3 = proj3.GetComponent<Rigidbody2D> ();
			rbProj3.velocity = new Vector2 (3, 0);

			GameObject proj4 = Instantiate (projectile4, pointLancement4.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj4 = proj4.GetComponent<Rigidbody2D> ();
			rbProj4.velocity = new Vector2 (0, 10);

			GameObject proj5 = Instantiate (projectile5, pointLancement5.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj5 = proj5.GetComponent<Rigidbody2D> ();
			rbProj5.velocity = new Vector2 (3, 6);


			GameObject proj6 = Instantiate (projectile6, pointLancement6.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rbProj6 = proj6.GetComponent<Rigidbody2D> ();
			rbProj6.velocity = new Vector2 (-3, 6);

		}

	}

}

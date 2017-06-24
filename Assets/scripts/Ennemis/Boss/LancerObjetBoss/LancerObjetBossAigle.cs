using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LancerObjetBossAigle : MonoBehaviour
{

	public GameObject projectileAigleFace;
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
		
		GameObject heros = GameObject.FindWithTag ("Player");
		charge -= Time.deltaTime;
		Debug.Log (transform.localRotation);

		if (heros != null && charge < 0) {

			charge = tempsEntreTir;
			GameObject proj3 = Instantiate (projectileAigleFace, pointLancement3.position, transform.localRotation) as GameObject;
			Rigidbody2D rbProj3 = proj3.GetComponent<Rigidbody2D> ();
			rbProj3.velocity = new Vector2 (0, 0);

		}
			

	}


		
}

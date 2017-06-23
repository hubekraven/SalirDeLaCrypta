using UnityEngine;
using System.Collections;

public class LancementSquelette : MonoBehaviour
{
	public GameObject projectileSquelette;
	public Transform pointLancement;
	public float forceTir = 30f;
	public float tempsEntreTir;
	private float charge;
	private Transform heros;
	private GameObject _perso;

	// Use this for initialization
	void Start ()
	{
		charge = tempsEntreTir;
		_perso = GameObject.Find ("Persos");//trouver le personnage
		//heros=fgameObject

		foreach (Transform child in _perso.transform) {
			if (child.gameObject.activeSelf == true) {
				heros = child;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		charge -= Time.deltaTime;

		if (heros != null && charge < 0) {
			Vector3 PositionFinale = heros.transform.position;
			GameObject projectileSqueletteClone = Instantiate (projectileSquelette, pointLancement.position, transform.localRotation) as GameObject;
			charge = tempsEntreTir;
			Rigidbody2D rb2dProjectileSqueletteClone = projectileSqueletteClone.GetComponent<Rigidbody2D> ();

			Vector3 dir = PositionFinale - pointLancement.position;	// Calcul la direction du tir
			rb2dProjectileSqueletteClone.AddForce (dir * forceTir);


		}
	}
		
}

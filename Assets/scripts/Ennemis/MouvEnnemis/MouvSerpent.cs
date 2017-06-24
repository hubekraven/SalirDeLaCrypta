using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// source d'inspiration https://www.youtube.com/watch?v=SVRtar6CFEI
public class MouvSerpent : MonoBehaviour
{
	
	private float scale = 0.1f;
//  le scale initial des serpents dans la salle
	public float vitesse = 2f;
	public float acceleration = 4f;
	public Transform target;
	private Transform _perso;
	public Transform viseHerosDebut;
	public Transform viseHerosFin;
	public bool detecteHeros = false;
	private Vector3 direction = new Vector3 (-1, 0, 0);


	// Use this for initialization
	void Start ()
	{
		//foreach (Transform child in _perso.transform) {
		//	if (child.gameObject.activeSelf == true) {
		//		target = child;//le hero
		//	}
		//}
		target = GameObject.Find ("Persos").transform;// trouve le heros
	}


	// Update is called once per frame
	void Update ()
	{

		transform.Translate (direction * vitesse * Time.deltaTime);// applique la vitesse sur le mouvement du serpent
		if (direction.x == -1) {
			
			transform.localScale = new Vector2 (scale, scale);// mettre a jour le flip du serpent
		}
			
		Raycasting (); // Appel de la fonction Raycasting pour detecter le heros
		SivreHeros (); // Appel de la fonction  pour suivre le heros


	}



	void OnCollisionEnter2D (Collision2D Other)

	{

		if (Other.gameObject.tag == "collisionsalle") {
			direction.x *= -1;
			transform.localScale = new Vector2 (-scale, scale);// flip le serpent quand il touche les limites de la salle
			transform.Translate (direction * vitesse * Time.deltaTime);// applique la vitesse sur le mouvement du serpent

		}



	}

	void Raycasting ()
	{
		Debug.DrawLine (viseHerosDebut.position, viseHerosFin.position, Color.white);

		detecteHeros = Physics2D.Linecast (viseHerosDebut.position, viseHerosFin.position, 1 << LayerMask.NameToLayer ("perso"));// trouve le heros par son layer

	}



	void SivreHeros ()
	{
		if (detecteHeros == true) {
			transform.Translate (direction * acceleration * Time.deltaTime);// applique la vitesse sur le mouvement du serpent

			//transform.position = Vector2.MoveTowards (target.position, transform.position, 1 * Time.deltaTime);// suit le hero si le perso est trouve
			//transform.eulerAngles = new Vector3 (0, 0, 90); // tourne sous un angle de 90 pour attaquer le heros
			Invoke ("attendreSecondes", 1); // attendre 1 seconde avant de retablir la position normale sur l'axe des x.

		}

	}

	void attendreSecondes ()
	{

		transform.eulerAngles = new Vector3 (0, 0, 0);

	}
}
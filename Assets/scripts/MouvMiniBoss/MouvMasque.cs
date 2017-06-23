using UnityEngine;
using System.Collections;

public class MouvMasque : MonoBehaviour
{
	public float vitesse = 2f;
	private bool deplacement = false;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (deplacement)
			transform.Translate (Vector2.right * vitesse * Time.deltaTime); // Applique la vitesse quand le masque se deplace a gauche
			else
			transform.Translate (Vector2.left * vitesse * Time.deltaTime); // Applique la vitesse quand la masque se deplace a droite
		
		// limite des deplacements du masque
		if (transform.position.x >= 2.5f) {
			deplacement = false;
		}


		if (transform.position.x <= -8.5f) {
			deplacement = true;
		}
			
	}

}

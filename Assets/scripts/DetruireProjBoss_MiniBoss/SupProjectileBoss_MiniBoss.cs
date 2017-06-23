using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupProjectileBoss_MiniBoss : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D Other)
	{

		// detruire les projectile des miniboss et boss des qu ils touchent leurs salle respectives ( j'ai donne le tag de collisionsalle a toutes les salles) 
		// detruire le projectile des miniboss et boss des qu ils touchent le heros
		if (Other.gameObject.tag == "collisionsalle" || Other.gameObject.name == "Perso" || Other.gameObject.tag == "bouclier") {

			GameObject.Destroy (this.gameObject);

		}

	}
}

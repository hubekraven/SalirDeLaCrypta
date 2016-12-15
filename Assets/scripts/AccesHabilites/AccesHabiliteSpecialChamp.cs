using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesHabiliteSpecialChamp : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D coll)
	{
		// destruction de l'item genere dans la salle du champigon par le game object bouclier qui a le sprite render desactiver
		if (coll.gameObject.tag == "bouclier") {
			GameObject.Destroy (this.gameObject);
		}
	}
}

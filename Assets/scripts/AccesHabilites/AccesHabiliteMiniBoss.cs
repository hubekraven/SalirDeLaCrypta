using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesHabiliteMiniBoss : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D coll)
	{

		//Interaction du personnage avec les items habilites momie, taupe, masque uniquement (distruction des items generes dans les salles)
		if (coll.gameObject.name == "Perso") {
			GameObject.Destroy (this.gameObject);

		}

	}
}
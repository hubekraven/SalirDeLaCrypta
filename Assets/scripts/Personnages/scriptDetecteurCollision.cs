using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptDetecteurCollision : MonoBehaviour
{
	//==Variables==//
	private Transform _pointCollision;
	private Transform monTransform;


	// Use this for initialization
	void Start ()
	{
		monTransform = GetComponent<Transform> ();	
	}

	//à la collision avec un des points de collision envoi un message à la methode reaction et le vecteur
	void OnTriggerEnter2D (Collider2D coll)
	{
		Debug.Log (coll.gameObject.layer);
		//Debug.Log (coll.gameObject.transform.parent.name);
		//Debug.Log (coll.gameObject.transform.parent.parent.name);
		//verifie que la collision est faite avec un ennemis ou les projectiles des ennemis(layer 17)
	/*	if (coll.gameObject.transform.parent.name == "mesEnnemis") {
			if (monTransform.name == "point_Haut") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (0f, -1f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Bas") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (0f, 1f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Gauche") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (1f, 0f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Droit") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (-1f, 0f), SendMessageOptions.RequireReceiver);
			}
		}*/

		/*else if (coll.gameObject.transform.parent.name == "bossGrandSerpent_model(Clone)") {
			if (monTransform.name == "point_Haut") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (0f, -1f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Bas") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (0f, 1f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Gauche") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (1f, 0f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Droit") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (-1f, 0f), SendMessageOptions.RequireReceiver);
			}
		}*/
	if((coll.gameObject.layer==11)||(coll.gameObject.layer == 13)||(coll.gameObject.layer == 14)||(coll.gameObject.layer == 15)||(coll.gameObject.layer == 17)||(coll.gameObject.layer == 20)) {
			if (monTransform.name == "point_Haut") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (0f, -1f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Bas") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (0f, 1f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Gauche") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (1f, 0f), SendMessageOptions.RequireReceiver);
			} else if (monTransform.name == "point_Droit") {
				monTransform.SendMessageUpwards ("Reaction", new Vector2 (-1f, 0f), SendMessageOptions.RequireReceiver);
			}
		}
	}

}
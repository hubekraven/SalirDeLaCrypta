using UnityEngine;
using System.Collections;

//www.youtube.com/watch?v=1mw1ufZq1N4
public class MouvMomie : MonoBehaviour
{
	public float DeplVitesse = 3f;
	private Transform playerCible;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (playerCible == null) {

			GameObject go = GameObject.Find ("Perso");// trouve le heros

			if (go != null) {
				
				playerCible = go.transform; // si le hero est vivant , la momie va le suivre ( player cible)
			}
		}

		if (playerCible == null)
			return;

		transform.position = Vector2.MoveTowards (transform.position, playerCible.position, DeplVitesse * Time.deltaTime);
	}
		
}

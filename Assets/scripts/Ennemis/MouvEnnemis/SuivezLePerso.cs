using UnityEngine;
using System.Collections;

public class SuivezLePerso : MonoBehaviour {
	public float DeplVitesse = 2f;
	private Transform playerCible;

	// Update is called once per frame
	void Update () {
		if (playerCible == null) {

			GameObject go = GameObject.FindWithTag ("Player");
			if (go != null) {
				playerCible = go.transform;
			}
		}

		if (playerCible == null)
			return;

		transform.position = Vector2.MoveTowards(transform.position, playerCible.position, DeplVitesse * Time.deltaTime);
	}
}

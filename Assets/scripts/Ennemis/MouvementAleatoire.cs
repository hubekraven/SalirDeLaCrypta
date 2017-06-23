using UnityEngine;
using System.Collections;

public class MouvementAleatoire : MonoBehaviour
{
	//answers.unity3d.com/questions/336663/random-movement-staying-in-an-area.html

	public float maxVitesse = 1.2f;

	//les limites du random
	private float maxX;
	private float minX;
	private float maxY;
	private float minY;

	private Transform _salle;
	private Transform _pointInstantiation;

	private float x;
	private float y;
	private float monTemps;

	void Start ()
	{
		_salle = transform.parent.parent;
		_pointInstantiation = _salle.Find ("pointInstantiation");
		maxX = _pointInstantiation.position.x + 6f;
		minX = _pointInstantiation.position.x - 6f;
		maxY = _pointInstantiation.position.y + 4f;
		minY = _pointInstantiation.position.y - 4f;

		x = Random.Range (-maxVitesse, maxVitesse);
		y = Random.Range (-maxVitesse, maxVitesse);
	}
		
	
	// Update is called once per frame
	void Update ()
	{
		monTemps += Time.deltaTime;

		if (transform.position.x > maxX) {
			x = Random.Range (-maxVitesse, 0f);
			monTemps = 0f;
		}

		if (transform.position.x < minX) {
			x = Random.Range (0f, maxVitesse);
			monTemps = 0f;
		}

		if (transform.position.y > maxY) {
			y = Random.Range (-maxVitesse, 0f);
			monTemps = 0f;
		}

		if (transform.position.y < minY) {
			y = Random.Range (0f, maxVitesse);
			monTemps = 0f;
		}

		if (monTemps > 1f) {
			x = Random.Range (-maxVitesse, maxVitesse);
			y = Random.Range (-maxVitesse, maxVitesse);
			monTemps = 0f;
		}

		transform.position = new Vector2 (transform.position.x + x, transform.position.y + y);
	}
}

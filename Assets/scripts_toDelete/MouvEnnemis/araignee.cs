using UnityEngine;
using System.Collections;

public class araignee : MonoBehaviour {
	//answers.unity3d.com/questions/336663/random-movement-staying-in-an-area.html

	public float maxVitesse = 1.2f;

	/*//les limites du random
	private float maxX = 2f;
	private float minX = -2f;
	private float maxY = 2f;
	private float minY = -2f;*/

	//les limites du random
	private float maxX;
	private float minX;
	private float maxY;
	private float minY;

	private Transform _salle;
	private Transform _pointInstantiation;
	//private float positionX;
	//private float positionY;

	private float x;
	private float y;
	private float monTemps;
	//private float angle;

	void Start(){
		//x = Random.Range (minX, maxX);
		//y = Random.Range (minY, maxY);
		//_salle=transform.root;
		_salle=transform.parent.parent;
		Debug.Log (_salle);
		_pointInstantiation=_salle.Find ("pointInstantiation");
		//Debug.Log ("Coordonnee: "+_pointInstanitation.position);
		//les limites du random
		maxX = _pointInstantiation.position.x + 6f;
		minX = _pointInstantiation.position.x-6f;
		maxY = _pointInstantiation.position.y + 4f;
		minY = _pointInstantiation.position.y-4f;

		x = Random.Range (-maxVitesse, maxVitesse);
		y = Random.Range (-maxVitesse, maxVitesse);
		//angle = Mathf.Atan2 (x,y) * Mathf.Rad2Deg;
		//Mathf.Rad2Deg - constante de conversion des radians pour degrés
		//angle = Mathf.Atan2 (x,y) * (180 / 3.141592f) + 90;
		//transform.localRotation = Quaternion.Euler (0, angle, 0);
		//transform.localRotation = Quaternion.Euler (angle, 0, 0);
	}
		
	
	// Update is called once per frame
	void Update () {
		monTemps += Time.deltaTime;

		if(transform.position.x > maxX){
			x = Random.Range (-maxVitesse, 0f);
			//x = Random.Range (minX, 0f);
		//	angle = Mathf.Atan2 (x,y) * (180 / 3.141592f) + 90;
			//transform.localRotation = Quaternion.Euler (0, angle, 0);
			//transform.localRotation = Quaternion.Euler (0, 0, angle);
			monTemps = 0f;
		}

		if(transform.position.x < minX){
			x = Random.Range (0f, maxVitesse);
			//x = Random.Range (0f, maxX);
			//angle = Mathf.Atan2 (x,y) * (180 / 3.141592f) + 90;
			//transform.localRotation = Quaternion.Euler (0, angle, 0);
			//transform.localRotation = Quaternion.Euler (0, 0, angle);
			monTemps = 0f;
		}

		if(transform.position.y > maxY){
			y = Random.Range (-maxVitesse, 0f);
			//y = Random.Range (minY, 0f);
			//angle = Mathf.Atan2 (x,y) * (180 / 3.141592f) + 90;
			//transform.localRotation = Quaternion.Euler (0, angle, 0);
			//transform.localRotation = Quaternion.Euler (0, 0, angle);
			monTemps = 0f;
		}

		if(transform.position.y < minY){
			y = Random.Range (0f, maxVitesse);
			//y = Random.Range (0f, maxY);
			//angle = Mathf.Atan2 (x,y) * (180 / 3.141592f) + 90;
			//transform.localRotation = Quaternion.Euler (0, angle, 0);
			//transform.localRotation = Quaternion.Euler (angle, 0, 0);
			monTemps = 0f;
		}

		if(monTemps > 1f){
			x = Random.Range (-maxVitesse, maxVitesse);
			y = Random.Range (-maxVitesse, maxVitesse);
			//angle = Mathf.Atan2 (x,y) * (180 / 3.141592f) + 90;
			//transform.localRotation = Quaternion.Euler (0, angle, 0);
			//transform.localRotation = Quaternion.Euler (angle, 0, 0);
			monTemps = 0f;
		}

		transform.position = new Vector2 (transform.position.x + x, transform.position.y + y);
		//transform.Translate () = new Vector2 (transform.position.x + x, transform.position.y + y);
		//transform.Translate( new Vector3 (transform.position.x, transform.position.y,0)* Time.deltaTime);
		//StartCoroutine(arretMovPerso(this.transform));
	}


		
}

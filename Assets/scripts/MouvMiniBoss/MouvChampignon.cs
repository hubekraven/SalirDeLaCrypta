using UnityEngine;
using System.Collections;

//https://www.youtube.com/watch?v=iLTP4EbM1N4// ce code est a ameliorer , on veut que le champigion glisse d'un point a l autre et pas disparaitre et apparaitre
public class MouvChampignon : MonoBehaviour {


	//public Transform [] PointsDaparition;
	public float tempsDaparition = 1.5f;
	//public GameObject MiniBossChampignon;
	//public Transform mesEnnemis;

	private Transform champignontransform;
	private Transform _pointInstantiation;


	private Transform _salle;
	private float maxX;
	private float minX;
	private float maxY;
	private float minY;

	float positionX;
	float positionY;

	//public float vitesse = 2f;
	//private float pasDeDeplacement;
	//private Vector3 cible;
	// Use this for initialization
	void Start () {



		_salle = transform.parent.parent;
		_pointInstantiation = _salle.Find("pointInstantiation");

		maxX = _pointInstantiation.position.x + 6f;
		minX = _pointInstantiation.position.x-6f;
		maxY = _pointInstantiation.position.y + 4f;
		minY = _pointInstantiation.position.y-4f;



		InvokeRepeating ("DeplacementChampignon", tempsDaparition, tempsDaparition);// // Appel de la fonction qui determine les limites des deplacements du miniBoss
		champignontransform = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {



	}

	void DeplacementChampignon(){

		positionX = Random.Range (minX, maxX);
		positionY = Random.Range (minY, maxY);
	
		//pasDeDeplacement = vitesse * Time.deltaTime;
		//cible.position.Set (positionX, positionY, 0);

		//int pointsIndex = Random.Range (0, PointsDaparition.Length);// Points de deplacement aleatoire
		champignontransform.gameObject.SetActive(false);
		//champignontransform.position = PointsDaparition [pointsIndex].position;
		champignontransform.position = new Vector2 ( positionX, positionY);
		//champignontransform.Translate (new Vector3(positionX , positionY, 0) * 1f);
		//champignontransform.position = Vector3.MoveTowards(transform.position, cible.position,pasDeDeplacement);
		champignontransform.gameObject.SetActive(true);




		// instantier le champignon selon les points de deplacement.


	}

}

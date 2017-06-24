using UnityEngine;
using System.Collections;

//https://www.youtube.com/watch?v=iLTP4EbM1N4
public class MouvTaupe : MonoBehaviour
{

	public float tempsDaparition = 1.5f;
	private Transform taupetransform;
	private Transform _pointInstantiation;
	private Transform _salle;
	private float maxX;
	private float minX;
	private float maxY;
	private float minY;

	float positionX;
	float positionY;


	// Use this for initialization
	void Start ()
	{
		
		_salle = transform.parent.parent;
		_pointInstantiation = _salle.Find ("pointInstantiation");

		maxX = _pointInstantiation.position.x + 6f;
		minX = _pointInstantiation.position.x - 6f;
		maxY = _pointInstantiation.position.y + 4f;
		minY = _pointInstantiation.position.y - 4f;

		InvokeRepeating ("DeplacementTaupe", tempsDaparition, tempsDaparition);// Appel a la fonction du random pour les deplacements entre les limites de la salle 
		taupetransform = GetComponent<Transform> ();

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void DeplacementTaupe ()
	{

		positionX = Random.Range (minX, maxX);
		positionY = Random.Range (minY, maxY);
		taupetransform.gameObject.SetActive (false);
		taupetransform.position = new Vector2 (positionX, positionY);
		taupetransform.gameObject.SetActive (true);

																												
	}
}

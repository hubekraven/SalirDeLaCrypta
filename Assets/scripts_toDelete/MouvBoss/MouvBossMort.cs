using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvBossMort : MonoBehaviour
{

	public float tempsDaparition = 1.5f;
	private Transform Bosstransform;
	private Transform _pointInstantiation;
	private Transform _salle;
	private float maxX;
	private float minX;
	private float maxY;
	private float minY;

	float positionX;
	float positionY;


	void Start ()
	{

		_salle = transform.parent.parent;
		_pointInstantiation = _salle.Find ("pointInstantiation");

		maxX = _pointInstantiation.position.x + 6f;
		minX = _pointInstantiation.position.x - 6f;
		maxY = _pointInstantiation.position.y + 4f;
		minY = _pointInstantiation.position.y - 4f;

		InvokeRepeating ("DeplacementBossMort", tempsDaparition, tempsDaparition);// Appel de la fonction qui determine les limites des deplacements du boos
		Bosstransform = GetComponent<Transform> ();
	}


	void DeplacementBossMort ()
	{
		
		positionX = Random.Range (minX, maxX);
		positionY = Random.Range (minY, maxY);
		Bosstransform.gameObject.SetActive (false);
		Bosstransform.position = new Vector2 (positionX, positionY);
		Bosstransform.gameObject.SetActive (true);

	}
}

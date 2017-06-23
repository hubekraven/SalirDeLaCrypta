using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDimensionSalle : MonoBehaviour {
	private Transform _salle;
	private Transform _pointInstantiation;
	//les limites du random
	public float maxX;
	public float minX;
	public float maxY;
	public float minY;
	public float ratioX=6f;//constante
	public float ratioY=4f;//constante

	// Use this for initialization
	void Start () {

		_salle=transform.root;//recuper la salle
		_pointInstantiation=_salle.Find ("pointInstantiation");//recuper le point instantiation de la salle

		//les limites(dimension) de la salles à partir du point instantiation 
		maxX = _pointInstantiation.position.x + ratioX;
		minX = _pointInstantiation.position.x - ratioX;
		maxY = _pointInstantiation.position.y + ratioY;
		minY = _pointInstantiation.position.y - ratioY;
	}
}

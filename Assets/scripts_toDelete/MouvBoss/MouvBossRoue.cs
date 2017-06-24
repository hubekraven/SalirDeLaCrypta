using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//answers.unity3d.com/questions/543461/object-follow-another-object-on-the-x-axis.html

public class MouvBossRoue : MonoBehaviour
{
	Transform Roue;

	void Start ()
	{
		Roue = GameObject.Find ("Perso").transform;

	}
		
	// Update is called once per frame
	void Update ()
	{
		// trouver le perso et suivre son vecteur 
		transform.position = new Vector3 (Roue.position.x, transform.position.y, transform.position.z);

	}

}

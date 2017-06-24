using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteScript : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Ouvrir () {

		anim.SetBool ("ouvrir", true);
	}
}

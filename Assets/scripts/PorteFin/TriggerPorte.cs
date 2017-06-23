using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPorte : MonoBehaviour {

	public PorteScript porte;
	public GameObject LumierActive;
	// Use this for initialization
	void Start () {

		LumierActive.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player")
			porte.Ouvrir ();
			LumierActive.SetActive (true);
	}
}

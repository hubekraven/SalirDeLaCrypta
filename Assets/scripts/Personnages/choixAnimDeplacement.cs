using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choixAnimDeplacement : MonoBehaviour {
	
	private  Animator anim;
	private Transform monTransform; 
	private float hori = 0f;
	private  float verti = 0f;
	private bool versLaDroite = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		monTransform = GetComponent<Transform> ();
		
	}
	
	void FixedUpdate ()
	{
		// https://forum.unity3d.com/threads/basic-2d-player-movement.257930/
		hori = Input.GetAxis ("Horizontal");
		verti = Input.GetAxis ("Vertical");
		anim.SetFloat ("gaucheDroite", Mathf.Abs(hori));
		anim.SetFloat ("hautBas", verti);

		if (versLaDroite && hori < 0) {
			Tourne ();
		} else if (!versLaDroite && hori > 0) {
			Tourne ();
		}
	}

	void Tourne(){
		versLaDroite = !versLaDroite;
		Vector3 orientation = monTransform.localScale;
		orientation.x *= -1f;
		monTransform.localScale = orientation;
	}
}
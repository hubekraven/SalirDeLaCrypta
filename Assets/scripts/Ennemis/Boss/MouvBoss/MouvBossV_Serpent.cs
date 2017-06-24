using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvBossV_Serpent : MonoBehaviour {

	public float Y_Min = -0.5f; //Set this to the lowest Y value you want the Item to move to.
	public float Y_Max = 0.5f; //Set this to the highest Y value you want the Item to move to.
	public float VerticalSpeed = 5f; //Speed the Item will move vertically.
	public float Dir = 1f; //Direction the Item is moving, either up or down.


	// Use this for initialization
	void Start () {
		//transform.eulerAngles = new Vector3 (0, 0, 90);
		//Dir = Random.value > 0.5f ? 1f : -1f; //Set Dir to start randomly either Up or Down.
		//float StartY = Random.Range(Y_Min, Y_Max);//Get a random value between Min and Max to start off at.

	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y > Y_Max)
			Dir = -1f;
		else if (transform.position.y < Y_Min)
			Dir = 1f;

		//Compute new position based on VerticalSpeed and whether we are going up or down
		Vector3 NewPos = new Vector3(transform.position.x, transform.position.y + ((VerticalSpeed * Time.deltaTime) * Dir), transform.position.z);
		transform.position = NewPos;

		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetruireProjectileApresTemps : MonoBehaviour
{
	//http://answers.unity3d.com/questions/133894/deleting-bullets-after-a-few-seconds.html
	public float timer;

	// Update is called once per frame
	void Update ()
	{
		timer += 1.0f * Time.deltaTime;

		if(timer >= 4){
			GameObject.Destroy (this.gameObject);
		}
	}

}

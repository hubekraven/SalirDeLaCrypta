using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptFindeJeu : MonoBehaviour {


	void OnCollisionEnter2D (Collision2D Other)

	{
		if (Other.gameObject.name == "Finjeu") {
			StartCoroutine ("introduction");

		}

	}



	IEnumerator introduction ()
	{
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene ("Menu");
	}

}



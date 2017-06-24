using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptEchelle : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D Other){
		if (Other.transform.tag == "Player") {
			SceneManager.LoadScene ("Fin");
		}
	}

}

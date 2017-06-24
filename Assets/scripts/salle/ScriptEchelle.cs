using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ScriptEchelle : MonoBehaviour {

	private Scene currentLevel;//niveau actuel
	private int iLevel;//index du niveau actuel

	void OnTriggerEnter2D(Collider2D Other){

		if (Other.transform.tag == "Player") 
		{
			currentLevel = SceneManager.GetActiveScene ();

			if (currentLevel.name != "Niveau4") 
			{
				iLevel = currentLevel.buildIndex;
				SceneManager.LoadScene (iLevel+1);
			}
			else
			{
				SceneManager.LoadScene ("Fin");
			}
		}
	}

}

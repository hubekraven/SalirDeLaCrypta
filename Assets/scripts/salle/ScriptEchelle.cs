using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ScriptEchelle : MonoBehaviour {

	private Scene currentLevel;//niveau actuel
	private int iLevel;//index du niveau actuel
	private GameObject _canvas;
	private bool finNiveau = false;

	void OnTriggerEnter2D(Collider2D Other){

		if (Other.transform.tag == "Player") 
		{
			finNiveau = true;
			//currentLevel = SceneManager.GetActiveScene ();
			GameObject.Find ("Canvas").SendMessage ("chargerNiveau",finNiveau);
			//_canvas.SendMessage("sauvegardePlayerState");
			//if (currentLevel.name != "Niveau4") 
			//{
				//iLevel = currentLevel.buildIndex;

				//SceneManager.LoadScene (iLevel+1);

			//}
			//else
			//{
				//SceneManager.LoadScene ("Fin");
			//}
		}
	}

}

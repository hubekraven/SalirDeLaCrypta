using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasChoixPerso : MonoBehaviour {
	// pour les playerPrefs: https://www.youtube.com/watch?v=h37OIxQ3ZBU
	//écran de départ
//	public GameObject PanelPerso;

	// Use this for initialization
	void Start () {
	//	PanelPerso.gameObject.SetActive (true);
		Time.timeScale = 1;
		PlayerPrefs.DeleteAll ();
	}

	public void choixYucan(string maScene){
		Debug.Log ("JE choisi Yucan");
			PlayerPrefs.SetString ("choixPerso", "Yucan");
			SceneManager.LoadScene (maScene);
	//		PanelPerso.gameObject.SetActive (false);
			Time.timeScale = 1;
		}

	public void choixNahua(string maScene){
		Debug.Log ("JE choisi Nahua");
			PlayerPrefs.SetString ("choixPerso", "Nahua");
		SceneManager.LoadScene (maScene);
		//	PanelPerso.gameObject.SetActive (false);
			Time.timeScale = 1;
	}
}

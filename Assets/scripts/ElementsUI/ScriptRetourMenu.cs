using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptRetourMenu : MonoBehaviour {

	public AudioSource audioBtnMenu;

	// Use this for initialization
	void Start ()
	{
		audioBtnMenu = GetComponent<AudioSource> ();
	}
		
	public void Rejouer () {
		SceneManager.LoadScene ("Niveau1");
	}
	public void Menu(){
		SceneManager.LoadScene ("Menu");
	}

	public void jouerAudio(){
		Debug.Log ("Play");
		audioBtnMenu.Play ();
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
	//écran de départ
	public GameObject ecranDemarrage;

	
	// pour les playerPrefs: https://www.youtube.com/watch?v=h37OIxQ3ZBU
	
	
	// Use this for initialization
	void Start ()
	{
		ecranDemarrage.gameObject.SetActive (true);
		Time.timeScale = 0;
	}

	public void JouerJeu (string maScene)
	{
		Debug.Log ("Commencer le jeu");
		SceneManager.LoadScene ("scene_Test_Julien");
		ecranDemarrage.gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	public void choixYucan(){
		PlayerPrefs.SetString ("choixPerso", "Yucan");
	}

	public void choixNahua(){
		PlayerPrefs.SetString ("choixPerso", "Nahua");
	}
}

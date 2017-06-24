using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canevas : MonoBehaviour
{
	//écran de départ
	public GameObject ecranDemarrage;
	public GameObject ecranCredits;
	public GameObject ecranMusique;

	public AudioSource audioBtnMenu;

	//animation
	//public Animator animMenu;


	// Use this for initialization
	void Start ()
	{
		//animMenu = GetComponent<Animator> ();
		audioBtnMenu = GetComponent<AudioSource> ();
		ecranDemarrage.gameObject.SetActive (true);
		ecranCredits.gameObject.SetActive (false);
		ecranMusique.gameObject.SetActive (false);

		//animMenu.Play ("anim_vol_chauve-souris");
		Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update () {
		//animMenu = GetComponent<Animator> ();
		//animMenu.Play ("anim_vol_chauve-souris");
	}


	public void JouerJeu (string maScene)
	{

		SceneManager.LoadScene (maScene);
		ecranDemarrage.gameObject.SetActive (false);
		ecranCredits.gameObject.SetActive (false);
		ecranMusique.gameObject.SetActive (false);


		Time.timeScale = 0;
	}

	public void voirCredits(){
		ecranCredits.gameObject.SetActive (true);
		ecranDemarrage.gameObject.SetActive (false);
		ecranMusique.gameObject.SetActive (false);

		Time.timeScale = 0;
	}

	public void voirCreditsMusique(){
		ecranMusique.gameObject.SetActive (true);
		ecranCredits.gameObject.SetActive (false);
		ecranDemarrage.gameObject.SetActive (false);

		Time.timeScale = 0;
	}


	public void retourMenu(){
		SceneManager.LoadScene ("Menu");
		//animMenu.Play ("anim_vol_chauve-souris");

		Time.timeScale = 0;
	}
		
	public void jouerAudio(){
		audioBtnMenu.Play ();
	}
}

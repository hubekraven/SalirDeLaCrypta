using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptGameManager : MonoBehaviour
{
	private Scene currentLevel;
	public GameObject _persos;
	private Transform joueur;
	private Transform tete;
	private personnage _scriptPersonnage;
	private LancerObjet _teteScript;
	//elements du UI
	private Transform _CanvasDomage;
	private Transform _CanvasVitesse;
	//niveau actuel
	private int iLevel;
	private bool peutUpdate = false;

	// Use this for initialization
	void Start ()
	{
		_persos = GameObject.Find ("Persos");
		if (_persos != null)
		{
			foreach(Transform child in _persos.transform)
			{
				if(child.gameObject.activeSelf == true)
				{
					joueur = child;
					tete = joueur.GetChild (1);
					_scriptPersonnage = joueur.GetComponent<personnage> () as personnage; 
					_teteScript = tete.GetComponent<LancerObjet> () as LancerObjet;//recuper le scrip lancer objet pour pouvoir changer le projectil instancié
				}
			}
		}


		currentLevel = SceneManager.GetActiveScene ();//recupper le niveau actuelle du jeu

		if(PlayerPrefs.HasKey ("playerState"))
		{
			_CanvasDomage = this.transform.GetChild (0);
			_CanvasVitesse= this.transform.GetChild (1);
			this.chargePlayerState ();
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(peutUpdate==true)
		{
			this.chargePlayerState ();
		}
	}
	//**function permet de sauvegarder tous les bonus & state du joueurs à la fin de chaque niveau//
	void sauvegardePlayerState ()
	{
		Debug.Log("SAUVEGARDE");
		PlayerPrefs.SetFloat ("vieJoueur", _scriptPersonnage.nbVie);//recuper la vie restante du joueur
		PlayerPrefs.SetFloat ("bombeJoueur", _scriptPersonnage.nbBombe);//recuper les bombes restantes du joueur
		PlayerPrefs.Save ();//sauvegarde toutes les preferences du joueurs
		//Debug.Log ("Vie Maximun du Joueur: " + PlayerPrefs.GetFloat ("vieMaxJoueur"));
		//Debug.Log ("vitesseJoueur: " + PlayerPrefs.GetFloat ("vitesseJoueur"));
		//Debug.Log ("domageJoueur: " + PlayerPrefs.GetFloat ("domageJoueur"));
	
	}

	void chargerNiveau (bool finNiveau)
	{
		if (finNiveau == true) {
			this.sauvegardePlayerState ();
			//WaitForSeconds()
			//gestion du lancement du prochain niveau
			if (currentLevel.name != "Niveau4") {
				iLevel = currentLevel.buildIndex;
				SceneManager.LoadScene (iLevel + 1);
				Debug.Log ("NIVEAU :" + iLevel.ToString ());
			} else {
				SceneManager.LoadScene ("Fin");
				PlayerPrefs.DeleteAll ();
				Debug.Log ("FIN DU JEU");
			}
		}
		finNiveau = false;
		//peutUpdate = true;
		PlayerPrefs.SetString ("playerState","yes");
	}
	//**function permet de charger les state du joueur au debut de chaque niveau
	void chargePlayerState ()
	{
		Debug.Log("chargePlayerState");
		Debug.Log ("joueur " + joueur );

		if (currentLevel.name != "Niveau1") {

			if (PlayerPrefs.HasKey ("vieMaxJoueur")) {
				
				//_scriptPersonnage.nbVieMax = PlayerPrefs.GetFloat ("vieMaxJoueur");
				Debug.Log ("KEY vieMaxJoueur");
			}
			if (PlayerPrefs.HasKey ("vitesseJoueur")) {
				Debug.Log ("KEY vitesseJoueur");
				_CanvasVitesse.gameObject.SetActive (true);
				_CanvasVitesse.GetChild (1).GetComponent <Text>().text = PlayerPrefs.GetFloat ("vitesseJoueur").ToString ();
				_scriptPersonnage.vitesse = PlayerPrefs.GetFloat ("vitesseJoueur");
			}
			if (PlayerPrefs.HasKey ("domageJoueur")) {
				Debug.Log ("KEY domageJoueur");
				_scriptPersonnage.domagePerso = PlayerPrefs.GetFloat ("domageJoueur");
				_CanvasDomage.gameObject.SetActive (true);
				_CanvasDomage.GetChild (1).GetComponent <Text>().text = PlayerPrefs.GetFloat ("domageJoueur").ToString ();
				_teteScript.projectile = Resources.Load ("elementsExtras/projectileUpgrade") as GameObject;//donne le nouveau projectil au personnage
			}
			_scriptPersonnage.nbVie = PlayerPrefs.GetFloat ("vieJoueur");
			_scriptPersonnage.nbBombe = PlayerPrefs.GetFloat ("bombeJoueur");
		} else {
			PlayerPrefs.DeleteAll ();
			//_scriptPersonnage.nbVie = PlayerPrefs.GetFloat ("vieJoueur");
			//_scriptPersonnage.nbBombe = PlayerPrefs.GetFloat ("bombeJoueur");
		}
		peutUpdate = false;
	}
}

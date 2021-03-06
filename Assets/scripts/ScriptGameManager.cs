﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptGameManager : MonoBehaviour
{
	public static ScriptGameManager gameManager;// defining a reference for current gameObject
	private Scene currentLevel;
	public GameObject _persos;
	private Transform joueur;
	private Transform tete;
	private personnage _scriptPersonnage;
	private LancerObjet _teteScript;
	//elements du UI
	private Transform _CanvasDomage;
	private Transform _CanvasVitesse;
	public Text txtnbBombe;
	public Text txtnbVies;
	public Text nbDomage;
	public Text nbVitesse;
	//niveau actuel
	private int iLevel;
	private bool peutUpdate = false;
	public int testVariable;

	//private string choixPersonnage;
	public Transform persoActif;

	private string bonusName;
	private bool _hasNewProjectil = false;
	//public string choixPersonnage;

	private GameObject nouveauProjectil;

	//TODO: Debuger Niveau 2 Collision Tete Boss serpent
	void Awake()
	{
		/***make sure that only one gameManager is Active in all the scenes
		ref: https://unity3d.com/fr/learn/tutorials/topics/scripting/persistence-saving-and-loading-data***/
		txtnbVies =this.txtnbVies;
		txtnbBombe =this.txtnbBombe;

		if (gameManager == null) {
			Debug.Log("==> This is the GameManager");
			DontDestroyOnLoad (gameObject);
			gameManager = this;
		} 
		else if (gameManager && gameManager != this) {
			Debug.Log("==> GameManager Detected this instance will be destroied!");
			Destroy (gameObject);
		}

	}

	// Use this for initialization
	void Start ()
	{
		   
		 

		Debug.Log("====> MANAGER");

		if (_persos == null) {
			_persos = GameObject.Find("Persos");
		}
		this.definirChoixPerso ();

		currentLevel = SceneManager.GetActiveScene ();//recupper le niveau actuelle du jeu
		Debug.Log("===> LEVEL " + currentLevel.name);
		//Debug.Log("PLAYER PREF " + PlayerPrefs.GetString ("choixPerso"));
		_CanvasDomage = this.transform.GetChild (0);
		_CanvasVitesse= this.transform.GetChild (1);
		nbDomage = _CanvasDomage.GetChild(1).GetComponent<Text>();
		nbVitesse=_CanvasVitesse.GetChild(1).GetComponent<Text>();
		this.chargePlayerState ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!peutUpdate)
		{
			if(PlayerPrefs.HasKey ("playerState"))
			{
				//Debug.Log("====> TESTING IN CANVAS");
				//_CanvasDomage = this.transform.GetChild (0);
				//_CanvasVitesse= this.transform.GetChild (1);
			}
			//this.chargePlayerState ();
		}
	}
	//**function permet de sauvegarder tous les bonus & state du joueurs à la fin de chaque niveau//
	void sauvegardePlayerState ()
	{
		//Debug.Log("SAUVEGARDE");
		PlayerPrefs.SetString("choixPerso",persoActif.transform.name);
		PlayerPrefs.SetFloat ("vieJoueur", _scriptPersonnage.nbVie);//recuper la vie restante du joueur
		PlayerPrefs.SetFloat ("vieMaxJoueur", _scriptPersonnage.nbVieMax);
		PlayerPrefs.SetFloat ("bombeJoueur", _scriptPersonnage.nbBombe);//recuper les bombes restantes du joueur
		if (PlayerPrefs.HasKey ("vitesseJoueur")) {
			PlayerPrefs.SetFloat ("vitesseJoueur", _scriptPersonnage.vitesse);//sauvegarde la vitesse
		}
		if(PlayerPrefs.HasKey ("domageJoueur")){
			PlayerPrefs.SetFloat ("domageJoueur", _scriptPersonnage.domagePerso);//sauvegarde les domages du joueur
		}
		PlayerPrefs.Save ();//sauvegarde toutes les preferences du joueurs

	
	}

	void chargerNiveau (bool finNiveau)
	{
		//Debug.Log ("===> CHARGEMENT NIVEAU " + currentLevel.name);
		if (finNiveau == true) {
			this.sauvegardePlayerState ();
			//gestion du lancement du prochain niveau
			if (currentLevel.name != "Niveau4") {
				iLevel = currentLevel.buildIndex;
				SceneManager.LoadScene (iLevel + 1);
				peutUpdate = true;
				//Debug.Log ("===> NIVEAU :" + currentLevel.);
			} else {
				SceneManager.LoadScene ("Fin");
				//PlayerPrefs.DeleteAll ();
				//Debug.Log ("FIN DU JEU");
			}
		}
		finNiveau = false;
		//PlayerPrefs.SetString ("playerState","yes");
	}
	//**function permet de charger les state du joueur au debut de chaque niveau
	void chargePlayerState ()
	{
		Debug.Log ("====LOADING PLAYER STATE=====");
		if (currentLevel.name != "Niveau1") {

			if (PlayerPrefs.HasKey ("vieMaxJoueur")) {
				_scriptPersonnage.nbVieMax = PlayerPrefs.GetFloat ("vieMaxJoueur");
			}
			if (PlayerPrefs.HasKey ("vitesseJoueur")) {
				Debug.Log ("===> KEY vitesseJoueur");
				_CanvasVitesse.gameObject.SetActive (true);
				_CanvasVitesse.GetChild (1).GetComponent <Text>().text = PlayerPrefs.GetFloat ("vitesseJoueur").ToString ();
				_scriptPersonnage.vitesse = PlayerPrefs.GetFloat ("vitesseJoueur");
			}
			if (PlayerPrefs.HasKey ("domageJoueur")) {
				Debug.Log ("===> KEY domageJoueur");
				_scriptPersonnage.domagePerso = PlayerPrefs.GetFloat ("domageJoueur");
				_CanvasDomage.gameObject.SetActive (true);
				_CanvasDomage.GetChild (1).GetComponent <Text>().text = PlayerPrefs.GetFloat ("domageJoueur").ToString ();
				_teteScript.projectile = Resources.Load ("elementsExtras/projectileUpgrade") as GameObject;//donne le nouveau projectil au personnage
			}
			_scriptPersonnage.nbVie = PlayerPrefs.GetFloat ("vieJoueur");
			_scriptPersonnage.nbBombe = PlayerPrefs.GetFloat ("bombeJoueur");
		} else {
			PlayerPrefs.DeleteAll ();
		}
		peutUpdate = false;
		Debug.Log("====FINISHED LOADING===");
	}

	void definirChoixPerso (){
	

		if (persoActif == null) {
			Debug.Log ("====> PERSO Active: " + PlayerPrefs.GetString ("choixPerso"));
			string choixPersonnage = PlayerPrefs.GetString ("choixPerso");
			Debug.Log("====> CALL TO CHOIX PERSO -->" + _persos.transform.childCount + " ---> "+ choixPersonnage+ " /  PersoActif---> "+ persoActif);
			//Debug.Log ("===>choixPersonnage -> " + _persos.transform.Find(choixPersonnage));
			persoActif = _persos.transform.Find (choixPersonnage);
		}
		//Debug.Log ("===>choixPersonnage -> " + persoActif.ToString());
		persoActif.gameObject.SetActive (true);
		//Debug.Log ("===>choixPersonnage -> " + persoActif.ToString());
		tete = persoActif.GetChild (1);
		_scriptPersonnage = persoActif.GetComponent<personnage> () as personnage; 
		_teteScript = tete.GetComponent<LancerObjet> () as LancerObjet;//recuper le scrip lancer objet pour pouvoir changer le projectil instancié
		//Debug.Log ("===>choixPersonnage -> " + choixPersonnage );

		//if (choixPersonnage == "Nahua") {
		//	monChoix = 1;
		//} else {
		//	monChoix = 0;
		//}

		//_persos = GameObject.Find ("Persos");
		//if (_persos) {
		//	joueur=_persos.transform.GetChild(monChoix);
		//	//Debug.Log ("===> LE JOUEUR " + joueur.ToString ());
		//	joueur.gameObject.SetActive (true);
		//	tete = joueur.GetChild (1);
		//	_scriptPersonnage = joueur.GetComponent<personnage> () as personnage; 
		//	_teteScript = tete.GetComponent<LancerObjet> () as LancerObjet;//recuper le scrip lancer objet pour pouvoir changer le projectil instancié
		//}
	}
	void upgradePlayer(string myMessage){

		if(myMessage == "upgradeVie"){
			bonusName ="VieUp";
			_scriptPersonnage.nbVieMax++;
			_scriptPersonnage.nbVie = _scriptPersonnage.nbVieMax;
			Debug.Log ("====> Upgrade VIE "+ _scriptPersonnage.nbVie );

		}
		if(myMessage == "upgradeVitesse"){
			
			if (!_CanvasVitesse.gameObject.activeInHierarchy) {
				_CanvasVitesse.gameObject.SetActive (true);
				_scriptPersonnage.vitesse += 0.5f;
				nbVitesse.text = _scriptPersonnage.vitesse.ToString ();
				PlayerPrefs.SetFloat ("vitesseJoueur", _scriptPersonnage.vitesse);
			} 
			else if (_CanvasVitesse.gameObject.activeInHierarchy) {
				bonusName ="Speed Up";
				_scriptPersonnage.vitesse += 0.5f;
				nbVitesse.text = _scriptPersonnage.vitesse.ToString ();
			}
		}
		if(myMessage == "upgradeProjectil"){
			
			if (!_CanvasDomage.gameObject.activeInHierarchy) {
				_CanvasDomage.gameObject.SetActive (true);
				_scriptPersonnage.domagePerso++;
				_teteScript.projectile=Resources.Load ("elementsExtras/projectileUpgrade") as GameObject;//donne le nouveau projectil au personnage
				PlayerPrefs.SetFloat("domageJoueur",_scriptPersonnage.domagePerso );
			}
			else if (_CanvasDomage.gameObject.activeInHierarchy) {
				_scriptPersonnage.domagePerso++;
				nbDomage.text = _scriptPersonnage.domagePerso.ToString ();

			}
		}
	
	}
}

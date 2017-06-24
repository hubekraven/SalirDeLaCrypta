using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemBoss : MonoBehaviour {

	private string bonusName;
	private int bonusUpgrage;
	private int indiceBonus;
	public Text txtnbVies;
	public Text nbDomage;
	public Text nbVitesse;
	private GameObject nouveauProjectil;
	private Transform _Canvas;
	private Transform _CanvasDomage;
	private Transform _CanvasVitesse;
	//public Transform Perso;

	private AudioSource sourceAudio_dropSpecial;
	AudioClip son_dropSpecial;
	// Use this for initialization
	void Start ()
	{


		_Canvas = GameObject.Find ("Canvas").transform;
		_CanvasDomage = _Canvas.GetChild (0);
		_CanvasVitesse = _Canvas.GetChild(1);
		if (_CanvasDomage.gameObject.activeSelf==false) {
			//nouveauProjectil = Resources.Load ("elementsExtras/projectileUpgrade") as GameObject;
		}

//		Debug.Log ("le Canevas" + _CanvasDomage);

		son_dropSpecial = Resources.Load ("sons/itemSpecial") as AudioClip;
		sourceAudio_dropSpecial = gameObject.AddComponent<AudioSource> () as AudioSource;
		sourceAudio_dropSpecial.clip = son_dropSpecial;
		sourceAudio_dropSpecial.playOnAwake = false;
		//sourceAudio_dropSpecial.PlayDelayed (1f);


	}

	void OnCollisionEnter2D (Collision2D coll){
//		Debug.Log (coll.transform.GetChild (1)); 
		if (coll.gameObject.name == "Yucan" || coll.gameObject.name == "Nahua") {
			sourceAudio_dropSpecial.Play ();
			//projectilePerso scriptProjectil = jojec.GetComponent <projectilePerso> () as projectilePerso;

			personnage playerScript = coll.gameObject.GetComponent<personnage> () as personnage;
			Transform tete = coll.gameObject.transform.GetChild (1);
			LancerObjet teteScript = tete.GetComponent<LancerObjet> () as LancerObjet;//recuper le scrip lancer objet pour pouvoir changer le projectil instancié
		

			for (int i = 1; i <= 3;) {

				//indiceBonus =2;
				indiceBonus = Random.Range (1, 4);
				//augmente la vie maximum du joueur de 2
				if (indiceBonus == 1) {
					
					bonusName ="VieUp";
					Debug.Log ("yoLife!!!");
					playerScript.nbVieMax++;
					playerScript.nbVie = playerScript.nbVieMax;
					Debug.Log (playerScript.nbVieMax);	
				}
				//augmente la vitesse de deplacement du perso	
				else if (indiceBonus == 2) {
					if (_CanvasVitesse.gameObject.activeSelf==false) {
						_CanvasVitesse.gameObject.SetActive(true);
					}
					else if(_CanvasVitesse.gameObject.activeSelf == true){
						nbVitesse=_CanvasVitesse.GetChild(1).GetComponent<Text>();
						bonusName ="Speed Up";
						playerScript.vitesse += 0.5f;
						Debug.Log ("yoSPEED!!! " + playerScript.vitesse);

						nbVitesse.text = playerScript.vitesse.ToString ();
					}

				}
				//change et augmente la puissance des projectil du peso
				else if (indiceBonus == 3) {
					if (_CanvasDomage.gameObject.activeSelf==false) {
						_CanvasDomage.gameObject.SetActive(true);
					}
					else if(_CanvasDomage.gameObject.activeSelf == true){
						playerScript.domagePerso++;
						nbDomage=_CanvasDomage.GetChild(1).GetComponent<Text>();
						Debug.Log ("yoPOWER!!! " + nbDomage);
						nbDomage.text = playerScript.domagePerso.ToString ();
						teteScript.projectile=Resources.Load ("elementsExtras/projectileUpgrade") as GameObject;//donne le nouveau projectil au personnage
		
					}
				}
				i++;
			}
			GameObject.Destroy (gameObject,1f);
		
			//

		}
	}
}

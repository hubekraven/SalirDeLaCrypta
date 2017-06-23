using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeteGrandSerpent : MonoBehaviour
{

	//==Variables==//
	private float vieRestante;
	public int nbvie = 5;
	private Transform serpent;
	private Transform dernierElement;
	private int tailleSerpent;
	public float pointBouclier = 10;// variable pour la resistance du boulcier
	private bool bouclierActif = true;// variable pour état du bouclier
	private Color couleurBase;
	private GameObject _perso;
	private Transform bouclier;
	public float acelerationVitesse;
	private float vitesse;
	//liste de mes sources audio
	private AudioSource sourceAudio_Touche;
	private AudioSource sourceAudio_Mort;
	private AudioSource sourceAudio_Bouclier;
	private AudioSource sourceAudio_Evenement;

	//liste des clip sonore qui seront utlisés
	AudioClip son_Touche;
	AudioClip son_Mort;
	AudioClip son_Bouclier;
	AudioClip son_alert;



	// Use this for initialization
	void Start ()
	{


		//gestion des sons
		//REF:https://www.youtube.com/watch?v=rIpQMbDCIQs&t=462s
		//https://docs.unity3d.com/560/Documentation/ScriptReference/Resources.Load.html

		//ajoute la source audio à la salle;
		sourceAudio_Touche =gameObject.AddComponent<AudioSource>() as AudioSource;
		sourceAudio_Mort =gameObject.AddComponent<AudioSource>() as AudioSource;
		sourceAudio_Bouclier =gameObject.AddComponent<AudioSource>() as AudioSource;
		sourceAudio_Evenement =gameObject.AddComponent<AudioSource>() as AudioSource;
		//charge les sons qui vont être joués par la salle
		son_Touche = Resources.Load ("sons/son_boss-bossHit_1") as AudioClip;
		son_alert = Resources.Load ("sons/son_bossSerpentAlerte2") as AudioClip;
		//son_Mort = Resources.Load ("sons/portes") as AudioClip;
		son_Bouclier = Resources.Load ("sons/bouclier") as AudioClip;
		sourceAudio_Bouclier.clip = son_Bouclier;
		sourceAudio_Bouclier.playOnAwake = false;
		//Fin gestion des sons
		sourceAudio_Touche.clip = son_Touche;
		sourceAudio_Touche.playOnAwake = false;
		sourceAudio_Evenement.clip = son_alert;
		sourceAudio_Evenement.playOnAwake = false;
		//sourceAudio_Touche.Stop ();

		serpent = GetComponent <Transform> ();
		bouclier = serpent.GetChild (0);//va chercher le gameobject bouclier du serpent
		tailleSerpent = serpent.parent.childCount;//recuper la taille du serpent
		vieRestante = nbvie * tailleSerpent;// calcule la vie total du serpent
		couleurBase = GetComponent<SpriteRenderer> ().color;// recuper la couleur original du sprite
		acelerationVitesse=0f;
	}


	// DÉBUT Gestion de la collision	avec les projectils/bombes du joueurs
	void OnCollisionEnter2D (Collision2D other)
	{

		//sourceAudio_Touche.Stop ();
		// lorsque la collision se fait entre les projectiles du joueur(layer 10) et la bombe(layer 15)
		if ((other.gameObject.layer == 10) || (other.gameObject.layer == 15)) {
			StartCoroutine (changeCouleurDomage (serpent.gameObject));//appel de la corroutine changeCouleurDomage


		}
	}// FIN de la Gestion de collision


	// DÉBUT Gestion des domages infligé à ce boss
	void Toucher (float dmg)
	{
		//if(bouclier==true){
		sourceAudio_Bouclier.Play ();
		//}
		pointBouclier -= 1;
		dernierElement = serpent.parent.GetChild (tailleSerpent - 1);//recuper le dernier segment du serpent
		//Debug.Log("VIE RESTANTE " + vieRestante);
		//deactive le boulcier après 10point de dommage domages
		if (pointBouclier <= 0) {
			bouclierActif = false;
			sourceAudio_Bouclier.Stop ();
			//si le boulcier est deactivé retire les points de vie du boss
			if (bouclierActif == false) {

				bouclier.gameObject.SetActive (false);
				vieRestante -= dmg;
				sourceAudio_Evenement.Stop ();
				sourceAudio_Touche.Play ();
				//detruit le dernier segement du corps du bosse à chaque perte de 5 points de vie
				if ((vieRestante % 5) == 0) {
					if (dernierElement.name != "bossSerpent_Tete") {
						Destroy (dernierElement.gameObject);// detruit le dernier segment du corps du boss
						tailleSerpent -= 1;//diminue sa taille
						//vitesse+=2f;
						//serpent.SendMessageUpwards ("Deplacement", 0f, SendMessageOptions.DontRequireReceiver);

						//serpent.SendMessageUpwards ("Deplacement",vitesse, SendMessageOptions.DontRequireReceiver);
						//Invoke("Deplacement",2);
						pointBouclier = 4;//renitie la resistance du bouclier
						bouclierActif = true;// reactive état du bouclier
						bouclier.gameObject.SetActive (true);// reactive le gameobject boulcier
						if((vieRestante % 10) == 0){
							//acelerationVitesse+=2;//augmente le facteur acceleration du serpent.
							sourceAudio_Touche.Pause ();
							sourceAudio_Bouclier.Pause ();
							sourceAudio_Evenement.Play ();
							serpent.SendMessageUpwards ("Pause", 1.85f, SendMessageOptions.DontRequireReceiver);
						}

					} 
					//detruit le gameobject serpent si le dernier segment est sa tete
					else if (dernierElement.name == "bossSerpent_Tete") {

						Destroy (serpent.parent.gameObject);
					}
				}
			}
		}
	}// FIN de la gestion des domages


	//DÉBUT énumerateur :change la couleur du srpite de mon gameboject après 1 centième de seconde alterne entre la nouvellecouleur et la couleur originale du sprite.
	IEnumerator changeCouleurDomage (GameObject objet)
	{
		//verifie que le bouclier est deactivé pour changer la couleur
		if (bouclier.gameObject.activeSelf == false) {
			//objet.GetComponent<SpriteRenderer> ().color = new Color (0.7F, 0.0F, 0.4F);

			objet.GetComponent<SpriteRenderer> ().color = new Color (0F, 1F, 0.855F);
			yield return new WaitForSeconds (0.1f);// temps d'attente
			objet.GetComponent<SpriteRenderer> ().color = couleurBase;
			//sourceAudio_Touche.Stop ();
		}
	}//FIN DE L'ENUMERATEUR
}

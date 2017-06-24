using UnityEngine;
using System.Collections;

///Script permet au contact de l'explosion de la bombe de déactive le sprite de de la fissure ainsi que le collider du passage secret 
/// et dactive le sprite du passage de la porte sécrete///
public class ScriptPassageSecret : MonoBehaviour
{
	
	//-- Variables-- //
	private Transform _passageSecret;
	private explosion scriptExplosion;
	private float resistance = 1;

	//liste de mes sources audio
	private AudioSource sourceAudio_PassageSecret;
	private AudioSource sourceAudio_Mort;
	private AudioSource sourceAudio_Bouclier;

	//liste des clip sonore qui seront utlisés
	AudioClip son_PassageSecret;
	AudioClip son_Mort;
	AudioClip son_Bouclier;


	// Use this for initialization

	void onCollisionEnter2D(Collider other){
		//Debug.Log (other.name);
		//GameObject _bombe = GameObject.Find ("bombe_drop(Clone)");

	}



	void Start ()
	{
		sourceAudio_PassageSecret = gameObject.AddComponent<AudioSource> () as AudioSource;
		son_PassageSecret = Resources.Load ("sons/son_passageSecret2") as AudioClip;
		sourceAudio_PassageSecret.clip = son_PassageSecret;
		sourceAudio_PassageSecret.playOnAwake = false;

	

		_passageSecret = GetComponent <Transform> ();
	}

	//DEBUT TOUCHER
	void Toucher (float dmg)
	{
		Debug.Log ("TOUCHEEEEEEEE");
		resistance -= dmg;
		if (resistance <= 0) {
			//active la porte secrete à la detection de l'explosion de la bombe
			_passageSecret.GetChild (2).gameObject.SetActive (false);
			_passageSecret.GetChild (1).gameObject.SetActive (true);
			this._passageSecret.GetComponent <BoxCollider2D> ().enabled = false;
			sourceAudio_PassageSecret.Play ();
			Destroy (_passageSecret.GetComponent<Rigidbody2D> ());
		}
	}
//FIN DE METODE TOUCHER
}

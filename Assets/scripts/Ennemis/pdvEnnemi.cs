using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pdvEnnemi : MonoBehaviour
{
	private Transform monTransform;
	private Transform _Salle;
	private AudioSource audio_ennemiMort;
	public float vieRestante;

	private AudioSource audio_ennemisMort;

	//liste des clip sonore qui seront utlisés

	AudioClip son_ennemiMort;


	void Start(){
		//ennemiMort= GetComponent<AudioSource> ();
		monTransform = GetComponent<Transform> ();


		audio_ennemiMort =gameObject.AddComponent<AudioSource>() as AudioSource;
		son_ennemiMort = Resources.Load ("sons/ennemiMort") as AudioClip;
		audio_ennemiMort.clip = son_ennemiMort;
		audio_ennemiMort.playOnAwake = false;


		//_Salle = gameObject.transform.root;
		if (monTransform.root.name == "SalleBoss") {
			//Boss = _Salle.FindChild("mesEnnemis").GetChild (0);
			Debug.Log ("BossSSSSSSSSSSSS");
		}

	}
	void Toucher (float dmg)
	{
		vieRestante -= dmg;
		if (vieRestante <= 0) {
			audio_ennemiMort.Play ();
			this.transform.localScale = new Vector3 (0,0,0); 
			GameObject.Destroy (this.gameObject,1);
		}

		else if (monTransform.root.name == "SalleBoss") {
			if (vieRestante <= 0) {
				GameObject.Destroy (this.gameObject);
				SceneManager.LoadScene ("Gagnant");
			}
		}
	}


}

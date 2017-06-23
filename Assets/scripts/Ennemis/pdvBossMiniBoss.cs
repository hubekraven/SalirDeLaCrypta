using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pdvBossMiniBoss : MonoBehaviour
{
	private Transform monTransform;
	private Transform _Salle;
	private AudioSource audio_ennemiMort;

	public float vieRestante;
	public float vieMax;
	public Image barVieBoss;

	//liste des clip sonore qui seront utlisés

	AudioClip son_ennemiMort;

	void Start ()
	{
		
		MetreAjourBarVie ();
		monTransform = GetComponent<Transform> ();

		audio_ennemiMort =gameObject.AddComponent<AudioSource>() as AudioSource;
		son_ennemiMort = Resources.Load ("sons/son_boss-bossHit_2") as AudioClip;
		audio_ennemiMort.clip = son_ennemiMort;
		audio_ennemiMort.playOnAwake = false;

		//_Salle = gameObject.transform.root;
		if (monTransform.root.name == "SalleBoss") {
			//Boss = _Salle.FindChild("mesEnnemis").GetChild (0);
			Debug.Log ("BossSSSSSSSSSSSS");
		}


	}


	void MetreAjourBarVie ()
	{
		float ratio = vieRestante / vieMax;
		barVieBoss.rectTransform.localScale = new Vector3 (ratio, 1, 1);//Reduire le rectangle vert en deminuant son scale

	}

	void Toucher (float dmg)
	{
		vieRestante -= dmg;
		audio_ennemiMort.Play ();

		if (vieRestante <= 0) {
			
			GameObject.Destroy (this.gameObject,1);

		} else if (monTransform.root.name == "SalleBoss") {
			if (vieRestante <= 0) {
				GameObject.Destroy (this.gameObject);
				SceneManager.LoadScene ("Gagnant");
			}
		}
		MetreAjourBarVie ();
	}


}
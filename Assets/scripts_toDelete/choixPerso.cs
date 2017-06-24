using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choixPerso : MonoBehaviour {

	// http://answers.unity3d.com/questions/894211/set-objects-child-to-activeinactive.html

	private string choixPersonnage;
	private int monChoix;

	// Use this for initialization
	void Start () {
		choixPersonnage = PlayerPrefs.GetString ("choixPerso");
		//monChoix = transform.Find (choixPersonnage);
		//monChoix.enabled = true; 
		if (choixPersonnage == "Nahua") {
			monChoix = 1;
		} else {
			monChoix = 0;
		}
		this.transform.GetChild (monChoix).gameObject.SetActive (true);

	}
	

}

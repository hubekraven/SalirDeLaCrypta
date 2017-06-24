using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class coeur : MonoBehaviour {

	public Text txtnbVies;
	//public Transform Perso;

	// http://answers.unity3d.com/questions/42843/referencing-non-static-variables-from-another-scri.html
	// pour aller chercher la variable d'un script d'un autre gameObject
	void Start()
	{
		//Perso = GetComponent<Transform> ();
	}

	void OnCollisionEnter2D (Collision2D coll){
		Debug.Log (coll.gameObject.name); 
		if (coll.gameObject.name == "Yucan" || coll.gameObject.name == "Nahua") {
			
			personnage playerScript = coll.gameObject.GetComponent<personnage> () as personnage;
			Debug.Log (playerScript.nbVie);
			Debug.Log (playerScript.nbVieMax);

			//Debug.Log (playerScript.nbVie);
			if (playerScript.nbVie < playerScript.nbVieMax) {
				//if (coll.gameObject.name == "Yucan" || coll.gameObject.name == "Nahua") {
				//if (coll.gameObject.tag == "Player") {
					//txtnbVies = Text.Find("nbVies");
				Debug.Log ("yo");
					playerScript.nbVie++;
					//txtnbVies.text = playerScript.nbVie.ToString ();
					GameObject.Destroy (this.gameObject);

				//}
			}
		}
	}
}


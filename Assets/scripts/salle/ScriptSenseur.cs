using UnityEngine;
using System.Collections;

public class ScriptSenseur : MonoBehaviour
{
	private Transform _salle;
	public ScriptSalle salleScript;
	private Transform sensTrans;


	// Use this for initialization
	void Start ()
	{
		//acceder au script de la salle pour recuper la variable du nombre de porte;
		this.sensTrans = GetComponent<Transform> ();

		Transform _salle = this.sensTrans.parent;

		//sonAlerte = GetComponent<AudioSource>();
		if (_salle != null) {
			salleScript = _salle.GetComponent<ScriptSalle> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D Other)
	{
		//Debug.Log (Other.name);
		/*//==NOTE: UTILISER CE SCRIPT AVEC NOUVEAU PREFAB PERSONNAGE
		if(Other.gameObject.transform.parent.name =="Perso"){
			//salleScript.PersoDetecte = true;
			if (sensTrans.parent.name != "SalleStart")
			salleScript.peutGenerEnnemis = true;
			GameObject.Destroy (this.gameObject);
			//Debug.Log("Mesure: " + sensTrans.GetComponent <BoxCollider2D>().size.x);
		}*/
		//script a utilise avec premier prefab perso

		if (Other.gameObject.name == "Perso") {
			if (sensTrans.parent.name != "SalleStart")
				salleScript.peutGenerEnnemis = true;
			GameObject.Destroy (this.gameObject);
			//Debug.Log("Mesure: " + sensTrans.GetComponent <BoxCollider2D>().size.x);
		}
	}
}

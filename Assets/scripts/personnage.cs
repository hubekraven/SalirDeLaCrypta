using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// http://answers.unity3d.com/questions/42843/referencing-non-static-variables-from-another-scri.html
// pour aller chercher la variable d'un script d'un autre gameObject

public class personnage : MonoBehaviour
{
	private Rigidbody2D rb;
	private Collider2D colli;
	public float vitesse = 1f;
	private float hori = 0f;
	private  float verti = 0f;
	public Text txtnbBombe;
	public Text txtnbVies;
	private int nbBombe = 0;
	public float nbVieMax = 3;
	public float nbVie = 3;
	public GameObject bombe;
	public Transform pointDepotBombe;

	// Use this for initialization
	void Start ()
	{
		this.rb = GetComponent<Rigidbody2D> ();
		this.colli = GetComponent<Collider2D> ();
		txtnbBombe.text = "0";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.E) && nbBombe > 0) {
			GameObject bombeExplose = Instantiate (bombe, pointDepotBombe.position, transform.localRotation) as GameObject;
			nbBombe--;
			txtnbBombe.text = nbBombe.ToString ();
		}
	}

	void FixedUpdate ()
	{
		// https://forum.unity3d.com/threads/basic-2d-player-movement.257930/
		hori = Input.GetAxis ("Horizontal");
		verti = Input.GetAxis ("Vertical");
		this.rb.velocity = new Vector2 (hori * vitesse, verti * vitesse);
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.name == "bombe(Clone)") {
			nbBombe++;
			txtnbBombe.text = nbBombe.ToString ();
		}
			
		if (coll.gameObject.name == "coeur(Clone)" && nbVie < nbVieMax) {
			nbVie++;
			txtnbVies.text = nbVie.ToString ();
		}

		if (coll.gameObject.transform.parent) {

			if (coll.gameObject.transform.parent.name == "mesEnnemis") {
				nbVie--;
				if (nbVie <= 0) {
					//Debug.Log ("mort");
					txtnbVies.text = nbVie.ToString ();
				} else {
					txtnbVies.text = nbVie.ToString ();
				}
			}
		}
	}

	void Toucher (float dmg)
	{
		nbVie -= dmg;
		if (nbVie <= 0) {
			Debug.Log ("JE SUIS MORT");
			txtnbVies.text = nbVie.ToString ();
		}
	}
}

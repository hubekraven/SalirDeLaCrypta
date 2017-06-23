using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// http://answers.unity3d.com/questions/42843/referencing-non-static-variables-from-another-scri.html
// pour aller chercher la variable d'un script d'un autre gameObject

public class personnage : MonoBehaviour
{
	private Rigidbody2D rb;
	private float hori = 0f;
	private  float verti = 0f;
	private bool ouch = false;
	private float tempsInvincible = 0f;
	private AudioSource monAudioSource;
	private AudioSource douleur;
	public Transform _detecTeurCollision;//recuper le gameObjet detecteur de collision

	public Text txtnbBombe;
	public Text txtnbVies;
	public float nbBombe = 0;
	public float nbVieMax = 3;
	public float nbVie = 3;
	public GameObject bombe;
	public Transform pointDepotBombe;
	public float vitesse = 1f;
	public choixPerso parent;
	public float puissance = 5000f;
	public float domagePerso=0;


	// Use this for initialization
	void Start ()
	{
		parent = this.gameObject.GetComponentInParent<choixPerso> ();
		monAudioSource = parent.GetComponent<AudioSource> ();
		douleur = this.GetComponent<AudioSource> ();
		
		this.rb = GetComponent<Rigidbody2D> ();
		txtnbBombe.text = nbBombe.ToString ();
		txtnbVies.text = nbVieMax.ToString();

		Transform _detectColl = Instantiate (_detecTeurCollision, rb.position, transform.localRotation) as Transform;//crée une instance du detecteur de Collision

		_detectColl.parent = this.rb.transform;//attache le detecteur de collision comme enfant du perso.

	}
	
	// Update is called once per frame
	void Update ()
	{
		txtnbVies.text = nbVie.ToString ();
		if (ouch) {
			tempsInvincible += Time.deltaTime;
			if(tempsInvincible >1){
				ouch = false;
				tempsInvincible = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.E) && nbBombe > 0) {
			monAudioSource.clip = parent.depotBombe;
			monAudioSource.Play ();
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
			monAudioSource.clip = parent.item_PickUp;
			monAudioSource.Play ();
			nbBombe++;
			txtnbBombe.text = nbBombe.ToString ();
		}

		if (coll.gameObject.transform.parent) {

			if ((coll.gameObject.transform.parent.name == "mesEnnemis" && !ouch)||(coll.gameObject.layer == 13 && !ouch)) {
				ouch = true;
				douleur.Play ();
				nbVie--;
				if (nbVie <= 0) {
					
					SceneManager.LoadScene ("Perdant");
					txtnbVies.text = nbVie.ToString ();
				} else {
					txtnbVies.text = nbVie.ToString ();
				}
			}
		}
	}
		
	void Toucher (float dmg)
	{
		if (!ouch) {
			douleur.Play ();
			nbVie -= dmg;
			txtnbVies.text = nbVie.ToString ();
			if (nbVie <= 0) {
				SceneManager.LoadScene ("Perdant");
			}
		}
	}


	//méthode rećoit un vecter et applique une force du sens du vecteur sur le rb du gameobject qui posséde la methode
	void Reaction (Vector2 _point)
	{

		rb.AddForce (_point * puissance);

	}
	//FIN DE LA MÉTHODE REACTION
}

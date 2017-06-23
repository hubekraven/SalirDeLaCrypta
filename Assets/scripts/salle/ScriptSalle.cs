using UnityEngine;
using System.Collections;
using System.Security;

public class ScriptSalle : MonoBehaviour
{

	//==Variables==//

	public Sprite[] decorations;
	public Sprite spritePorteOuverte;
	public Transform[] typeEnnemis;
	public Transform[] items;
	public Transform maCamera;
	public Transform mesEnnemis;
	public Transform _mesPortes;
	public Transform pointRamassageBonus;
	public Sprite spritePorteFerme;
	public bool PersoDetecte = false;
	public bool peutGenerEnnemis = false;
	public bool peutGenerItem = false;
	public int _nbitem;
	public int _ennemiMax = 5;
	private int _nbEnnemis;
	private int _nbPortes;
	private bool peutOuvrir = false;
	private float positionX;
	private float positionY;
	private Transform _tCibleCamera;
	private Vector3 _v3CibleCamera;
	private Vector3 _offset;
	private Transform _maDecoration;
	private Transform _passageSecret;
	private Transform _Pierre;
	private Transform _murPassage;
	private Transform _monTransform;

	// Use this for initialization
	void Start ()
	{
		
		_monTransform = GetComponent<Transform> ();//recuper le transform de ce ma salle
		_tCibleCamera = transform.FindChild ("pointInstantiation");//recuper le point La position du point instantiation  la position en z de ma camera
		_v3CibleCamera = _tCibleCamera.position;
		_v3CibleCamera.z = maCamera.position.z;

		changeDecor ();//appel la fonction change decore;

		_nbitem = 0;
		_passageSecret = transform.FindChild ("passageSecret");//recuper le passage secret

		//recuper ces deux enfants du gameobjet dans des variables 
		if (_passageSecret != null) {
			_Pierre = _passageSecret.GetChild (3);// la pierre 
			_murPassage = _passageSecret.GetChild (1);//le passage
		}

		//recuper elements mesEnnemis 
		if (mesEnnemis == null) {
			mesEnnemis = transform.FindChild ("mesEnnemis");
		}
		//recuper element les portes pour avoir acces a ces enfants (les portes)
		if (_mesPortes == null) {
			_mesPortes = transform.FindChild ("_mesPortes");
		}

		_nbPortes = _mesPortes.childCount;//nombre de portes presentes dans la salle
		positionX = pointRamassageBonus.position.x;
		positionY = pointRamassageBonus.position.y;
	}
	//--fin du Start

	// Update is called once per frame
	void Update ()
	{
		_nbEnnemis = mesEnnemis.childCount;// nombre d' ennemi present dans la salles
		//--fermes les portes et géner les ennemis 
		if (peutGenerEnnemis == true) {
			this.GenererEnnemis ();
			this.fermerPortes ();
			Destroy (mesEnnemis.GetChild (0).gameObject);
			peutOuvrir = true;
			peutGenerEnnemis = false;
		}//--fin condition peutGenererEnnemis

		//--a la detection du personnage change la position de la camera au point instantiation de cette salle
		if (PersoDetecte == true) {
			maCamera.position = _v3CibleCamera;
			PersoDetecte = !PersoDetecte;
		}//--fin de la condition de detection

		//==Gere l ouverture de la porte et l'octroi d"un item ou pas selon la presence d ennemi. 
		//==S il n y pas d ennemi change le sprite et deactive le collider des portes==//
		if ((peutOuvrir == true) && (_nbEnnemis <= 0)) {
			
			if (_nbitem < 1) {
				
				//verifie que la salle a généré un ennemi avant de génerer un item
				if (peutGenerItem == true) {
					this.GenererItems ();
					peutGenerItem = false;
				}

				this.ouvrirPortes ();
				_nbitem++;
				peutOuvrir = false;
			}
		}
		//fin de la gestion de l'ouverture desporte et octroi item
	}
	//--fin de Update
		
	// fonction va generer aléatoirement un item de la salles selon le type de salle
	void GenererItems ()
	{
		int nbaleatoire;
		//Vector3 nouvellePosition;//position d'instantiation de objet

		//routine a suive si ce n'est pas la salle du boss ou du miniboss
		if ((_monTransform.name == "SalleBoss") || (_monTransform.name == "SalleSecrete")) {
			Transform bonusItem = GameObject.Instantiate (items [0], pointRamassageBonus.position, Quaternion.identity) as Transform;
		} // gener l'item du boss à la position du boss
		else {
			
			nbaleatoire = Random.Range (0, items.Length);
			Transform bonusItem = GameObject.Instantiate (items [nbaleatoire], pointRamassageBonus.position, Quaternion.identity) as Transform;
			if (bonusItem.name == "AucunObjet(Clone)") {
				Destroy (bonusItem.gameObject);
			}
		}
	}
	//--Fin de la fonction GenerItems

	// fonction va generer aléatoirement les ennemis de la salles selon le type de salle
	void GenererEnnemis ()
	{
		float hori = Input.GetAxis ("Horizontal");
		float verti = Input.GetAxis ("Vertical");
		Collider2D limite;
		Vector3 nouvellePosition;//position d'instantiation des ennemis

		//routine a suivre si c'est la salle du boss ou du miniboss
		if ((_monTransform.name == "SalleBoss") || (_monTransform.name == "SalleSecrete")) {
			
			//determine la position selon l' input 
			if (hori > 0) {
				nouvellePosition = new Vector3 (positionX + 9f, (positionY), 0f);	
			} else if (hori < 0) {
				nouvellePosition = new Vector3 (positionX - 9f, (positionY), 0f);
			} else if (verti > 0) {
				nouvellePosition = new Vector3 (positionX, (positionY + 7f), 0f);	
			} else if (verti < 0) {
				nouvellePosition = new Vector3 (positionX, (positionY - 7f), 0f);
			} else {
				nouvellePosition = new Vector3 (positionX, (positionY), 0f);
			}
			Transform nouvelEnnemi = GameObject.Instantiate (typeEnnemis [0], nouvellePosition, Quaternion.identity) as Transform;
			nouvelEnnemi.parent = mesEnnemis;
		} else {
			// Si ce n'est pas la salle boss ou miniboss, génere des énnemis selon le nombre d'ennemis maximus a crée dans la salle
			for (int i = 0; i < _ennemiMax;) {
				
				nouvellePosition = new Vector3 (Random.Range ((positionX - 6.0f), (positionX + 6.0f)), Random.Range ((positionY - 4f), (positionY + 4.0f)), 0f);
				limite = Physics2D.OverlapCircle (nouvellePosition, 1f, ~0, 0f, 0f);// verificateur de collision avec un collider dans la salle

				//verifie si dans les limite de la position il n' a pas d'objet et cree un ennemi. si non cherche une autre position. 
				if (limite == null) {
					int nbaleatoire = Random.Range (0, typeEnnemis.Length);
					Transform nouvelEnnemi = GameObject.Instantiate (typeEnnemis [nbaleatoire], nouvellePosition, Quaternion.identity) as Transform;

					//si c'est un objet de type vide detruit le si non ajoute le aux ennemis
					if (nouvelEnnemi.name == "AucunObjet(Clone)") {
						Destroy (nouvelEnnemi.gameObject);
					} else {
						nouvelEnnemi.parent = mesEnnemis;
						peutGenerItem = true;
					}
					//fin verification objet vide 
				} else {
					//cree une nouvelle position
					nouvellePosition = new Vector3 (Random.Range ((positionX - 6.0f), (positionX + 6.0f)), Random.Range ((positionY - 4f), (positionY + 4.0f)), 0f);
				}
				i++;
			}
		}	
	}
	//--fin de La fonction GenererEnnemis ()

	//function qui va fermer les portes de la salle ou du passage secret
	void fermerPortes ()
	{
		if (_monTransform.name != "SalleStart") {
			for (int i = 0; i < _nbPortes; i++) {
				_mesPortes.GetChild (i).GetComponent<SpriteRenderer> ().sprite = spritePorteFerme;
				_mesPortes.GetChild (i).GetComponent<BoxCollider2D> ().enabled = true;
				_mesPortes.GetChild (i).GetChild (0).gameObject.SetActive (false);//deactive le sprite superieur de la porte
			}
			if (_passageSecret != null) {
				//Debug.Log (_murePassage.gameObject.activeSelf);
				if (_murPassage.gameObject.activeSelf == true)
					_Pierre.gameObject.SetActive (true);
			}
		}
	}
	//--fin de la fonction fermePortes()

	//function qui va ouvrir les portes de la salle ou du passage secret
	void ouvrirPortes ()
	{
		for (int i = 0; i < _nbPortes; i++) {
			_mesPortes.GetChild (i).GetComponent<SpriteRenderer> ().sprite = spritePorteOuverte;
			_mesPortes.GetChild (i).GetComponent<BoxCollider2D> ().enabled = false;
			_mesPortes.GetChild (i).GetChild (0).gameObject.SetActive (true);//active le sprite superieur de la porte
		}
		if (_passageSecret != null) {
			//Debug.Log (_murePassage.gameObject.activeSelf);
			if ((_murPassage.gameObject.activeSelf == true) && (_murPassage.gameObject.activeSelf == true)) {
				Destroy (_Pierre.gameObject);
				Destroy (_passageSecret.GetComponent<Rigidbody2D> ());
			}
		}
	}
	//fin de la fonction ouvrirPorte()

	//change le décor en fonction du type de salle (couleur dessin mur et du sol)
	void changeDecor ()
	{
		//si c'est la salle de départ
		if (_monTransform.name == "SalleStart") {
			_maDecoration = transform.FindChild ("Decorations");
			if (_maDecoration != null) {
				for (int i = 8; i < 16; i++) {
					_maDecoration.GetChild (i).GetComponent<SpriteRenderer> ().enabled = false;
				}
			}

			transform.FindChild ("SalleSol").GetComponent<SpriteRenderer> ().sprite = decorations [1];
		}

		//si c' cest la salle secrete
		else if (_monTransform.name == "SalleSecrete") {
			_maDecoration = transform.FindChild ("Decorations");
			if (_maDecoration != null) {
				for (int i = 8; i < 16; i++) {
					_maDecoration.GetChild (i).GetComponent<SpriteRenderer> ().sprite = decorations [4];
				}
			}
			transform.FindChild ("SalleSol").GetComponent<SpriteRenderer> ().sprite = decorations [2];
		}
		//si c' cest la salle du boss
		else if (_monTransform.name == "SalleBoss") {
			_maDecoration = transform.FindChild ("Decorations");
			if (_maDecoration != null) {
				for (int i = 8; i < 16; i++) {
					_maDecoration.GetChild (i).GetComponent<SpriteRenderer> ().sprite = decorations [3];
				}
			}
			transform.FindChild ("SalleSol").GetComponent<SpriteRenderer> ().sprite = decorations [0];
		}
		//--fin du changement 

	}
	//fin de la fonction changeDecor
}

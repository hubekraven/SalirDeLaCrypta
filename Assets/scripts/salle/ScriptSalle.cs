using UnityEngine;
using System.Collections;
using System.Security;
using System.Collections.Generic;

//using System.Collections.Generic;
//using UnityEditor;

public class ScriptSalle : MonoBehaviour
{

	//==Variables==//

	public Sprite[] decorations;

	public Transform[] typeEnnemis;
	public Transform[] objetsSalle;
	public Transform[] items;
	public Transform maCamera;
	public Transform mesEnnemis;
	public Transform _mesPortes;
	public Transform pointRamassageBonus;
	public Sprite spritePorteFerme;
	//public Sprite spritePorteFerme_boss;
	public Sprite spritePorteOuverte;
	//public Sprite spritePorteOuverte_boss;

	public bool PersoDetecte = false;
	public bool peutGenerEnnemis = false;
	public bool peutGenerItem = false;
	public int _nbitem;
	public int _ennemiMax = 5;
	public int _objetsMax = 1;
	private int _nbEnnemis;
	private int _nbPortes;
	private bool peutOuvrir = false;
	private float positionX;
	private float positionY;
	private Transform _tCibleCamera;
	private Vector3 _v3CibleCamera;
	private Vector3 _offset;
	private Vector3 nouvellePosition;//position d'instantiation des ennemis
	private Transform porteEntree;
	private GameObject _persos;
	private Transform _perso;
	private Transform _maDecoration;
	private Transform _mesPassageSecrets;
	private Transform _passageSecret;
	private Transform _Pierre;
	private Transform _murPassage;
	private Transform _monTransform;
	private Transform _mesObjets;
	private Collider2D limite;
	//liste de mes sources audio
	private AudioSource sourceAudio_porte;
	private AudioSource sourceAudio_passageSecret;
	private AudioSource sourceAudio_dropSpecial;
	private AudioSource sourceAudio_apparissionEnn;
	private AudioSource sourceAudio_drop;

	//liste des clip sonore qui seront utlisés
	AudioClip son_itemDrop;

	AudioClip son_passageSecret;
	AudioClip son_porteOuvres;
	AudioClip son_porteFerme;
	AudioClip son_dropSpecial;
	AudioClip son_apparissionEnn;

	// Use this for initialization
	void Start ()
	{
		//gestion des sons
		//REF:https://www.youtube.com/watch?v=rIpQMbDCIQs&t=462s
		//https://docs.unity3d.com/560/Documentation/ScriptReference/Resources.Load.html

		//ajoute la source audio à la salle;
		sourceAudio_porte = gameObject.AddComponent<AudioSource> () as AudioSource;
		sourceAudio_passageSecret = gameObject.AddComponent<AudioSource> () as AudioSource;
		sourceAudio_dropSpecial = gameObject.AddComponent<AudioSource> () as AudioSource;
		sourceAudio_apparissionEnn = gameObject.AddComponent<AudioSource> () as AudioSource;
		sourceAudio_drop = gameObject.AddComponent<AudioSource> () as AudioSource;

		//charge les sons qui vont être joués par la salle
		son_itemDrop = Resources.Load ("sons/item_drop") as AudioClip;
		son_passageSecret = Resources.Load ("sons/portes") as AudioClip;
		son_porteOuvres = Resources.Load ("sons/portes") as AudioClip;
		son_porteFerme = Resources.Load ("sons/portes") as AudioClip;
		son_dropSpecial = Resources.Load ("sons/itemSpecial") as AudioClip;
		son_apparissionEnn = Resources.Load ("sons/pepingrillin_spawn") as AudioClip;
		//Fin gestion des sons

		//recuper le personnage personnage qui est selectionné
		_persos = GameObject.Find ("Persos");
		foreach(Transform child in _persos.transform){
			if(child.gameObject.activeSelf == true){
				_perso = child.transform;
			}
			
		}
		//Debug.Log ("PERSO" + _perso.position.x + ", " + _perso.position.y);

		//recuper la camera
		if (maCamera == null) {
			maCamera = GameObject.Find ("Camera").transform;
		}

		// RECUPERER LE numreo d'un layer: Debug.Log(LayerMask.NameToLayer ("Default"));
		_monTransform = GetComponent<Transform> ();//recuper le transform de ce ma salle
		_tCibleCamera = transform.FindChild ("pointInstantiation");//recuper le point La position du point instantiation  la position en z de ma camera
		_v3CibleCamera = _tCibleCamera.position;
		_v3CibleCamera.z = maCamera.position.z;
		_mesObjets = transform.FindChild ("mesObjets");
		changeDecor ();//appel la fonction change decore;


		_nbitem = 0;
		_mesPassageSecrets = transform.FindChild ("mesPassages");//recuper le passage secret

		//va chercher le passage secret active dans la salle
		foreach (Transform child in _mesPassageSecrets) {
			if (child.gameObject.activeSelf == true) {
				_passageSecret = child;
			}
		}



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
	

		//_nbPortes = _mesPortes.childCount;//nombre de portes presentes dans la salle
		positionX = pointRamassageBonus.position.x;
		positionY = pointRamassageBonus.position.y;
		generObjets ();
	}
	//--fin du Start

	// Update is called once per frame
	void Update ()
	{
		_nbEnnemis = mesEnnemis.childCount;// nombre d' ennemi present dans la salles

		//--fermes les portes et géner les ennemis 
		if (peutGenerEnnemis == true) {
			this.fermerPortes ();
			Invoke ("GenererEnnemis", 0.8f);//appel de la la méthode après x seconde
			Destroy (mesEnnemis.GetChild (0).gameObject);
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
				Invoke ("ouvrirPortes", 0.5f);//appel de la fonction après x secondes
				//this.ouvrirPortes ();
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
		Vector3 nouvellePosition;//position d'instantiation de objet

		//routine a suive si c'est la salle du boss ou du miniboss
		if ((_monTransform.name == "SalleBoss") || (_monTransform.name == "SalleSecrete")) {
			Transform bonusItem = GameObject.Instantiate (items [0], pointRamassageBonus.position, Quaternion.identity) as Transform;
			sourceAudio_drop.clip = son_itemDrop;
			sourceAudio_drop.playOnAwake = false;
			sourceAudio_drop.pitch = 0.8f;
			sourceAudio_drop.Play ();

			//gener l'echelle
			if (_monTransform.name == "SalleBoss") {
				nouvellePosition = new Vector3 ((positionX + 8), (positionY + 8), 0);
				Transform _Echelle = GameObject.Instantiate (items [1], nouvellePosition, Quaternion.identity) as Transform;
				sourceAudio_dropSpecial.clip = son_dropSpecial;
				sourceAudio_dropSpecial.playOnAwake = false;
				//sourceAudio_dropSpecial.PlayDelayed (1f);
				sourceAudio_dropSpecial.Play ();
			}

		} // gener l'item d'une salle
		else {
			sourceAudio_drop.clip = son_itemDrop;
			sourceAudio_drop.pitch = 0.8f;
			nbaleatoire = Random.Range (0, items.Length);
			Transform bonusItem = GameObject.Instantiate (items [nbaleatoire], pointRamassageBonus.position, Quaternion.identity) as Transform;

			if (bonusItem.name == "AucunObjet(Clone)") {
				Destroy (bonusItem.gameObject);
			} else {
				sourceAudio_drop.Play ();
			}
		}
	}
	//--Fin de la fonction GenerItems

	// fonction va generer aléatoirement les ennemis de la salles selon le type de salle
	void GenererEnnemis ()
	{   
		//sourceAudio.Stop ();
		//sourceAudio.clip = son_porteOuvres;
		//sourceAudio.PlayOneShot ( son_apparissionEnnemis);
		sourceAudio_apparissionEnn.clip = son_apparissionEnn;//

		//routine a suivre si c'est la salle du boss ou du miniboss
		if (_monTransform.name == "SalleBoss") {
			
			foreach (Transform portes in _mesPortes) {
				if (portes.gameObject.activeSelf == true) {
					porteEntree= portes;
				}

			}
			if (porteEntree.name == "Porte_new(H)") {
				nouvellePosition = new Vector3 (positionX, (positionY - 5f), 0f);

			}
			else if (porteEntree.name == "Porte_new(B)") {
				nouvellePosition = new Vector3 (positionX, (positionY + 5f), 0f);

			} 
			else if (porteEntree.name == "Porte_new(G)")
			{
				nouvellePosition = new Vector3 (positionX + 9f, positionY, 0f);

			} 
			else if (porteEntree.name == "Porte_new(D)") 
			{
				nouvellePosition = new Vector3 (positionX - 9f, positionY, 0f);
			}

			Transform nouvelEnnemi = GameObject.Instantiate (typeEnnemis [0], nouvellePosition, Quaternion.identity) as Transform;
			nouvelEnnemi.parent = mesEnnemis;
			peutGenerItem = true;
			
		} 

		else if (_monTransform.name == "SalleSecrete") {


			if (_passageSecret.name == "passageSecret(H)") {
				nouvellePosition = new Vector3 (positionX, (positionY - 7f), 0f);

			}
			else if (_passageSecret.name == "passageSecret(B)") {
				nouvellePosition = new Vector3 (positionX, (positionY + 7f), 0f);

			} 
			else if (_passageSecret.name == "passageSecret(G)")
			{
				nouvellePosition = new Vector3 (positionX, (positionY + 7), 0f);

			} 
			else if (_passageSecret.name == "passageSecret(D)") 
			{
				nouvellePosition = new Vector3 (positionX, (positionY + 7), 0f);
			}

			Transform nouvelEnnemi = GameObject.Instantiate (typeEnnemis [0], nouvellePosition, Quaternion.identity) as Transform;
			nouvelEnnemi.parent = mesEnnemis;
			peutGenerItem = true;
		}

		else {

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
		peutOuvrir = true;//peut ouvrir les portes aprés avoir généré les ennemis;
		sourceAudio_apparissionEnn.Play ();
	}
	//--fin de La fonction GenererEnnemis ()

	//function qui va fermer les portes de la salle ou du passage secret
	void fermerPortes ()
	{
		//sourceAudio.Stop ();
		sourceAudio_porte.clip = son_porteOuvres;//assigne le son des portes à la source audio
		sourceAudio_porte.pitch = 0.4f;//change le pitch

		if (_monTransform.name != "SalleStart") {

			foreach (Transform portes in _mesPortes) {
				if (portes.gameObject.activeSelf == true) {

					//verifie si cette cette porte est une porte boss
					if(portes.GetChild (0).gameObject.activeSelf == true){
						portes.GetComponent<SpriteRenderer> ().sprite =  decorations [8];//change le decore de la porte de boss(ferme)
						portes.GetChild (1).GetComponent<SpriteRenderer> ().sprite = decorations [10];//change le dessus de la porte
					}else{
						portes.GetComponent<SpriteRenderer> ().sprite = decorations [5];//prend le decore de la porte normale(ferme)
					}
					portes.GetComponent<BoxCollider2D> ().enabled = true;
					portes.GetChild (1).gameObject.SetActive (true);//deactive le sprite superieur de la porte
							//portes.GetChild (0).gameObject.SetActive (true);//active le sprite superieur de la porte
				}
			}
			if (_passageSecret != null) {
				//Debug.Log (_murePassage.gameObject.activeSelf);
				if (_murPassage.gameObject.activeSelf == true)
					_Pierre.gameObject.SetActive (true);
			}
			sourceAudio_porte.Play ();//joue le son
		}
	
	}
	//--fin de la fonction fermePortes()

	//function qui va ouvrir les portes de la salle ou du passage secret
	void ouvrirPortes ()
	{
		sourceAudio_porte.clip = son_porteFerme;
		sourceAudio_porte.playOnAwake = false;
		sourceAudio_porte.pitch = 1.5f;

		foreach (Transform portes in _mesPortes) {
			if (portes.gameObject.activeSelf == true) {

				//verifie si cette cette porte est une porte boss
				if(portes.GetChild (0).gameObject.activeSelf == true){
					portes.GetComponent<SpriteRenderer> ().sprite =  decorations [9];//change le decore de la porte de boss(ouvert)
					portes.GetChild (1).GetComponent<SpriteRenderer> ().sprite = decorations [10];//change le dessus de la porte
				}else{
					portes.GetComponent<SpriteRenderer> ().sprite = decorations [6];//prend le decore de la porte normale(ouvert)
				}
				portes.GetComponent<BoxCollider2D> ().enabled = false;
				portes.GetChild (1).gameObject.SetActive (true);//deactive le sprite superieur de la porte
			}
		}
		if (_passageSecret != null) {
			//Debug.Log (_murePassage.gameObject.activeSelf);
			if ((_murPassage.gameObject.activeSelf == true) && (_murPassage.gameObject.activeSelf == true)) {
				Destroy (_Pierre.gameObject);
				Destroy (_passageSecret.GetComponent<Rigidbody2D> ());
			}
		}
		sourceAudio_porte.Play ();
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
					_maDecoration.GetChild (i).GetComponent<SpriteRenderer> ().color = new Color (203f, 140f, 140f, 255f);
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

			foreach (Transform portes in _mesPortes) {
				if (portes.gameObject.activeSelf == true) {
					portes.GetComponent<SpriteRenderer> ().sprite = decorations [9];//prend le decore de la porte boss ouverte
					portes.GetChild(1).GetComponent<SpriteRenderer> ().sprite = decorations [10];//prend le decore du dessus de la porte boss
				}
			}
		}
		//si c' cest la salle du boss
		else {
				foreach(Transform portes in _mesPortes)
				{
					if (portes.gameObject.activeSelf == true)
					{
						//verifie si cette cette porte est une porte boss
						if(portes.GetChild (0).gameObject.activeSelf == true){
							portes.GetComponent<SpriteRenderer> ().sprite =  decorations [9];//change le decore pour la porte de boss
							portes.GetChild (1).GetComponent<SpriteRenderer> ().sprite =  decorations [10];//le dessus la porteBoss
						}//portes.GetComponent<BoxCollider2D> ().enabled = false;
						//portes.GetChild (0).gameObject.SetActive (true);//active le sprite superieur de la porte
					}
				}
			}
		//--fin du changement 

		//}
	}
	//fin de la fonction changeDecor

	// fonction qui va génere aléatoirement des objets dans une salle//
	void generObjets ()
	{
		
		int nbaleatoire;
		Vector3 nouvellePosition;//position d'instantiation de objet
		Collider2D limite;

		if ((_monTransform.name != "SalleBoss") || (_monTransform.name != "SalleSecrete")) {
			//ajouter un objet dans un tableau(utiliser une liste): http://answers.unity3d.com/questions/57139/c-adding-items-to-an-already-created-array.html
			//initialiser la variable liste http://answers.unity3d.com/questions/380567/error-cs0165-use-of-unassigned-local-variable.html
			List<Transform> _objetCree = new List<Transform> ();
			nbaleatoire = Random.Range (0, objetsSalle.Length);
			if (objetsSalle.Length != 0) {
				for (int i = 0; i < _objetsMax;) {
					nouvellePosition = new Vector3 (Random.Range ((positionX - 7.0f), (positionX + 7.0f)), Random.Range ((positionY - 5f), (positionY + 5.0f)), 0f);
					limite = Physics2D.OverlapCircle (nouvellePosition, 2f, 21, 0f, 0f);// verificateur de collision avec un collider dans la salle dans layer monde
					if (limite == null) {
						Transform nouvelObjet = GameObject.Instantiate (objetsSalle [nbaleatoire], nouvellePosition, Quaternion.identity) as Transform;
						//nouvelObjet.parent = _mesObjets;
						if (nouvelObjet.name == "AucunObjet(Clone)") {
							Destroy (nouvelObjet.gameObject);
						} else {
							_objetCree.Add (nouvelObjet);//ajoute l'objet crée à la liste objetCree
						}
						i++;
					}
				}
				//parcourir la liste http://answers.unity3d.com/questions/380567/error-cs0165-use-of-unassigned-local-variable.html
				foreach (Transform _objetListe in _objetCree) {
					_objetListe.parent = _mesObjets;//deplace chaque element dans la liste objetCree vers  mesObjets
				}
			} else {
				objetsSalle = new Transform[0];
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class tete : MonoBehaviour {
	
	public SpriteRenderer teteSprite;
	public Sprite[] choixTete; 

	// Use this for initialization
	void Start () {
		this.teteSprite = GetComponent<SpriteRenderer>();  

		this.teteSprite.sprite = choixTete [2];
	}
	
	// Update is called once per frame
	void Update () {
		this.teteSprite.sprite = choixTete [2];

		if(Input.GetKey(KeyCode.UpArrow))	
		{
			if (this.teteSprite.sprite != choixTete [0]) {
				this.teteSprite.sprite = choixTete [0];
			}
		}

		if(Input.GetKey(KeyCode.LeftArrow))	
		{
			if (this.teteSprite.sprite != choixTete [1]) {
				this.teteSprite.sprite = choixTete [1];
			}
		}

		if(Input.GetKey(KeyCode.DownArrow))	
		{
			if (this.teteSprite.sprite != choixTete [2]) {
				this.teteSprite.sprite = choixTete [2];
			}
		}

		if(Input.GetKey(KeyCode.RightArrow))	
		{
			if (this.teteSprite.sprite != choixTete [3]) {
				this.teteSprite.sprite = choixTete [3];
			}
		}
		// -------------- fin du changement de tête
	}
}
using UnityEngine;
using System.Collections;

public class pdvEnnemi : MonoBehaviour
{

	public float vieRestante;


	void Toucher (float dmg)
	{
		vieRestante -= dmg;
		if (vieRestante <= 0) {
			GameObject.Destroy (this.gameObject);
		}
	}

}

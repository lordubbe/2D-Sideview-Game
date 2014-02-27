using UnityEngine;
using System.Collections;

public class WeaponPickUp : MonoBehaviour {
	public bool pickedUp = false;
	public Sprite pickedUpSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			pickedUp = true;
			print(col.gameObject.name+" picked up "+gameObject.name);
			gameObject.collider2D.enabled = false;
			gameObject.AddComponent<RotateBasedOnMouse>();
			Destroy(GetComponent<Rigidbody2D>());
			gameObject.GetComponent<SpriteRenderer>().sprite = pickedUpSprite;
		}
	}

}

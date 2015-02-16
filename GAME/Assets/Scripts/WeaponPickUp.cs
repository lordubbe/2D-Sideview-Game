using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponPickUp : MonoBehaviour {
	public bool pickedUp = false;
	public Sprite pickedUpSprite;
	private Sprite defaultSprite;
	private bool dropped;

	// Use this for initialization
	void Start () {
		defaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Q)){
			//drop weapon
			if(pickedUp){
				print ("dropping "+gameObject.name);
				pickedUp = false;
				//gameObject.collider2D.enabled = true;
				gameObject.AddComponent<Rigidbody2D>();
				gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
				GameObject player = GetComponent<RotateBasedOnMouse>().player;
				GetComponent<Rigidbody2D>().AddForce((player.GetComponent<Rigidbody2D>().velocity.normalized+Vector2.up)*5f, ForceMode2D.Impulse);
				if(player.transform.localScale.x > 0){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(1f,0), ForceMode2D.Impulse);
				}else{
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f,0), ForceMode2D.Impulse);
				}
				Destroy (GetComponent<RotateBasedOnMouse>());
				dropped = true;
				gameObject.GetComponent<Collider2D>().enabled = true;
				gameObject.GetComponent<Collider2D>().isTrigger = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player" && !dropped){
			pickedUp = true;
			print(col.gameObject.name+" picked up "+gameObject.name);
			gameObject.GetComponent<Collider2D>().enabled = false;
			gameObject.AddComponent<RotateBasedOnMouse>();
			Destroy(GetComponent<Rigidbody2D>());
			gameObject.GetComponent<SpriteRenderer>().sprite = pickedUpSprite;
			col.gameObject.GetComponent<PlayerStats>().weapons.Add(gameObject.transform);//Add to player inventory
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			dropped = false;
			gameObject.GetComponent<Collider2D>().isTrigger = false;
		}
	}

}

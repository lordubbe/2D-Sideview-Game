using UnityEngine;
using System.Collections;

public class FreezeBullet : MonoBehaviour {

	public float bulletSpeed = 100f;

	public Transform iceEffect;

	private Vector3 direction;
	public Transform FreezeBulletDeath;
	public float damage = 10;

	// Use this for initialization
	void Start () {
		direction = Vector3.Normalize ((Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position)))*100;
		rigidbody2D.velocity = direction*bulletSpeed*Time.deltaTime;

		StartCoroutine(waitAndKill(1.3f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator waitAndKill(float time){
		yield return new WaitForSeconds(time);
		killBullet();
	}

	IEnumerator waitAndEnableCollider(float time){
		yield return new WaitForSeconds(time);
		transform.collider2D.enabled = true;
	}

	void killBullet(){
		Instantiate(FreezeBulletDeath, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D collider){
		print ("hit "+collider.transform.name);//PRINT WHAT IT HITS
		if(collider.gameObject.name != "FreezeBulletSpawn" && collider.gameObject.name != "FreezeGun" && collider.gameObject.name != "Player"){//if it's not another instance of itself and not the player
			//print ("hit "+collider.transform.name);//PRINT WHAT IT HITS
			//If bullet hits player
			if(collider.gameObject.tag == "Player"){//on player hit
				collider.gameObject.GetComponent<HealthScript>().health -= damage;
			}

			if(collider.gameObject.name != "FreezeBulletIce" && collider.gameObject.name == "Statue"){//stupid hard-coded shit
				Transform iceBlock = Instantiate(iceEffect, new Vector2(collider.transform.position.x, collider.transform.position.y-0.416f), Quaternion.identity) as Transform;//spawn iceblock thing (not very well done atm, but it works)
				iceBlock.parent = collider.transform;
			}

			killBullet();

		}
	}
}

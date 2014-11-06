using UnityEngine;
using System.Collections;

public class FireChargeBullet : MonoBehaviour {

	public enum Mode{
		Primary, Secondary
	};

	public Mode _mode;
	public Transform FireChargeExplosion;
	private Transform bulletSpawn;
	private Transform fireChargeGun;

	public float bulletSpeed = 1.0f;
	private Vector2 direction;
	private bool chargeFired = false;
	private bool wasAlreadyFired = false;
	public float killTime = 2f;
	public float fireSpeed = 1000f;
	public float damage;
	
	private Vector3 maxLengthForCharge;

	void Start () {
		bulletSpawn = GameObject.Find("FireBulletSpawn").transform;
		fireChargeGun = GameObject.Find("FireChargeGun").transform; 
		//transform.collider2D.enabled = false;
	}


	// Update is called once per frame	
	void Update () {
		maxLengthForCharge = Vector3.Normalize(fireChargeGun.GetComponent<RotateBasedOnMouse>().lookPos)*1;
		Debug.DrawLine(bulletSpawn.transform.position, transform.position);//DEBUG LINE

		if(_mode != null){
			if(_mode == Mode.Secondary){//Secondary shot (charge)
				if(Input.GetButton("Fire2")){
					if(!wasAlreadyFired){
						trackMouse();
					}
				}else{
					if(!wasAlreadyFired){//Release charge
/*change this pls*/		rigidbody2D.AddForce((new Vector2(fireChargeGun.GetComponent<FireChargeGun>().lookDir.x, fireChargeGun.GetComponent<FireChargeGun>().lookDir.y)+fireChargeGun.GetComponent<RotateBasedOnMouse>().player.rigidbody2D.velocity.normalized)*bulletSpeed, ForceMode2D.Impulse);
/*!!!!!!!!!!!!!!!*/		wasAlreadyFired = true;
						StartCoroutine(waitAndKill(killTime));
					}
				}
			}
			if(_mode == Mode.Primary){//normal shot
				if(!wasAlreadyFired){
					Vector2 lookDir = new Vector2(fireChargeGun.GetComponent<FireChargeGun>().lookDir.x, fireChargeGun.GetComponent<FireChargeGun>().lookDir.y);
					Vector2 playerVel = fireChargeGun.GetComponent<RotateBasedOnMouse>().player.rigidbody2D.velocity.normalized;
					print (playerVel);
					rigidbody2D.AddForce((lookDir).normalized*bulletSpeed, ForceMode2D.Impulse);
					wasAlreadyFired = true;
				}
				StartCoroutine(waitAndKill(killTime));
			}
		}
		if(wasAlreadyFired){	
		//	StartCoroutine(waitAndEnableCollider(0.1f));
		}
		if(Input.GetButtonDown ("Fire1") || Input.GetButtonDown("Fire2")){//kill bullets in scene on click
			StartCoroutine(waitAndKill(0.1f));
		}
	}
	
	IEnumerator waitAndKill(float time){
		yield return new WaitForSeconds(time);
		Instantiate(FireChargeExplosion, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
		if(gameObject.GetComponent<FireChargeGun>().bulletsInGame != null){
			fireChargeGun.GetComponent<FireChargeGun>().bulletsInGame-=1;
		}
	}

	IEnumerator waitAndEnableCollider(float time){
		yield return new WaitForSeconds(time);
		collider2D.enabled = true;
		//gameObject.AddComponent<DontGoThroughThings>();
	}

	
	void trackMouse(){
		//direction = (Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position));//AT MOUSE POSITION
		direction = (bulletSpawn.transform.position-(maxLengthForCharge*-0.8f)-transform.position);//DEFINED MAX-LENGTH (0.8f)
		rigidbody2D.velocity = direction*bulletSpeed*60*Time.deltaTime;
	}

	void killBullet(){
		Instantiate(FireChargeExplosion, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
		if(gameObject.GetComponent<FireChargeGun>().bulletsInGame != null){
			fireChargeGun.GetComponent<FireChargeGun>().bulletsInGame-=1;
		}
	}

	void OnCollisionEnter2D(Collision2D collider){
		if(collider.gameObject.name != this.name && collider.gameObject.name != "FireChargeGun"){//if it's not another instance of itself and not the player
			//If bullet hits player
			if(collider.gameObject.tag == "Player"){//on player hit
				collider.gameObject.GetComponent<HealthScript>().health -= damage;
				print ("player took "+damage+" damage");
			}
			killBullet();
		}
	}

	
}


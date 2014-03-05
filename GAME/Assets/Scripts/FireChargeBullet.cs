using UnityEngine;
using System.Collections;

public class FireChargeBullet : MonoBehaviour {

	public Transform FireChargeExplosion;
	public float bulletSpeed = 100f;
	private Vector2 direction;
	private bool chargeFired = false;
	private bool wasAlreadyFired = false;
	public float killTime = 2f;
	public float fireSpeed = 1000f;
	public float damage;
	
	private Vector3 maxLengthForCharge;

	void Start () {

	}


	// Update is called once per frame	
	void Update () {
		maxLengthForCharge = Vector3.Normalize(GameObject.Find("FireChargeGun").GetComponent<RotateBasedOnMouse>().lookPos)*1.5f;
		
		Debug.DrawLine(GameObject.Find("FireBulletSpawn").transform.position, transform.position);//DEBUG LINE
		//
		Debug.DrawRay(GameObject.Find("FireBulletSpawn").transform.position, maxLengthForCharge, Color.yellow);//NORMALIZED VECTOR
		//
		if(Input.GetMouseButton(0)){//MOUSE HOLD
			if(!wasAlreadyFired){//if it's not an old shot
				trackMouse();
			}else if(wasAlreadyFired){
				StartCoroutine(waitAndKill(0.1f));
			}
			chargeFired = false;
		}else if(Input.GetMouseButtonUp(0)){//MOUSE RELEASE
			rigidbody2D.AddForce(Vector3.Normalize((transform.position-GameObject.Find("FireBulletSpawn").transform.position))*fireSpeed);
			chargeFired = true;
			wasAlreadyFired = true;
			StartCoroutine(waitAndKill(killTime));
		}
	}
	
	IEnumerator waitAndKill(float time){
		yield return new WaitForSeconds(time);
		Instantiate(FireChargeExplosion, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
		if(gameObject.GetComponent<FireChargeGun>().bulletsInGame != null){
			GameObject.Find("FireChargeGun").GetComponent<FireChargeGun>().bulletsInGame-=1;
		}
	}
	
	void trackMouse(){
		direction = (Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position));
		rigidbody2D.velocity = direction*bulletSpeed*Time.deltaTime;
	}

	void killBullet(){
		Instantiate(FireChargeExplosion, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
		if(gameObject.GetComponent<FireChargeGun>().bulletsInGame != null){
			GameObject.Find("FireChargeGun").GetComponent<FireChargeGun>().bulletsInGame-=1;
		}
	}

	void OnCollisionEnter2D(Collision2D collider){
		if(collider.gameObject.name != this.name){//if it's not another instance of itself and not the player
			//print ("hit "+collider.transform.name);//PRINT WHAT IT HITS
			killBullet();
			//If bullet hits player
			if(collider.gameObject.tag == "Player"){//on player hit
				collider.gameObject.GetComponent<HealthScript>().health -= damage;
				killBullet();
			}
		}
	}

	
}


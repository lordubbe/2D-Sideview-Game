using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponPickUp))]

public class RPG : MonoBehaviour {
	
	public Transform bullet;
	public Transform bulletSpawn;
	
	public float ShootInterval = 0.3f;
	private float nextShotTime;
	public int maxBullets = 10;
	public int bulletsInGame;
	public Vector3 lookDir;

	// Use this for initialization
	void Start () {
		bulletsInGame=0;
	}
	
	// Update is called once per frame
	void Update () {

		if(bulletsInGame < 0){
			bulletsInGame = 0;
		}
		//SHOOT
		if(gameObject.GetComponent<WeaponPickUp>().pickedUp){//GameObject.Find ("FireChargeGun").GetComponent<WeaponPickUp>().pickedUp){
			lookDir = Vector3.Normalize(GetComponent<RotateBasedOnMouse>().lookPos);
			Debug.DrawRay (bulletSpawn.transform.position, lookDir, Color.green);

			if(Input.GetButton("Fire1") && nextShotTime < Time.time && bulletsInGame < maxBullets){
				Transform projectile = Instantiate(bullet, bulletSpawn.transform.position, transform.rotation) as Transform;// as GameObject;
				projectile.GetComponent<RPGbullet>()._mode = RPGbullet.Mode.Primary;
				bulletsInGame++;
				nextShotTime = Time.time+ShootInterval;
			}else if(Input.GetButton("Fire2") && nextShotTime < Time.time && bulletsInGame < maxBullets){
				Transform projectile = Instantiate(bullet, bulletSpawn.transform.position, transform.rotation) as Transform;// as GameObject;
				projectile.GetComponent<RPGbullet>()._mode = RPGbullet.Mode.Secondary;
				bulletsInGame++;
				nextShotTime = Time.time+ShootInterval;
			}
		}

	}

}

using UnityEngine;
using System.Collections;

public class FreezeGun : MonoBehaviour {
	
	public Transform bullet;
	public Transform bulletSpawn;
	
	public float ShootInterval;
	private float nextShotTime;
	public int maxBullets;
	public int bulletsInGame;
	
	// Use this for initialization
	void Start () {
		bulletsInGame=0;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<WeaponPickUp>().pickedUp){
			if(bulletsInGame < 0){
				bulletsInGame = 0;
			}
			//print (bulletsInGame);
			//SHOOT
			if(Input.GetButton("Fire1") && nextShotTime < Time.time && bulletsInGame < maxBullets){
				/*//FIRE CHARGE GUN SCRIPT
				Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);// as GameObject;
				bulletsInGame++;
				nextShotTime = Time.time+ShootInterval;
				*/
				print ("FREEZE LOL");
				nextShotTime = Time.time+ShootInterval;
			}
		}
	}
}

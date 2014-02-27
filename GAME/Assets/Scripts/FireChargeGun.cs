using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponPickUp))]

public class FireChargeGun : MonoBehaviour {
	
	public Transform bullet;
	public Transform bulletSpawn;
	
	public float ShootInterval = 0.3f;
	private float nextShotTime;
	public int maxBullets = 10;
	public int bulletsInGame;


	// Use this for initialization
	void Start () {
		bulletsInGame=0;
	}
	
	// Update is called once per frame
	void Update () {

		if(bulletsInGame < 0){
			bulletsInGame = 0;
		}
		//print (bulletsInGame);
		//SHOOT
		if(gameObject.GetComponent<WeaponPickUp>().pickedUp){//GameObject.Find ("FireChargeGun").GetComponent<WeaponPickUp>().pickedUp){
			if(Input.GetButton("Fire1") && nextShotTime < Time.time && bulletsInGame < maxBullets){
				Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);// as GameObject;
				bulletsInGame++;
				nextShotTime = Time.time+ShootInterval;
			}
		}
	}
}

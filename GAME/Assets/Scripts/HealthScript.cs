using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public float health;
	public uint totalLives;
	bool gameOver;

	private uint livesLeft;

	// Use this for initialization
	void Start () {
		gameOver = false;
		livesLeft = totalLives;

	}
	
	// Update is called once per frame
	void Update () {

		if(health <= 0 ){//no negative hp, hp=0 is one life down
			health = 0;
			if(livesLeft>0){//if still lives left
				//respawn();
				livesLeft--;
			}else if(livesLeft <= 0){//if no lives left
				livesLeft = 0;
				gameOver = true;
				print ("GAME OVER FAGET GET REKT");
			}
		}

	}
}//lel

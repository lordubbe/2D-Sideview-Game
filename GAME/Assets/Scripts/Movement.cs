using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour {
	
	public float MoveSpeed = 6f;
	public float jumpHeight = 1900f;
	
	public bool isJumping = false;
	public bool isGrabbingWall = false;

	private Vector2 oldVelocity;
	private Vector2 newVelocity;

	private float summedDistance;
	private float leftRayDist;
	private float rightRayDist;
	private float sideRayDist;

	private Vector2 leftRay;
	private Vector2 rightRay;
	private Vector2 sideRay;
	private RaycastHit2D sideHit;

	void Update () {
		 

		//print (isJumping);
		Vector2 direction = Vector2.zero;
		float speed = MoveSpeed;
		
		oldVelocity = rigidbody2D.velocity;
		direction.x = Input.GetAxis("Horizontal");
		direction.y = 0;

		newVelocity = new Vector2(direction.x*speed, oldVelocity.y);
		rigidbody2D.velocity = newVelocity;

		//CAST RAYS
			//BOTTOM (GROUND DETECTION)
			leftRay = new Vector3(transform.position.x-0.19f, transform.position.y-0.41f, transform.position.z);
			rightRay = new Vector3(transform.position.x+0.19f, transform.position.y-0.41f, transform.position.z);
			Debug.DrawRay(leftRay, Vector3.down, Color.green, 0.1f);
			Debug.DrawRay(rightRay, Vector3.down, Color.red, 0.1f);
			RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(leftRay.x, leftRay.y), -Vector2.up);
			RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(rightRay.x, rightRay.y), -Vector2.up);

			//SIDE (WALL GRABBING)
			if(transform.localScale.x>0){//if facing right)
				sideRay = new Vector2(transform.position.x+0.206f, transform.position.y-0.40f);
				Debug.DrawRay (sideRay, Vector2.right, Color.blue, 0.1f);
				sideHit = Physics2D.Raycast (new Vector2(sideRay.x, sideRay.y), Vector2.right);
			}else if(transform.localScale.x<0){
				sideRay = new Vector2(transform.position.x-0.206f, transform.position.y-0.40f);
				Debug.DrawRay (sideRay, -Vector2.right, Color.blue, 0.1f);
				sideHit = Physics2D.Raycast (new Vector2(sideRay.x, sideRay.y), -Vector2.right);
			}

		//CALCULATE DISTANCES
		leftRayDist = (transform.position.y-leftHit.point.y);
		rightRayDist = (transform.position.y-rightHit.point.y);

		//ENABLE WALL JUMP
		if(sideHit.collider != null){//if it hits something
			sideRayDist = Vector2.Distance ( sideRay, sideHit.point);
			//print (sideRayDist);
			if(sideRayDist < 0.03f ){//&& (Input.GetAxisRaw("Horizontal") != 0)){//close to wall and pressing a direction
				isGrabbingWall = true;
				//rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);//PREVENT WALL CLIMP
			}else{isGrabbingWall = false;}
		}

		//PREVENT DOUBLE-JUMPING
		if(isGrabbingWall || leftRayDist<0.5 || rightRayDist<0.5){
			if(rigidbody2D.velocity.y == 0){
				isJumping = false;
			}
		}else if(!isGrabbingWall && (leftRayDist>0.5 && rightRayDist>0.5)){
			if(rigidbody2D.velocity.y != 0){	
				isJumping = true; 
			}
		}
		//JUMP
		if(Input.GetButtonDown("Jump") && !isJumping){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			isJumping = true;
			if(isGrabbingWall && Input.GetAxisRaw("Horizontal")<0){
				//print ("WALL JUMP");
				rigidbody2D.AddForce(new Vector2(jumpHeight/2, 0f));
			}else if(isGrabbingWall && Input.GetAxisRaw("Horizontal")>0){
				//print ("WALL JUMP");
				rigidbody2D.AddForce(new Vector2(-jumpHeight/2, 0f));
			}
			rigidbody2D.AddForce(new Vector2(0, jumpHeight));
		}

	}
	
}

using UnityEngine;
using System.Collections;

public class FlipBasedOnDirection : MonoBehaviour {

	bool right = false, left = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//GET DIRECTION
		if(Input.GetAxisRaw("Horizontal")>0){
			right = true;
		}
		if(Input.GetAxisRaw("Horizontal")<0){
			left = true;
		}
		
		//FLIP
		if(right){
			right=false;
			transform.localScale = new Vector3(10, 10, 1);
		}
		if(left){
			left=false;
			transform.localScale = new Vector3(-10, 10, 1);
		}
			
	}
}

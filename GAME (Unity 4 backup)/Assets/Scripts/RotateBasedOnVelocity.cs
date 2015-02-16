using UnityEngine;
using System.Collections;

public class RotateBasedOnVelocity : MonoBehaviour {
	
	public Rigidbody2D PhysicsTarget;
	
	public float MaxAngle;
	public float MaxVelocity;
	public float TweenSpeed = 10f;
	private Vector3 startEuler;
	// Use this for initialization
	void Start () {
		startEuler = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = Vector3.zero;
		offset.z = (Mathf.Clamp(PhysicsTarget.velocity.x, -MaxVelocity, MaxVelocity)/MaxVelocity)*MaxAngle;
		transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, startEuler + offset, TweenSpeed);
	}
}

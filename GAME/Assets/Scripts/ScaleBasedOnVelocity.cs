using UnityEngine;
using System.Collections;

public class ScaleBasedOnVelocity : MonoBehaviour {
	
	public Rigidbody2D PhysicsTarget;
	
	public Vector3 OffsetScale;
	public float MaxVelocity;
	public float TweenSpeed = 10f;
	
	private Vector3 startScale;
	private float dir;
	// Use this for initialization
	void Start () {
		startScale = transform.localScale;

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 offset = Vector3.zero;
		offset.x = (Mathf.Clamp((-PhysicsTarget.velocity.y),-MaxVelocity,MaxVelocity)/MaxVelocity)*OffsetScale.x;
		transform.localScale = Vector3.Lerp(transform.localScale, startScale + offset, Time.deltaTime*TweenSpeed);

	}
}

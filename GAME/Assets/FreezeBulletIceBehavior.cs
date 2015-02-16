using UnityEngine;
using System.Collections;

public class FreezeBulletIceBehavior : MonoBehaviour {

	public float freezeDuration = 5.0f;
	private Color materialColor;

	private float startAlpha;
	private float currentAlpha;
	private ParticleSystem originalParticles;


	void Start(){
		materialColor = gameObject.GetComponent<Renderer>().material.color;
		StartCoroutine(waitAndKill (freezeDuration));
		startAlpha = gameObject.GetComponent<Renderer>().material.color.a;
		currentAlpha = startAlpha;
		originalParticles = gameObject.GetComponentInChildren<ParticleSystem>();
	}

	IEnumerator waitAndKill(float time){
		yield return new WaitForSeconds(time);
		GameObject.Destroy(gameObject);
	}

	void Update(){
		//fade out ice object
		gameObject.GetComponent<Renderer>().material.color = new Color (materialColor.r,materialColor.g,materialColor.b, currentAlpha);
		//fade out ice object particles
		gameObject.GetComponentInChildren<ParticleSystem>().startColor = new Color(originalParticles.startColor.r, originalParticles.startColor.g, originalParticles.startColor.b, currentAlpha);

		//decrease the current alpha
		if(currentAlpha>0){
			currentAlpha-=((freezeDuration/100)*freezeDuration)*Time.deltaTime;
		}


		//disable collider one currentAlpha is basically invisible
		if(currentAlpha<0.1f){
		   gameObject.GetComponent<Collider2D>().enabled = false;
		}
	}

}

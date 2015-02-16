using UnityEngine;
using System.Collections;

public class DestroyParticleOnDeath : MonoBehaviour {

	void LateUpdate () 
	{
		if (!GetComponent<ParticleSystem>().IsAlive())
			Object.Destroy (this.gameObject);	
	}
}

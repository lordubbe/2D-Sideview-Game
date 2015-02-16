using UnityEngine;
using System.Collections;

public class DestroyParticleOnDeath : MonoBehaviour {

	void LateUpdate () 
	{
		if (!particleSystem.IsAlive())
			Object.Destroy (this.gameObject);	
	}
}

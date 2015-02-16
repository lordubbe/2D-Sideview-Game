using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour {

	public GameObject weightPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
		if ( Input.GetMouseButtonDown(0) ) {
		
			var pt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pt.z = 3;
			Instantiate(weightPrefab,pt,Quaternion.identity);
		
		}
	
	}
}

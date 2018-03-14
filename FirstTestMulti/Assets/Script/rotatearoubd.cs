using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatearoubd : MonoBehaviour {

	public Transform center;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (center);
		transform.RotateAround (center.position, new Vector3 (3.0f, 1.0f, 1.0f), 50 * Time.deltaTime);
	}
}

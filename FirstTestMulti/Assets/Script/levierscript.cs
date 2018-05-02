using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levierscript : MonoBehaviour
{

	public List<GameObject> thingtoopen;
	public bool isdefinitiv = true;
	public float duration = 2;
	private float time = 0;

	private bool havetocompt = false;
	
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (havetocompt)
		{
			time += Time.deltaTime;
		}
		if (time > duration)
		{
			havetocompt = false;
			foreach (var objectt in thingtoopen)
			{

				objectt.gameObject.SetActive(!objectt.gameObject.activeInHierarchy);
			}
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
		{
				
			foreach (var objectt in thingtoopen)
			{
				
				objectt.gameObject.SetActive(!objectt.gameObject.activeInHierarchy);
			}
			if (!isdefinitiv)
			{
				havetocompt = true;
			}
			time = 0;
		}
	}
}
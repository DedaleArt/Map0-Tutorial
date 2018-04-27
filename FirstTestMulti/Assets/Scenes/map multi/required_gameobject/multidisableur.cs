using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class multidisableur : NetworkBehaviour
{
	
	[SerializeField]
	public Behaviour[] thingtodisable;
	
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
		{
			foreach (var elementsBehaviour in thingtodisable)
			{
				elementsBehaviour.enabled = false;
			}
		}
		else
		{
			
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}

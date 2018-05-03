using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
	public List<GameObject> checkpoints;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Joueur"))
		{
			foreach (var checkpoint in checkpoints)
			{
				
			}
		}

		
	}
}

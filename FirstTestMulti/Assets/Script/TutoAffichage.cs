using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoAffichage : MonoBehaviour
{

	public Canvas display;
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Joueur"))
		{
			display.enabled = true;
		}

		
	}


	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag("Joueur"))
		{
			display.enabled = true;
		}
	}
}

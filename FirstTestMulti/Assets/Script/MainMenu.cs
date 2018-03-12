using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Solo(string SolButn)
	{
		SceneManager.LoadScene(SolButn);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}

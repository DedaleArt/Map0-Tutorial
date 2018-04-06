using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	public void LoadGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void BackMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void ReloadMap()
	{
		SceneManager.LoadScene("Map0");
	}
	
	public void Quit()
	{
		Debug.Log("QUIT");
		Application.Quit ();
	}
}

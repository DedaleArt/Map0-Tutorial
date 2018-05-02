using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public string LevelCurrent;
	
	public void LoadGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void BackMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void ReloadMap(string LevelCurrent)
	{
		SceneManager.LoadScene(LevelCurrent);
	}
	
	public void Quit()
	{
		Debug.Log("QUIT");
		Application.Quit ();
	}
}

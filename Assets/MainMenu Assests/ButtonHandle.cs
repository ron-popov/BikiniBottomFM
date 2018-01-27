using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnStartGame(string characterName)
	{
		PlayerPrefs.SetString("CharacterName", characterName);
		SceneManager.LoadScene("scene1", LoadSceneMode.Single);
	}

	public void OnCredits()
	{
		SceneManager.LoadScene("CreditsScene", LoadSceneMode.Single);
	}

	public void OnExit()
	{
		Application.Quit();
	}
}

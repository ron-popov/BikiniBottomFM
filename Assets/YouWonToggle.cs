using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWonToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("DidWin") == 1)
		{
			PlayerPrefs.SetInt("DidWin", 0);
		}
		else
		{
			GameObject.Find("Canvas/Panel/Text").SetActive(false);
			GameObject.Find("Canvas/Panel/Text (1)").SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
		{
			Application.Quit();
		}
	}
}

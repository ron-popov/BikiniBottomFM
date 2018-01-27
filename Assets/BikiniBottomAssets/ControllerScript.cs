using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour {

	private const float minDistanceFromTower = 10.0f;

	private const int towerCount = 6;

	private Color takenColor = Color.red;

	private Color freeColor = Color.green;

	private AudioSource audioSource = null;
	private float SoundTimeSpan = 0.0f;
	private float timespan = 0.0f;
	// Goes through the data saved about each tower and paints it accordingly
	public void PaintTowers()
	{
		GameObject[] radioTowers = GameObject.FindGameObjectsWithTag("RadioTower");
		foreach (GameObject radioTower in radioTowers)
		{
			int towerId = int.Parse(radioTower.name.Substring(5));

			bool isTaken = (PlayerPrefs.GetInt("Tower" + towerId) == 1);
			//Find the tower gameobject

			if(radioTower == null)
			{
				Debug.Log("NULL TOWER ERROR");
			}
			else
			{
				PaintObject(radioTower, isTaken ? takenColor : freeColor);
			}
		}
	}

	private void PaintObject(GameObject o, Color c)
	{
		Renderer r = o.GetComponent<Renderer>();
		if(r != null)
		{
			r.material.color = c;
		}

		foreach(Transform child in o.transform)
		{
			GameObject childGameObject = child.gameObject;
			PaintObject(childGameObject, c);
		}

	}


	// Use this for initialization
	void Start () 
	{
		PaintTowers();
		PlayMusic();
		UpdateBuildingText();
	}

	void UpdateBuildingText()
	{
		int counter = 0;
		for(int i = 1 ; i <= towerCount ; i++)
		{
			bool state = (PlayerPrefs.GetInt("Tower" + i) == 1);
			if(state)
			{
				counter++;
			}
		}

		GameObject.Find("Canvas/TowersText").GetComponent<Text>().text = "Towers : " + counter + " out of 6";
	}

	// Update is called once per frame
	void Update () 
	{		
		timespan-=Time.deltaTime;
		if(timespan<=-1)
		{
			PlayMusic();
		}
        if(Input.GetKeyDown("h"))
        {
            Debug.Log("Cleaning memory");
            for(int i = 1 ; i <= 6 ; i++)
            {
                PlayerPrefs.SetInt("Tower" + i, 0);
            }
			// Stopping music
			//string name = PlayerPrefs.GetString("CharacterName");
			//if(audioSource!=null)
			//	audioSource.Stop();
			/*GameObject soundObject = GameObject.Find("Sounds/"+name);
			soundObject.SetActive(false);
			soundObject.SetActive(true);*/
			//PaintTowers();
			SceneManager.LoadScene("scene1", LoadSceneMode.Single);
		}

		if(Input.GetKeyDown("g"))
		{
			PlayerPrefs.SetInt("Tower1", 1);
			PlayerPrefs.SetInt("Tower2", 1);
			PlayerPrefs.SetInt("Tower3", 1);
			PlayerPrefs.SetInt("Tower4", 1);
			PlayerPrefs.SetInt("Tower5", 0);
			PlayerPrefs.SetInt("Tower6", 1);
			SceneManager.LoadScene("scene1", LoadSceneMode.Single);
		}

		// Movement controls
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 20.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

		// Space handler
		if(Input.GetKeyDown("space"))
		{
			// Space Bar Handler
			// Foreach tower
			GameObject[] radioTowers = GameObject.FindGameObjectsWithTag("RadioTower");
			foreach (GameObject radioTower in radioTowers)
			{
				// Calculating distance from tower
				Vector3 characterPos = GetComponent<Transform>().position;
				int towerId = int.Parse(radioTower.name.Substring(5));
				Vector3 radioTowerPos = GameObject.Find("TowerPos/" + towerId).transform.position;
				float distance = Vector3.Distance(characterPos, radioTowerPos);
				
				// Check if in interaction Distance
				if(distance < minDistanceFromTower)
				{
					print("Checking if tower is taken");
					bool isTowerTaken = (PlayerPrefs.GetInt("Tower" + towerId) == 1);

					// If the tower is not taken then Open minigame
					if(isTowerTaken)
					{
						Debug.Log("Tower is already taken");
						// Tell user tower is already taken
					}
					else
					{
						print("Entering tower " + towerId + " from a distance of " + distance);

						PlayerPrefs.SetInt("CurrentTower", towerId);

						SceneManager.LoadScene("file", LoadSceneMode.Single);
					}
				}
				else
				{
					Debug.Log("Too far away");
				}
			}
		}
	}

	void OnDestroy() {
		PlayerPrefs.SetString("LastOperationStatus", "none");
    }
	void PlayMusic()
	{
		for(int i=1;i<=6;i++)
		{
			if(PlayerPrefs.GetInt("Tower"+i.ToString())==1)
			{
				GameObject o = GameObject.Find("TowerPos/"+i.ToString());
				Vector3 location = new Vector3(0,0,0);
				location.x = o.transform.position.x;
				location.z = o.transform.position.z;
				string name = PlayerPrefs.GetString("CharacterName");
				AudioSource audio = GameObject.Find("Sounds/"+name).GetComponent<AudioSource>();
				audio.volume = audio.volume;
				audio.loop = true;
				audio.maxDistance = 2.0f;
				AudioClip ac =audio.clip;
				audioSource  = audio;
				SoundTimeSpan = ac.length;
				timespan = SoundTimeSpan;
				AudioSource.PlayClipAtPoint(ac,location);
				
			}
		}
	}
}

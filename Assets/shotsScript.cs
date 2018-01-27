using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shotsScript : MonoBehaviour {
    
	// Use this for initialization
    bool shouldKill = false;
    float timeToKill = 10.0f;

	void Start () {
		this.shouldKill = GameObject.Find("player").GetComponent<playerScript>().shouldDie;
	}

	// Update is called once per frame
	void Update () {
        Debug.Log(gameObject.name);

        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        gameObject.transform.position = new Vector3(x, y, 0);

        //Debug.Log(timeToKill + ":" + shouldKill);

        if(shouldKill && timeToKill <= 0f)
        {
            // Killing the scene
            SceneManager.LoadScene("scene1", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("file");
        }
        if(shouldKill)
        {
            timeToKill -= Time.deltaTime;
            //GameObject.Destroy(gameObject);
        }
	}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="player")
        {
            GameObject.Find("Canvas/instructionText").GetComponent<Text>().text = "you lost";
            GameObject.Find("Canvas/instructionText").GetComponent<Text>().color = Color.red;

            // Starting scene killing process
            GameObject.Find("player").GetComponent<playerScript>().shouldDie = true;
        }
        GameObject.Destroy(gameObject);
    }
}

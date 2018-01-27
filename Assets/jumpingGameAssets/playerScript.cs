using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerScript : MonoBehaviour {
    float timespan = 0.02f;
    bool available = true;
    GameObject tower = null;
    public bool shouldDie = false;
    private bool moved;
    private float timeToKill = 2.0f;
    private Quaternion rotation;
    private Vector3 PrevLocation;
	// Use this for initialization
	void Start () {
      rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,0);
        moved = false;
        /*if(Input.GetKeyUp("h"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(0, 0, 10);
        }*/
        transform.rotation = this.rotation;
        if(shouldDie && timeToKill <= 0f)
        {
            // Killing the scene
            SceneManager.LoadScene("scene1", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("file");
        }
        if(shouldDie)
        {
            timeToKill -= Time.deltaTime;
        }

        if (Input.GetKeyDown("space")||Input.GetKeyDown("w"))
        {
            if(available)
            {
                MoveUp(40);
                moved = true;
            }
        }
        if(Input.GetKey("a"))//move left
        {
            timespan -= Time.deltaTime;
            if (timespan<= 0)
            {
                Move(-1);
                timespan = 0.02f;
                moved = true;
            }
        }//move left
        if(Input.GetKey("d"))//move right
        {
            timespan -= Time.deltaTime;
            if (timespan <= 0)
            {
                Move();
                timespan = 0.02f;
                moved = true;
            }
        }//move right
        if(Input.GetKeyDown("f") && tower != null)
        {
            if(!shouldDie)
            {
                if (tower.GetComponent<towerScript>() == null)
                    return;
                tower.GetComponent<towerScript>().usable = false;
                //AudioClip ac = GameObject.Find("patrick").GetComponent<AudioSource>().clip;
                //AudioSource.PlayClipAtPoint(ac, tower.transform.position);
                /*AudioClip ac = GameObject.Find("patrick").GetComponent<AudioSource>().GetComponent<AudioClip>();
                AudioSource.PlayClipAtPoint(ac, tower.transform.position);*/
                tower = null;
                GameObject.Find("Canvas/instructionText").GetComponent<Text>().text = "";
                
                // Win case
                int TowerId = PlayerPrefs.GetInt("CurrentTower");
                PlayerPrefs.SetInt("Tower" + TowerId, 1);

                bool isFinished = true;
                for(int i = 1 ; i <= 6 ; i++)
                {
                    bool state = (PlayerPrefs.GetInt("Tower" + i) == 1);
                    if(! state)
                    {
                        isFinished = false;
                        break;
                    }
                }

                if(isFinished)
                {
                    // Start Credits and game over scene
                    PlayerPrefs.SetInt("DidWin", 1);
                    SceneManager.LoadScene("CreditsScene");
                }
                else
                {
                    SceneManager.LoadScene("scene1", LoadSceneMode.Single);
                    SceneManager.UnloadSceneAsync("file");
                }
            }
        }
        // Prevent moving when player did not intended
        if(!moved)
        {
            transform.position = new Vector3(PrevLocation.x,transform.position.y,PrevLocation.z);
        }
	}
    private void MoveUp(int Force=1)
    {
        GameObject obj = gameObject;
        obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0)*Force);
        available = false;
        PrevLocation = gameObject.transform.position;
    }
    private void Move(int Force=1)
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        gameObject.transform.position = new Vector3(x + 0.2f* Force, y, z);
        PrevLocation = gameObject.transform.position;
    }
    public void OnCollisionEnter(Collision collision)
    {
        available = true;
        if (collision.gameObject.name == "top")
        {
           if(collision.gameObject.GetComponent<towerScript>().usable && collision.gameObject.transform.position.y < gameObject.transform.position.y-0.1f)
            {
                GameObject.Find("Canvas/instructionText").GetComponent<Text>().text = "Press F To Play Song";
                tower = collision.gameObject;
            }
        }
    }
}

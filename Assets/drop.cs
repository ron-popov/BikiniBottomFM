using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class drop : MonoBehaviour {
    float timespan = TIME_CONST;
    const float TIME_CONST = 1.0f;
    public bool shooting = true;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        if(GameObject.Find("player").GetComponent<playerScript>().shouldDie)
        {
            return;
        }
        timespan -= Time.deltaTime;
        if(timespan<=0)
        {
            timespan = TIME_CONST;
            Vector3 v = gameObject.transform.position;
            float yDif = gameObject.GetComponent<Collider>().bounds.size.y - 0.1f;
            float width = gameObject.GetComponent<Collider>().bounds.size.x;
            System.Random rand = new System.Random();
            float span = rand.Next((int)(width * -500),(int)(width * 500)) / 1000;
            Instantiate(Resources.Load("burger"), new Vector3(v.x+span,v.y-yDif,v.z), Quaternion.identity);
        }
	}
    
   
}













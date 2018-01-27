using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {
    
    Quaternion rotation;
	// Use this for initialization
	void Start () {
        rotation = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.rotation = rotation;
        //transform.localPosition = new Vector3(0, 0, -10);
	}
}

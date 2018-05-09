using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    public GameObject mainCam;



	// Use this for initialization
	void Start () {

		mainCam = GameObject.Find("Main Cam");

	}
	
	// Update is called once per frame
	void Update () {
		
        transform.LookAt(mainCam.transform);

    }
}

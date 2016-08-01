using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    public Camera mainCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(mainCam.transform);

    }
}

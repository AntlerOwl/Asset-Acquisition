using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public static GameObject[] guardList;

    //Player Recorded by the camera
    public static bool playerHasBeenRecordedOnCamera;
    public static bool recordingsHaveBeenDestroyed;

	// Use this for initialization
	void Start () {

        guardList = GameObject.FindGameObjectsWithTag("Guard");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

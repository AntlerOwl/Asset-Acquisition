using UnityEngine;
using System.Collections;

public class GuardMonitorCheckScript : MonoBehaviour {

    public static bool guardIsWatchingMonitors;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SecRoomGuard"))
        {
            guardIsWatchingMonitors = true;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SecRoomGuard"))
        {
            guardIsWatchingMonitors = false;
        }
    }
}

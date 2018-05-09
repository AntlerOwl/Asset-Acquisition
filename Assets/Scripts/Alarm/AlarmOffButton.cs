using UnityEngine;
using System.Collections;

public class AlarmOffButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {

        if (Alarm.globalAlarm != false)
        {
            Alarm.globalAlarm = false;
        }

    }
}

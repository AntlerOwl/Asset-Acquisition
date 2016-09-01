using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour {

    public static bool globalAlarm;
    public static int alarmTriggersTotal;

	// Use this for initialization
	void Start () {

        globalAlarm = false;
        alarmTriggersTotal = 0;

	}
	
	// Update is called once per frame
	void Update () {
        

	}
}

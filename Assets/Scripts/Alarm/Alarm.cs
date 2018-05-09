using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Alarm : MonoBehaviour {

    public static bool globalAlarm;
    public static int alarmTriggersTotal;

    public Text alarmsTotalDisplay;

	// Use this for initialization
	void Start () {

        globalAlarm = false;
        alarmTriggersTotal = 0;

	}
	
	// Update is called once per frame
	void Update () {

        alarmsTotalDisplay.text = alarmTriggersTotal.ToString();

        //print(alarmTriggersTotal);
	}
}

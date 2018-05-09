using UnityEngine;
using System.Collections;

public class AlarmOnButton : MonoBehaviour {

    public GameObject connectAlarm;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnMouseUp()
    {

        if(Alarm.globalAlarm != true)
        {
           // Alarm.globalAlarm = true;
           // Alarm.alarmTriggersTotal++;
            connectAlarm.GetComponent<LowPolyLaser>().CallGuardForAlarm();
        }

    }
}

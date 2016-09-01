using UnityEngine;
using System.Collections;

public class AlarmPanel : MonoBehaviour {

    public GameObject panelScreen;

    public Material mat1;
    public Material mat2;

    private Renderer rend;

	// Use this for initialization
	void Start () {

        //mat = panelScreen.GetComponent<Renderer>().material;
        rend = panelScreen.GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {

        if(Alarm.globalAlarm == true)
        {
            //Blink between colours



            rend.material = mat1;
        }
        else
        {
            rend.material = mat2;
        }
	
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetButtonDown("e") && Alarm.globalAlarm == true)
        {
            Alarm.globalAlarm = false;
        }
    }
}

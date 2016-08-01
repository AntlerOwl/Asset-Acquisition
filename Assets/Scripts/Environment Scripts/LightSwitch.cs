using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

    public Light lightObject;

	// Use this for initialization
	void Start () {

        //lightObject = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("e"))
        {

        }
	}

    void OnTriggerStay(Collider other)
    {
        if(Input.GetButtonDown("e") && other.tag == "Player")
        {
            lightObject.enabled = !lightObject.enabled;
        }
    }
}

using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

    public GameObject doorObject;
    public bool doorOpen;
    

    private float timeBetweenDoorInteract = 20;

    float doorOpenTime;
    

	// Use this for initialization
	void Start () {

        doorOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if(Input.GetButtonDown("e") && doorOpen == false && Time.time > doorOpenTime)
        {
            doorOpenTime = Time.time + timeBetweenDoorInteract / 1000;
            doorObject.GetComponent<Animation>().Play("DoorSlideLeft");
            doorOpen = !doorOpen;
        }
        if (Input.GetKeyDown("e") && doorOpen == true && Time.time > doorOpenTime)
        {
            doorOpenTime = Time.time + timeBetweenDoorInteract / 1000;
            doorObject.GetComponent<Animation>().Play("DoorSlideLeftClose");
            doorOpen = !doorOpen;
        }
    }


}



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
        if(Input.GetButtonDown("e") && doorOpen == false && Time.time > doorOpenTime && other.gameObject.tag == "Player")
        {
            doorOpenTime = Time.time + timeBetweenDoorInteract / 1000;
            doorObject.GetComponent<Animation>().Play("DoorSlideLeft");
            doorOpen = true;
        }
        if (Input.GetKeyDown("e") && doorOpen == true && Time.time > doorOpenTime)
        {
            doorOpenTime = Time.time + timeBetweenDoorInteract / 1000;
            doorObject.GetComponent<Animation>().Play("DoorSlideLeftClose");
            doorOpen = false;
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Guard" )
        {
            doorObject.GetComponent<Animation>().Play("DoorSlideLeft");
            doorOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Guard" && doorOpen == true)
        {
            doorObject.GetComponent<Animation>().Play("DoorSlideLeftClose");
            doorOpen = false;
        }
    }




}



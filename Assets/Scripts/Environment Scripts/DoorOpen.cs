using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

    public GameObject doorObject;
    public bool doorOpen;
    public bool locked;

    private bool playerInTrigger;
    private float timeBetweenDoorInteract = 0.5f;

    float doorOpenTime;
    

	// Use this for initialization
	void Start () {

        doorOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        

        if(playerInTrigger == true)
        {
            if (Input.GetKeyDown("e") && doorOpen == false && Time.time > doorOpenTime && locked == false)
            {
                doorOpenTime = Time.time + timeBetweenDoorInteract;
                doorObject.GetComponent<Animation>().Play("DoorSlideLeft");
                doorOpen = true;
            }
            if (Input.GetKeyDown("e") && doorOpen == true && Time.time > doorOpenTime && locked == false)
            {
                doorOpenTime = Time.time + timeBetweenDoorInteract;
                doorObject.GetComponent<Animation>().Play("DoorSlideLeftClose");
                doorOpen = false;
            }

            if (Input.GetKeyDown("e") && locked == true)
            {
                //Spill av lyd, og gjerne endre et grønt lys eller noe i den duren
            }
        }
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Guard"  && locked != true)
        {
            doorObject.GetComponent<Animation>().Play("DoorSlideLeft");
            doorOpen = true;
        }

        if(other.gameObject.tag == "Player")
        {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Guard" && doorOpen == true && locked != true)
        {
            doorObject.GetComponent<Animation>().Play("DoorSlideLeftClose");
            doorOpen = false;
        }

        if(other.gameObject.tag == "Player")
        {
            playerInTrigger = false;
        }
    }




}



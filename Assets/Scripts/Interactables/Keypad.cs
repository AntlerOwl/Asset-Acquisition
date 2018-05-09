using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {
    

    public GameObject objectToActivate;
    public GameObject objectToDeActivate;
    public GameObject door;


    public string correctCode;
    public string playerCode = "";
    private bool solved;
    private bool playerInTrigger;

   
    public GameObject panelUI;
    public Text currentCodeText;

	// Use this for initialization
	void Start () {

        playerInTrigger = false;
        solved = false;
	}
	
	// Update is called once per frame
	
    void Update()
    {
        currentCodeText.text = playerCode;

        if(playerCode == correctCode && solved != true)
        {
            solved = true;

            if(objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            if (objectToDeActivate != null)
            {
                objectToDeActivate.SetActive(false);
            }

            if(door.GetComponent<DoorOpen>().locked == true)
            {
                door.GetComponent<DoorOpen>().locked = false;
            }
        }

        if(playerInTrigger == true && Input.GetKeyDown("e") && solved == !true)
        {
            if (panelUI.activeInHierarchy == false)
            {
                panelUI.SetActive(true);
                Player.busyInteracting = true;
                print("Test 1");

            }
            else
            {
                panelUI.SetActive(false);
                Player.busyInteracting = false;
                print("test2");
            }
                          
        }

        
    }
   

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Player") )
        {
            playerInTrigger = true;                                   
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == ("Player"))
        {
            panelUI.SetActive(false);
            playerInTrigger = false;
            Player.busyInteracting = false;
            playerCode = "";
        }
    }
}

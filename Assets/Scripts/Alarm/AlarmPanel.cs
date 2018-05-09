using UnityEngine;
using System.Collections;

public class AlarmPanel : MonoBehaviour {

    public GameObject panelScreen;
    private bool playerInTrigger;


    public GameObject objectToActivate;
    public GameObject objectToDeActivate;

    public GameObject LargePanel;

    public Material mat1;
    public Material mat2;

    private Renderer rend;


	// Use this for initialization
	void Start () {

        //mat = panelScreen.GetComponent<Renderer>().material;
        rend = panelScreen.GetComponent<Renderer>();

        playerInTrigger = false;

	}
	
	// Update is called once per frame
	void Update () {

        if(Alarm.globalAlarm == true)
        {           
            rend.material = mat1;
        }
        else
        {
            rend.material = mat2;
        }

        if (Input.GetKeyDown("e") && playerInTrigger == true)
        {
            if(LargePanel.activeInHierarchy == false)
            {
                LargePanel.SetActive(true);
                Player.busyInteracting = true;
            }
            else
            {
                LargePanel.SetActive(false);
                Player.busyInteracting = false;
            }
            

            if(objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            if (objectToDeActivate != null)
            {
                objectToDeActivate.SetActive(false);
            }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInTrigger = true;
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInTrigger = false;
            LargePanel.SetActive(false);
            Player.busyInteracting = false;
        }
    }
}

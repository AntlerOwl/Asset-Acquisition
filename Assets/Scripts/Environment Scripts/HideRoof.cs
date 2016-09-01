using UnityEngine;
using System.Collections;

public class HideRoof : MonoBehaviour {

    public GameObject toHide;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            toHide.SetActive(false);
        }
                
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            toHide.SetActive(true);
        }
    }
}

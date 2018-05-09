using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyCodes : MonoBehaviour {


    //public static string correctCode = "2469";
    //public static string playerCode = "";

    public GameObject parentKeyPad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnMouseUp()
    {
        if(parentKeyPad.GetComponent<Keypad>().playerCode.Length < 4)
        {
            parentKeyPad.GetComponent<Keypad>().playerCode += gameObject.name;
        }

        //playerCode += gameObject.name;

        if (gameObject.tag == "resetbutton")
        {
            parentKeyPad.GetComponent<Keypad>().playerCode = "";
        }
    }

    

}

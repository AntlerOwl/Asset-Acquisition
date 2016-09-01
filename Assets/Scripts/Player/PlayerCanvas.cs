using UnityEngine;
using System.Collections;

public class PlayerCanvas : MonoBehaviour {

    public Camera cam;
    public GameObject statsScreen;
    public Transform targetTransform;

	// Use this for initialization
	void Start () {

       
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown("tab"))
        {
            statsScreen.SetActive(true);

        }
        if(Input.GetKeyUp("tab"))
        {
            statsScreen.SetActive(false);

        }

        gameObject.transform.position = targetTransform.transform.position;

    }

    void FixedUpdate()
    {
        transform.LookAt(cam.transform);
    }
}

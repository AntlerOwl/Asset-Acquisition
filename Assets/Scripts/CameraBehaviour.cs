using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public Transform cameraTarget;
    private Vector3 offset;

    public float speed;
    

	// Use this for initialization
	void Start () {

        offset = transform.position - cameraTarget.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

        
        var target = cameraTarget.transform.position + offset;
        var moveStuff= target - gameObject.transform.position;

        transform.Translate((moveStuff) * speed * Time.deltaTime);
	}
}

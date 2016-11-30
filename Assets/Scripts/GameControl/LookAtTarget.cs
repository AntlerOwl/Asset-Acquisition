using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {


    public Transform lookTarget;


	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.LookAt(lookTarget);
	}
}

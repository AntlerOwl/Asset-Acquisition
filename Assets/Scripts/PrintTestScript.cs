using UnityEngine;
using System.Collections;

public class PrintTestScript : MonoBehaviour {

    public Guard grd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        print(grd.patFire);
	
	}
}

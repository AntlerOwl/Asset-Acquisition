using UnityEngine;
using System.Collections;

public class AlarmLights : MonoBehaviour {

    public float spinSpeed;
    public GameObject bulb1;
    public GameObject bulb2;
    

    private Material mat;

	// Use this for initialization
	void Start () {
        
        
        mat = bulb1.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        

        if (Alarm.globalAlarm == true)
        {
            gameObject.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
            mat.color = Color.red;
        }
        else
        {
            mat.color = Color.grey;
        }
	
	}
}

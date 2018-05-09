using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraZone : MonoBehaviour {

    //info
    public string cameraName;
	public int cameraZoneState;

    private NavMeshAgent agent;


	// Use this for initialization
	void Start () {

		cameraZoneState = 1;
        agent = gameObject.GetComponent<NavMeshAgent>();
		
	}   
	
	// Update is called once per frame
	void Update () {

		switch (cameraZoneState)
		{
			case 1:

                agent.speed = agent.speed * 1;

				break;

			case 2:

                agent.speed = agent.speed * 1.5f;

				break; 
		}
		
	}

	void Moving()
	{
		print("Kameraet begynner å bevege seg");
	}

    void StopMoving()
    {
        print("Kameraet stopper å bevege seg");
    }
}

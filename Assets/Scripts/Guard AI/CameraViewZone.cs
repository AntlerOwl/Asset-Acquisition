using UnityEngine;
using System.Collections;

public class CameraViewZone : MonoBehaviour {

    public bool callForGuard;
    public GameObject guardCallTargetPoint;

    public GameObject playerObject;

    public static bool playerIsInCamerasView;

	// Use this for initialization
	void Start () {

        callForGuard = false;
        playerIsInCamerasView = false;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            guardCallTargetPoint.transform.position = other.transform.position;
        }

    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            guardCallTargetPoint.transform.position = other.transform.position;
            playerIsInCamerasView = true;

            //Vurder her å putte en sjekk for at global alarm må være av
            if (other.gameObject.tag == "Player"  && GuardMonitorCheckScript.guardIsWatchingMonitors == true)
            {


               
                    callForGuard = true;
                    float[] distancesToGuards = new float[GameControl.guardList.Length];
                    NavMeshPath path = new NavMeshPath();

                    for (int i = 0; i < GameControl.guardList.Length; i++)
                    {
                        //Check to not interfere with GuardPatrol
                        GameControl.guardList[i].GetComponent<GuardPatrol>().beingSearchedFor = true;
                        //Makes sure a guard doesn't wait out it's current nodestop time if it is in waiting on its original patrol
                        GameControl.guardList[i].GetComponent<GuardPatrol>().nodeStopTime = 0.0f;

                        //Calculate Path
                        GameControl.guardList[i].GetComponent<NavMeshAgent>().CalculatePath(guardCallTargetPoint.transform.position, path);

                        //Set Path
                        GameControl.guardList[i].GetComponent<NavMeshAgent>().SetPath(path);
                        GameControl.guardList[i].GetComponent<NavMeshAgent>().Stop();

                        //Remaining Distance to the Path set
                        distancesToGuards[i] = GameControl.guardList[i].GetComponent<NavMeshAgent>().remainingDistance;

                        int indexOfGuardNearestPoint = 0;
                        for (int a = 1; a < distancesToGuards.Length; a++)
                        {
                            if (distancesToGuards[a] < distancesToGuards[indexOfGuardNearestPoint])
                            {
                                indexOfGuardNearestPoint = a;
                            }
                        }


                        for (int b = 0; b < GameControl.guardList.Length; b++)
                        {
                            GameControl.guardList[b].GetComponent<Guard>().guardState = 1;
                            GameControl.guardList[b].GetComponent<Guard>().patFire = 0;
                            GameControl.guardList[b].GetComponent<GuardPatrol>().beingSearchedFor = false;
                        }

                        GameControl.guardList[indexOfGuardNearestPoint].GetComponent<Guard>().guardState = 4;
                        GameControl.guardList[indexOfGuardNearestPoint].GetComponent<Guard>().alarmCalledBy = guardCallTargetPoint;

                        GameControl.guardList[i].GetComponent<NavMeshAgent>().Resume();
                       

                }

                Alarm.globalAlarm = true;
                Alarm.alarmTriggersTotal++;

            }
            if(GuardMonitorCheckScript.guardIsWatchingMonitors == false && GameControl.recordingsHaveBeenDestroyed != true)
            {
                GameControl.playerHasBeenRecordedOnCamera = true;
            }
        }          
       
    }

    void OnTriggerExit(Collider other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            playerIsInCamerasView = false;

            guardCallTargetPoint.transform.position = other.transform.position;
            playerIsInCamerasView = true;

            //Vurder her å putte en sjekk for at global alarm må være av
            if (other.gameObject.tag == "Player" && GuardMonitorCheckScript.guardIsWatchingMonitors == true)
            {



                callForGuard = true;
                float[] distancesToGuards = new float[GameControl.guardList.Length];
                NavMeshPath path = new NavMeshPath();

                for (int i = 0; i < GameControl.guardList.Length; i++)
                {
                    //Check to not interfere with GuardPatrol
                    GameControl.guardList[i].GetComponent<GuardPatrol>().beingSearchedFor = true;
                    //Makes sure a guard doesn't wait out it's current nodestop time if it is in waiting on its original patrol
                    GameControl.guardList[i].GetComponent<GuardPatrol>().nodeStopTime = 0.0f;

                    //Calculate Path
                    GameControl.guardList[i].GetComponent<NavMeshAgent>().CalculatePath(guardCallTargetPoint.transform.position, path);

                    //Set Path
                    GameControl.guardList[i].GetComponent<NavMeshAgent>().SetPath(path);
                    GameControl.guardList[i].GetComponent<NavMeshAgent>().Stop();

                    //Remaining Distance to the Path set
                    distancesToGuards[i] = GameControl.guardList[i].GetComponent<NavMeshAgent>().remainingDistance;

                    int indexOfGuardNearestPoint = 0;
                    for (int a = 1; a < distancesToGuards.Length; a++)
                    {
                        if (distancesToGuards[a] < distancesToGuards[indexOfGuardNearestPoint])
                        {
                            indexOfGuardNearestPoint = a;
                        }
                    }


                    for (int b = 0; b < GameControl.guardList.Length; b++)
                    {
                        GameControl.guardList[b].GetComponent<Guard>().guardState = 1;
                        GameControl.guardList[b].GetComponent<Guard>().patFire = 0;
                        GameControl.guardList[b].GetComponent<GuardPatrol>().beingSearchedFor = false;
                    }

                    GameControl.guardList[indexOfGuardNearestPoint].GetComponent<Guard>().guardState = 4;

                    GameControl.guardList[i].GetComponent<NavMeshAgent>().Resume();


                }

                Alarm.globalAlarm = true;
                Alarm.alarmTriggersTotal++;

            }

            if(GuardMonitorCheckScript.guardIsWatchingMonitors == false && GameControl.recordingsHaveBeenDestroyed != true)
            {
                GameControl.playerHasBeenRecordedOnCamera = true;
            }
        }
    }
}

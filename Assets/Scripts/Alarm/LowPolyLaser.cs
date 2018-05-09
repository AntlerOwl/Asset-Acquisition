using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class LowPolyLaser : MonoBehaviour
{
    GameObject beamHitParticles;
    ParticleSystem bhp;
    public float beamRotationSpeed = 400.0f;
    public float beamExtendSpeed = 10.0f;
    public float zScaleFactor = 20.0f;
    public float distanceToHitPoint;
    
    public GameObject guardCallTargetPoint;   

    private GameControl ctrl;

    void Awake()
    {
        // Make sure the particles system is the first child of THIS extendy beam cube.
        // And that the particle system does NOT Play on Awake!
        beamHitParticles = transform.parent.GetChild(1).gameObject; // So it doesn't scale the particles, made it a sibling.
        bhp = beamHitParticles.GetComponent<ParticleSystem>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            distanceToHitPoint = Vector3.Distance(transform.position, hit.point);

            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, (distanceToHitPoint * zScaleFactor)), (beamExtendSpeed * Time.deltaTime)); // Because distance-units != scale-units.

            beamHitParticles.transform.position = hit.point;
            //beamHitParticles.transform.position = Vector3.Lerp(beamHitParticles.transform.position, hit.point, 20*Time.deltaTime); 
            // ^ Ends up being weird because it has to travel to the hit point everytime since in the else I let it stay where it was.
            // If I could keep it on the end of the beam cube, extended or unextended that would work.
        }
        else
        {
            bhp.Stop(); // Oops we hit nothing, scale the laser back and stop the particles!
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, transform.localScale.z), (beamExtendSpeed * Time.deltaTime));
            // This is screwy but its a nice effect. It messed with the collider though I think... Seems to work, will keep it.
            //transform.localScale = Vector3.one; // Simply just makes it disappear essentially.
        }

        transform.Rotate(0, 0, (Time.deltaTime * beamRotationSpeed));
    }

    void OnTriggerEnter(Collider other)
    {
        bhp.Play();
        
        if (other.gameObject.tag == "Player") // && Alarm.globalAlarm != true
        {

            CallGuardForAlarm();
           

               /* float[] distancesToGuards = new float[GameControl.guardList.Length];
                NavMeshPath path = new NavMeshPath();

                for (int i = 0; i < GameControl.guardList.Length; i++)
                {
                    //Check to not interfere with GuardPatrol
                    GameControl.guardList[i].GetComponent<GuardPatrolv2>().beingSearchedFor = true;
                    //Makes sure a guard doesn't wait out it's current nodestop time if it is in waiting on its original patrol
                    GameControl.guardList[i].GetComponent<GuardPatrolv2>().waitTimer = 0.0f;

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
                        if(distancesToGuards[a] < distancesToGuards[indexOfGuardNearestPoint])
                        {
                            indexOfGuardNearestPoint = a;
                        }
                    }

            
                    for (int b = 0; b < GameControl.guardList.Length; b++)
                    {
                        GameControl.guardList[b].GetComponent<Guard>().guardState = 1;
                        GameControl.guardList[b].GetComponent<Guard>().patFire = 0;
                        GameControl.guardList[b].GetComponent<GuardPatrolv2>().beingSearchedFor = false;
                    }
                   
                    GameControl.guardList[indexOfGuardNearestPoint].GetComponent<Guard>().guardState = 4;
                    GameControl.guardList[indexOfGuardNearestPoint].GetComponent<Guard>().alarmCalledBy = guardCallTargetPoint;

                GameControl.guardList[i].GetComponent<NavMeshAgent>().Resume();
                    
               
                
            }

            Alarm.globalAlarm = true;
            Alarm.alarmTriggersTotal++;
            */

        }
    }

    public void CallGuardForAlarm()
    {
        float[] distancesToGuards = new float[GameControl.guardList.Length];
        NavMeshPath path = new NavMeshPath();

        for (int i = 0; i < GameControl.guardList.Length; i++)
        {
            //Check to not interfere with GuardPatrol
            GameControl.guardList[i].GetComponent<GuardPatrolv2>().beingSearchedFor = true;
            //Makes sure a guard doesn't wait out it's current nodestop time if it is in waiting on its original patrol
            GameControl.guardList[i].GetComponent<GuardPatrolv2>().waitTimer = 0.0f;

            //Calculate Path
            GameControl.guardList[i].GetComponent<NavMeshAgent>().CalculatePath(guardCallTargetPoint.transform.position, path);

            //Set Path
            GameControl.guardList[i].GetComponent<NavMeshAgent>().SetPath(path);
            GameControl.guardList[i].GetComponent<NavMeshAgent>().isStopped = true;

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
                GameControl.guardList[b].GetComponent<GuardPatrolv2>().beingSearchedFor = false;
            }

            GameControl.guardList[indexOfGuardNearestPoint].GetComponent<Guard>().guardState = 4;
            GameControl.guardList[indexOfGuardNearestPoint].GetComponent<Guard>().alarmCalledBy = guardCallTargetPoint;

            GameControl.guardList[i].GetComponent<NavMeshAgent>().isStopped = false;



        }

        Alarm.globalAlarm = true;
        Alarm.alarmTriggersTotal++;
    }
}

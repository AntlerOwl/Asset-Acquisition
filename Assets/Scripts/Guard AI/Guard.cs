using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof (NavMeshAgent))]
public class Guard : MonoBehaviour {

    public static bool caughtPlayer;

    private NavMeshAgent agent;
    

    private GameObject playerObject;
    
    public Transform jailTarget;
    public Transform itemSpawnTarget;
    private GameObject gobletPrefab;
    private GameObject platePrefab;
    public float waitBeforePatrolResumes;
    private GameObject gC;
    public int gobletsTotal;
    private Quaternion randomItemRotation;

    private Transform target;

    private FieldOfView fov;
    private GuardPatrol gpat;
    private GuardPatrolv2 gpat2;
    public int guardState;

    private IEnumerator fovEnumerator;
    private float targetFovRadius;
    private bool scaleFov;

    public int patFire;
    public int moveToTargetAreaFire;

    public Transform currentHidingSpot;
    public Transform[] nyListeMedGjemmesteder = new Transform[3];
    public GameObject alarmCalledBy;
    
    public int hidingSpotsChecked = 0;
    public float hidingSpotWaitTime;
    private Transform lookToSpot;
    private bool searchingCheck;
    private bool patrollingHidingSpots;

    //Look around variables//
    public float rotationSpeed;
    private bool startSecondLook;

    private bool hasLookedAround = false;
    private Quaternion lookRotation;
    private Vector3 direction;
    private string lookSpotType;

    //Sound source variables
    public GameObject soundSourceNode;
    public bool soundTargetSet;
    public bool hasWaitedBeforeMovingToSoundSource;
    private Quaternion randomRotation;

    public float waitingAfterHavingHeardSound;
    private bool startSecondSoundLook;

    private Quaternion lookRotationSound;
    private Vector3 directionSound;
    public float rotationSpeedSound;

    public bool currentlySearchingSound;

    Player playerScript;



	// Use this for initialization
	void Start () {

        //waitBeforePatrolResumes = 1.0f;

        agent = gameObject.GetComponent<NavMeshAgent>();

        fov = gameObject.GetComponent<FieldOfView>();
        gpat = gameObject.GetComponent<GuardPatrol>();
        gpat2 = gameObject.GetComponent<GuardPatrolv2>();

        playerObject = GameObject.FindGameObjectWithTag("Player");

        gC = GameObject.FindGameObjectWithTag("GameController");

        caughtPlayer = false;

        guardState = 1;

        patFire = 0;

        fovEnumerator = fovLerpChange(1.0f);

        alarmCalledBy = null;

        hidingSpotWaitTime = 1.75f;

        startSecondLook = false;

        soundTargetSet = false;

        platePrefab = GameObject.Find("GoldenPlateSafety");

        gobletPrefab = GameObject.Find("GoldenGobletSafety");

        

}
	
	// Update is called once per frame
	void Update () {

        //Testing av variabler


        //Switch system for guard states
        switch (guardState)
        {
            //Patrolling state
            case 1:

                if(patFire < 1)
                {                   
                    Patrolling();
                    patFire = 1;
                    gpat2.waitTimer = 0.0f;

                    //Reset for searching alarm spots
                    hidingSpotsChecked = 0;
                }
                
                break;

            //Standing still and doing absolutely fucking nothing
            case 2:

                agent.Stop();
                agent.ResetPath();
                break;

            //Chasing the Player
            case 3:
                ChasingPlayer();
                break;

            //Moving to target area 
            case 4:
                if(moveToTargetAreaFire < 1)
                {
                    MovingToTargetArea();
                    moveToTargetAreaFire = 1;
                }               
                break;

            //Searching target area
            case 5:
                if(searchingCheck != true)
                {
                    SearchingTheArea();
                }
                break;
            
            //Move to where the target was spotted
            case 6:
                MovingToCameraSpotArea();
                break;

            //Placeholder for now
            case 7:

                if(soundTargetSet == false)
                {
                    agent.Resume();
                    gpat2.waitTimer = 0.0f;
                    SetSoundSourceTargetpoint();
                }                  
                
                break;

            //Default state
            default:
                break;
        }


        
        //Sets guard speed and the length of their viewcone when the alarm goes off
        if(guardState == 1)
        {
            if(Alarm.globalAlarm == true)
            {
                agent.speed = 4.15f;

                targetFovRadius = 12.0f;

                if(scaleFov == false)
                {
                    fovEnumerator = fovLerpChange(2.0f);
                    StartCoroutine(fovEnumerator);
                }
                
                
            }
            else
            {
                agent.speed = 3.5f;

                targetFovRadius = 9.5f;


                if (scaleFov == false)
                {
                    fovEnumerator = fovLerpChange(2.0f);
                    StartCoroutine(fovEnumerator);
                }
            }

        }
        
        //Speed and radius of the guards when in state 4 or 5
        if(guardState == 4)
        {
            agent.speed = 4.25f;
            targetFovRadius = 12.0f;
        }

        if (guardState == 5)
        {
            agent.speed = 3.75f;
            targetFovRadius = 12.5f;
        }

        //Sets the guards state to 5 when they arrive at the target location after an alarm
        if (agent.remainingDistance < 0.125f && guardState == 4)
            {
                guardState = 5;
                
            }

        //Sets to guard to move to their next patrol point!
        if (guardState == 5 && agent.remainingDistance < 0.125)
        {
            
            if(hidingSpotsChecked < 3)
            {
                
                

                if(hidingSpotWaitTime <= 0)
                {
                    agent.Resume();
                    MoveToNextHidingSpot2();
                }
                else
                {
                    if (lookToSpot != null)
                    {
                        if(lookSpotType == "single")
                        {
                            LookAroundSingle();
                        }
                        if (lookSpotType == "double")
                        {

                            if(startSecondLook == false)
                            {
                                LookAroundDouble();
                            }
                            else
                            {
                                LookAroundDouble2();
                            }
                            
                        }
                        
                    }
                    agent.Stop();
                    hidingSpotWaitTime = hidingSpotWaitTime - Time.deltaTime;
                }
                
            }
            else
            {                
                if(hidingSpotWaitTime <= 0)
                {
                    agent.Resume();
                    guardState = 1;
                    searchingCheck = false;
                }
                else
                {
                    if(lookToSpot != null)
                    {
                        if(lookSpotType == "single")
                        {
                            LookAroundSingle();
                        }

                        if (lookSpotType == "double" && startSecondLook == false)
                        {
                            LookAroundDouble();
                        }

                        if (startSecondLook == true)
                        {
                            LookAroundDouble2();
                        }

                    }

                    agent.Stop();
                    hidingSpotWaitTime = hidingSpotWaitTime - Time.deltaTime;
                }
            }
       
        }

        //Controls guard behaviour when they're moving to target area, after the player has been spotted by the camera
        if(guardState == 6)
        {
            if(CameraViewZone.playerIsInCamerasView == true)
            {
                agent.SetDestination(playerObject.transform.position);
            }

            if (agent.remainingDistance < 0.125f)
            {
                
                agent.Stop();
            }
        }

        randomRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);


        //Guard state 7 stuff//

        if(guardState == 7)
        {
            agent.speed = 5.35f;
        }
        else
        {
            soundSourceNode.transform.Rotate(Vector3.down * 40 * Time.deltaTime);
        }

        if (guardState == 7 && soundTargetSet == true && hasWaitedBeforeMovingToSoundSource == false)
        {
            if (waitingAfterHavingHeardSound > 0)
            {
                agent.Stop();
                waitingAfterHavingHeardSound = waitingAfterHavingHeardSound - Time.deltaTime;
              
            }

            if (waitingAfterHavingHeardSound <= 0)
            {
                hasWaitedBeforeMovingToSoundSource = true;
                agent.Resume();
                agent.destination = soundSourceNode.transform.position;
            }
        }


        if(guardState == 7 && hasWaitedBeforeMovingToSoundSource == true && soundTargetSet == true)
        {
            agent.Resume();
        }

        if(guardState == 7 && agent.remainingDistance < 0.25 && hasWaitedBeforeMovingToSoundSource == true)
        {
            agent.Stop();
            currentlySearchingSound = true;
            
            if(startSecondSoundLook == false)
            {
                LookAroundAtSoundSource();
            }
            else
            {
                LookAroundAtSoundSource2();
            }

        }
               

    }


    

    //When the guards collide with the player
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && PlayerController.playerCaught == false)
        {
            guardState = 2;            
            caughtPlayer = true;
            PlayerController.playerCaught = true;
            gameObject.GetComponent<AILines>().speech.text = gameObject.GetComponent<AILines>().caughtLines[Random.Range(0, gameObject.GetComponent<AILines>().caughtLines.Length)];            

            StartCoroutine(SendPlayerToJailCoroutine());            
        }
    }

    IEnumerator SendPlayerToJailCoroutine()
    {
        yield return new WaitForSeconds(3.25f);
        caughtPlayer = false;
        patFire = 0;
        guardState = 1;
        playerObject.transform.position = jailTarget.transform.position;

        //For loop to instantiating 
        gobletExplosion();
        plateExplosion();
        gC.GetComponent<PlayerInventory>().goldenGoblet = 0;
        gC.GetComponent<PlayerInventory>().goldenPlate = 0;
        gC.GetComponent<PlayerInventory>().cashTotal = 0;

        yield return null;
    }

       

    void Patrolling()
    {
        //print("State change patrolling");
        agent.Resume();
        //gpat.GotoNextPoint();
        gpat2.moveToNextNode();
        agent.speed = 3.5f;
        fov.viewRadius = 9.5f;
    }


    void PatrollingWhileAlarm()
    {
        agent.speed = 4.5f;
        fov.viewRadius = 12.0f;
        gpat.GotoNextPoint();
    }


    void ChasingPlayer()
    {
        agent.Resume();
        gpat2.waitTimer = 0.0f;
        target = playerObject.transform;

        agent.SetDestination(target.position);
        agent.speed = 7.25f;
                
    }

    
    void MovingToTargetArea()
    {
        agent.speed = 5.5f;

    }


    //Guard State 5 functionality!
    void SearchingTheArea()
    {

        Transform[] possibleHidingSpots = alarmCalledBy.GetComponent<AlarmTargetPoint>().searchSpots;

        for (var i = 0; i < 3; i++)
        {
            nyListeMedGjemmesteder[i] = possibleHidingSpots[Random.Range(0, possibleHidingSpots.Length)];
            
        }
        searchingCheck = true;     
    }
    
    //Looking around function//
    void LookAroundSingle()
    {
        direction = (nyListeMedGjemmesteder[hidingSpotsChecked - 1].GetComponent<PatrolNodes>().lookPoint.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        
    }

    void LookAroundDouble()
    {
        direction = (nyListeMedGjemmesteder[hidingSpotsChecked - 1].GetComponent<PatrolNodes>().lookPoint.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {
            startSecondLook = true;
            LookAroundDouble2();            
        }

    }

    void LookAroundDouble2()
    {
        direction = (nyListeMedGjemmesteder[hidingSpotsChecked - 1].GetComponent<PatrolNodes>().lookPoint2.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {
            
        }
    }

    void LookAroundAtSoundSource()
    {
        directionSound = (soundSourceNode.GetComponent<PatrolNodes>().lookPoint.position - transform.position).normalized;
        lookRotationSound = Quaternion.LookRotation(directionSound);

       
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationSound, Time.deltaTime * rotationSpeedSound);
        if (Quaternion.Angle(transform.rotation, lookRotationSound) < 5)
        {
            startSecondSoundLook = true;
        }
    }

    void LookAroundAtSoundSource2()
    {
        directionSound = (soundSourceNode.GetComponent<PatrolNodes>().lookPoint2.position - transform.position).normalized;
        lookRotationSound = Quaternion.LookRotation(directionSound);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationSound, Time.deltaTime * rotationSpeedSound);

        if (Quaternion.Angle(transform.rotation, lookRotationSound) < 5)
        {
            guardState = 1;
            agent.Resume();
            hasWaitedBeforeMovingToSoundSource = false;
            startSecondSoundLook = false;
            soundTargetSet = false;
            currentlySearchingSound = false;
        }
    }



    void MoveToNextHidingSpot2()
    {
        
        agent.Resume();

        agent.destination = nyListeMedGjemmesteder[hidingSpotsChecked].position;
        hidingSpotsChecked++;

        hidingSpotWaitTime = nyListeMedGjemmesteder[hidingSpotsChecked -1].GetComponent<PatrolNodes>().waitTime;
        lookToSpot = nyListeMedGjemmesteder[hidingSpotsChecked -1].GetComponent<PatrolNodes>().lookPoint;
        lookSpotType = nyListeMedGjemmesteder[hidingSpotsChecked - 1].GetComponent<PatrolNodes>().lookPointType;
        startSecondLook = false;

    }

    void MovingToCameraSpotArea()
    {
        
    }

    void SetSoundSourceTargetpoint()
    {
        waitingAfterHavingHeardSound = 0.75f;
        soundTargetSet = true;        
    }

    public IEnumerator fovLerpChange(float duration)
    {
        scaleFov = true;
        

        var originalFov = fov.viewRadius;

        float progress = duration;       

        while (progress > 0)
        {
            progress -= Time.deltaTime;
            fov.viewRadius = Mathf.Lerp(targetFovRadius, originalFov, progress);
        }

        scaleFov = false;
        yield return null;

    }


    public IEnumerator waitXSeconds(float duration)
    {
        agent.Stop();

        yield return new WaitForSeconds(duration);

        MoveToNextHidingSpot2();

        yield return null;
    }

    void gobletExplosion()
    {
        int tGG = gC.GetComponent<PlayerInventory>().goldenGoblet;

        for (int i = 0; i < tGG; i++)
        {
            Instantiate(gobletPrefab, itemSpawnTarget.position, transform.rotation);
        }
    }

    void plateExplosion()
    {
        int tGP = gC.GetComponent<PlayerInventory>().goldenPlate;

        for (int i = 0; i < tGP; i++)
        {
            Instantiate(platePrefab, itemSpawnTarget.position, Quaternion.Euler(Random.Range(0, 180), 0, (Random.Range(0, 180))));
        }
    }
}

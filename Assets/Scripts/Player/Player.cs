using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour {

    public float currentSpeed;
    public float moveSpeed;
    public bool playerMoving;

    public GameObject lookPointPlane;
    public LayerMask mask = -1;

    public GameObject noiseSphere;
    private Vector3 noiseSphereSize;
    private Vector3 targetScale;
    private IEnumerator scaleCoroutine;

    private Rigidbody rigidB;

    PlayerController controller;
    Camera viewCamera;
    GunController gunController;

    
    
    

	// Use this for initialization
	void Start () {
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();
        controller = GetComponent<PlayerController>();
        noiseSphereSize = noiseSphere.transform.localScale;
        playerMoving = false;
              
	}
	
	// Update is called once per frame
	void Update () {
        

        //movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
     
        
        //Check to see if player is moving
        if(Input.GetButton("W") || Input.GetButton("A") || Input.GetButton("S") || Input.GetButton("D"))
        {         
            playerMoving = true;
        }
        else
        {
            playerMoving = false;
        }

        //Switch that sets target scale for NoiseRing 

        int moveSpeedCase = (int)moveSpeed;
        

        switch (moveSpeedCase)
        {
            case 3:
                targetScale = new Vector3(1.6f, 0.2f, 1.6f);
                break;
            case 4:
                targetScale = new Vector3(2.75f, 0.2f, 2.75f);
                break;
            case 5:
                targetScale = new Vector3(4.5f, 0.2f, 4.5f);
                break;
            case 6:
                targetScale = new Vector3(7.5f, 0.2f, 7.5f);
                break;
            case 7:
                targetScale = new Vector3(11.5f, 0.2f, 11.5f);                
                break;
            default:
                print("Something went wrong here!");                
                break;
        }

     

        //Event for when the scrollwheel is scrolled up/down
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && moveSpeed > 3.5 || Input.GetAxis("Mouse ScrollWheel") > 0f && moveSpeed < 7.5)
        {
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }
            scaleCoroutine = scaleNoiseRing(0.75f);
            StartCoroutine(scaleCoroutine);
        }
        
        //Movespeed changes based on scroll wheel +/-
        moveSpeed += Input.GetAxis("Mouse ScrollWheel");
        moveSpeed = Mathf.Clamp(moveSpeed, 3.5f, 7.5f);

        


        //look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            
            Vector3 point = hit.point;
            controller.LookAt(point);  

    
        //weapon input
        if (Input.GetMouseButtonDown(0))
            {
                gunController.Shoot();
            }

        }
        
        
	}

    //Coroutine that scales the size of the NoiseSphere
    public IEnumerator scaleNoiseRing(float duration)
    {
        
        var originalScale = noiseSphere.transform.localScale;

        float progress = duration;

        while (progress > 0)
        {
            progress -= Time.deltaTime;
            noiseSphere.transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
            yield return null;    
        }
        
        yield return null;

    }
}

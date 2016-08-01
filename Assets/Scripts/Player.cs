using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour {

    
    public float moveSpeed;
    PlayerController controller;
    Camera viewCamera;
    GunController gunController;

	// Use this for initialization
	void Start () {
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();
        controller = GetComponent<PlayerController>();

       
	}
	
	// Update is called once per frame
	void Update () {
        //movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        //Movespeed changes basert på musehjul
        moveSpeed += Input.GetAxis("Mouse ScrollWheel");
        moveSpeed = Mathf.Clamp(moveSpeed, 3.5f, 6.5f);

        //look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayDistance;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            
            Vector3 point = hit.point;
            //Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);  

    
        //weapon input
        if (Input.GetMouseButtonDown(0))
            {
                gunController.Shoot();
            }

        } 
	}
}

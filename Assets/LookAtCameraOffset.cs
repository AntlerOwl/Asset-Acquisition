using UnityEngine;
using System.Collections;
using UnityEngine;

public class LookAtCameraOffset : MonoBehaviour
{


    public GameObject lookTarget;


    // Use this for initialization
    void Start()
    {
        lookTarget = GameObject.Find(("OffsetCamposition"));
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.LookAt(lookTarget.transform);
    }
}

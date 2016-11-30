using UnityEngine;
using System.Collections;

public class Noisemaker : MonoBehaviour {


    public float thrust;
    public float spinSpeed;

    public GameObject panel;

    public Vector3 endSize;

    public Camera mainCam;

    private int activated;
    private int interactingWith;

    private Vector3 sackPosition;
    private GameObject sack;
    private Rigidbody rb;
    private GameObject gc;

    // Use this for initialization
    void Start () {

        sack = GameObject.Find("sackPosition");

        activated = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        gc = GameObject.Find("Game Control");

    }
	
	// Update is called once per frame
	void Update () {

        sackPosition = sack.transform.position;

        if(mainCam != null)
        {
            panel.transform.LookAt(mainCam.transform.position);
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetButton("Fire2") && activated == 0)
        {
            activated = 1;

            gameObject.GetComponent<BoxCollider>().enabled = false;
            panel.SetActive(false);
            StartCoroutine(MoveObjectToSack(1.25f));

        }

        if(other.gameObject.tag == "Player" && Input.GetButton("e") && activated == 0 && interactingWith == 0)
        {
            interactingWith = 1;
            panel.SetActive(true);
        }

    }

    void onTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && interactingWith == 1)
        {
            interactingWith = 0;
            panel.SetActive(false);
            print("Went outta the thing!");
        }

        if(other.gameObject.tag == "Player")
        {
            panel.SetActive(false);
        }
    }

    public IEnumerator MoveObjectToSack(float duration)
    {
        rb.AddForce(new Vector3(0, 550, 0));
        yield return new WaitForSeconds(0.3f);

        var originalPosition = gameObject.transform.position;
        var originalScale = gameObject.transform.localScale;
        var targetScale = endSize;
        float progress = duration;



        while (progress > 0)
        {
            progress -= Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(sackPosition, originalPosition, progress);
            transform.Rotate(Vector3.left * spinSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);

            yield return null;
        }

        gc.GetComponent<PlayerToolsInventory>().noiseMakers++;

        Destroy(gameObject);
        yield return null;
    }

}

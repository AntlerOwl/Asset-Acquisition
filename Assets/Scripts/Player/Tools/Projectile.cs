using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public float speed = 10;
    private bool hasHit = false;
    private float lifeTime = 20.0f;

    private BoxCollider bCol;

    public LayerMask collisionMask;

    // Use this for initialization
    void Start()
    {
        bCol = gameObject.GetComponent<BoxCollider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);

        if (hasHit == true)
        {
            CountDownUntilDestroy();
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        speed = 0;
        hasHit = true;
        bCol.enabled = !bCol.enabled;
        if (hit.collider.gameObject.tag == "Guard")
        {
            gameObject.transform.parent = hit.collider.gameObject.transform;
        }


    }

    void CountDownUntilDestroy()
    {
        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}

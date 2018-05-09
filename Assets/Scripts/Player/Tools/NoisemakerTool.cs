using UnityEngine;
using System.Collections;

public class NoisemakerTool : Gun {
    
    private GameObject gc;
    public GameObject visPart;

    void Awake()
    {
        gc = GameObject.Find("Game Control");
    }

	// Use this for initialization
	void Start () {

       
	
	}
	
	// Update is called once per frame
	void Update () {

        if(gc.GetComponent<PlayerToolsInventory>().noiseMakers >= 1)
        {
            visPart.SetActive(true);
        }
        else
        {
            visPart.SetActive(false);
        }

    }

    public override void Shoot()
    {

        if (Time.time > nextShotTime && gc.GetComponent<PlayerToolsInventory>().noiseMakers > 0)
        {
            
            gc.GetComponent<PlayerToolsInventory>().noiseMakers--;
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);


        }        

    }


}

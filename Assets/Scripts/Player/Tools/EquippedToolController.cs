using UnityEngine;
using System.Collections;

public class EquippedToolController : MonoBehaviour {

    public Transform weaponHold;
    public Gun startingGun;
    public Gun noiseMaker;
    public Gun equippedGun;

    public int currentlyEquippedWeaponNumber;


	// Use this for initialization
	void Start () {

        if(startingGun != null)
        {
            EquipGun(startingGun);
        }

        currentlyEquippedWeaponNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {

        
	
	}

    public void EquipGun (Gun gunToEquip)
    {
        if(equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;

    }

    public void Shoot()
    {
        if(equippedGun != null)
        {
            equippedGun.Shoot();
        }
        
        /*
        if(currentlyEquippedWeaponNumber == 1)
        {
            equippedGun.Shoot();
        }

        if (currentlyEquippedWeaponNumber == 2)
        {
            equippedGun.ShootNoise();
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] MainCharacter mainCharacter;

    //[SerializeField] GameObject currentWeapon;

    [SerializeField] LaserPistol laserPistol;


    // Start is called before the first frame update
    void Start()
    {
        laserPistol = mainCharacter.GetWeaponController().GetCurrentWeapon();
    }

    void FixedUpdate()
    {
        if (mainCharacter.GetIsDead()) {
            return;
        }

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            movement += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            movement += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            movement += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            movement += new Vector3(1, 0, 0);
        }

        mainCharacter.Move(movement);
    }

    // Update is called once per frame
    void Update() {

        if (mainCharacter.GetIsDead()) {
            return;
        }
        
        mainCharacter.AimTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetButton("Fire1") && !laserPistol.GetIsFiring()) {
            laserPistol.StartCoroutine(laserPistol.FireWeaponRoutine());
        }
        if (Input.GetButtonUp("Fire1")) {
            laserPistol.SetIsFiring(false);
            StopCoroutine(laserPistol.FireWeaponRoutine());
        }
        if (Input.GetKey(KeyCode.R) && laserPistol.GetCurrentAmmo() != laserPistol.GetMaxAmmo()) {
            //Need to add coroutine for this so that it takes time
            laserPistol.Reload();
        }
        if (Input.GetKey(KeyCode.Alpha1) && !laserPistol.GetIsReloading()) {
            mainCharacter.GetWeaponController().SetWeapon1();
        }
        if (Input.GetKey(KeyCode.Alpha2) && !laserPistol.GetIsReloading()) {
            mainCharacter.GetWeaponController().SetWeapon2();
        }
    }

    public MainCharacter GetMainCharacter() {
        return mainCharacter;
    }

    public void SetLaserPistol(LaserPistol newWeapon) {
        laserPistol = newWeapon;
    }
    
}

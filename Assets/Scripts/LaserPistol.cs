using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPistol : Weapon
{
    
    [SerializeField] GameObject bullet;
    [SerializeField] FirePoint firePoint;
    [SerializeField] float fireRate = .24f;
    [SerializeField] float bulletSpeed = 50.0f;
    [SerializeField] int maxAmmo = 16;
    [SerializeField] float reloadTime = 1.1f;
    [SerializeField] int currentAmmo;
    [SerializeField] int projectileCount;
    [SerializeField] float accuracy = 1f;
    private bool isFiring = false;
    private float lastFireTime = 0;
    public bool isReloading = false; // change back to private

    [SerializeField] float minAccuracy;
    [SerializeField] float maxAccuracy;

    

    // Start is called before the first frame update
    void Awake() {
        currentAmmo = maxAmmo;
    }

    public override void Shoot() {
        if (currentAmmo < 1) {
            //Create UI for showing bullets. 
            //Also want to put some flashy UI that says "Press R to reload!"
            //Also want to play an empty gun sound here.
            return;
        }
        if (isReloading) {
            return;
        }

        if (Time.time >= lastFireTime + fireRate) {
            //accuracy = Random.Range(.5f, 1f);
            firePoint.Shoot(bulletSpeed, minAccuracy, maxAccuracy, projectileCount);
            lastFireTime = Time.time;
            currentAmmo--;
        }
        else {
            return;
        }

        
    }

    void Update()
    {
        
    }

    public IEnumerator FireWeaponRoutine() {
        isFiring = true;

        while (isFiring) {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void Reload() {

        if (isReloading || currentAmmo == maxAmmo) {
            return;
        }
        isReloading = true;
        StartCoroutine(ReloadRoutine());

        IEnumerator ReloadRoutine() {
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            isReloading = false;
        }
    }

    public GameObject GetBullet() {
        return bullet;
    }

    public int GetCurrentAmmo() {
        return currentAmmo;
    }

    public int GetMaxAmmo() {
        return maxAmmo;
    }

    public bool GetIsFiring() {
        return isFiring;
    }

    public void SetIsFiring(bool res) {
        isFiring = res;
    }
    public bool GetIsReloading() {
        return isReloading;
    }
}

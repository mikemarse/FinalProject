using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

    [SerializeField] MainCharacter mainCharacter;
    [SerializeField] PlayerInputHandler playerInputHandler;
    int totalWeapons = 1;
    [SerializeField] int currentWeaponIndex;
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject weaponController;
    [SerializeField] GameObject currentWeapon;


    [SerializeField] Image weaponImage;
    [SerializeField] Sprite laserShotgun;
    [SerializeField] Sprite laserPistol;
    
    
    void Awake()
    {
        totalWeapons = weaponController.transform.childCount;
        weapons = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++) {
            weapons[i] = weaponController.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }
        weapons[1].SetActive(true);
        currentWeapon = weapons[1];
        weaponImage.sprite = laserShotgun;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWeapon1() {
        if (currentWeapon.GetComponent<LaserPistol>().GetIsReloading()) {
            return;
        }
        for (int i = 0; i < totalWeapons; i++) {
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
        weaponImage.sprite = laserPistol;
        mainCharacter.SetLaserPistol(weapons[0].GetComponent<LaserPistol>());
        playerInputHandler.SetLaserPistol(weapons[0].GetComponent<LaserPistol>());
    }

    public void SetWeapon2() {
        if (currentWeapon.GetComponent<LaserPistol>().GetIsReloading()) {
            return;
        }
        for (int i = 0; i < totalWeapons; i++) {
            weapons[i].SetActive(false);
        }
        weapons[1].SetActive(true);
        weaponImage.sprite = laserShotgun;
        mainCharacter.SetLaserPistol(weapons[1].GetComponent<LaserPistol>());
        playerInputHandler.SetLaserPistol(weapons[1].GetComponent<LaserPistol>());
    }

    public LaserPistol GetCurrentWeapon() {
        return currentWeapon.GetComponent<LaserPistol>();
    }
}

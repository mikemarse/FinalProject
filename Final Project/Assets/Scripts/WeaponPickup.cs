using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    [SerializeField] MainCharacter mainCharacter;
    private bool inRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            inRange = false;
        }
    }

    void Pickup()
    {
        if (gameObject.name == "LaserPistolPickup") {
            mainCharacter.GetWeaponController().SetWeapon1();
        }
        else if (gameObject.name == "LaserShotgunPickup") {
            mainCharacter.GetWeaponController().SetWeapon2();
        }
        Destroy(gameObject);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && inRange) {
            Pickup();
        }
    }
}

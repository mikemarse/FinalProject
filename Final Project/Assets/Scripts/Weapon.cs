using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    //public float fireRate;
    public float damage;
    //public Transform firePoint;

    
    public abstract void Shoot();

}

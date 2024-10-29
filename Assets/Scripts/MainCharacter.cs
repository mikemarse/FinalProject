using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    [SerializeField] ScreenManager screenManager;

    [SerializeField] FirePoint firePoint;
    [SerializeField] LaserPistol currentWeapon;
    [SerializeField] WeaponController weaponController;

    [Header("Movement")]
    [SerializeField] float speed = 10;


    [Header("Aiming")]
    [SerializeField] Transform targetTransform;

    [SerializeField] Rigidbody2D rigidBody;

    [Header("Tools")]

    //[SerializeField] Weapon currentWeapon;


    // Trackers
    bool isDead = false;

    // Start is called before the first frame update

    void Awake()
    {
        // currentWeapon = weaponController.GetCurrentWeapon();
    }

    void Start()
    {
        currentWeapon = weaponController.GetCurrentWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            Debug.Log("You are dead!");
        }
    }

    void FixedUpdate() {
        
    }

    public void Move(Vector3 movement) {
        //rigidBody.velocity = movement * speed;
        rigidBody.MovePosition(transform.position + (movement * speed) * Time.fixedDeltaTime);
    }

    public void AimTarget(Transform targetTransform) {
        AimTarget(targetTransform.position);
    }

    public void AimTarget(Vector3 aimPos) {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
    }

    // // Probably will send a parameter later to know which weapon to attack with.
    // public void attack() {
    //     firePoint.Shoot();
    //     **attack with current weapon here**
    // }

    public LaserPistol GetLaserPistol() {
        return currentWeapon;
    }

    public void SetLaserPistol(LaserPistol newWeapon) {
        currentWeapon = newWeapon;
    }

    public WeaponController GetWeaponController() {
        return weaponController;
    }

    public void GameOver() {
        screenManager.GameOver();
    }

    public bool GetIsDead() {
        return isDead;
    }

    public void SetIsDead(bool res) {
        isDead = res;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{

    //[SerializeField] MainCharacter mainCharacter;

    [Header("Config")]

    //public Weapon equippedWeapon;

    [SerializeField] GameObject bullet;

    // Start is called before the first frame update

    // Update is called once per frame
    public void Shoot(float bulletSpeed, float minAccuracy, float maxAccuracy, int projectileCount) {

        for (int i = 0; i < projectileCount; i++) {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            float accuracy = Random.Range(minAccuracy, maxAccuracy);
            float randomAccuracy = Random.Range(-accuracy * 90f, accuracy * 90f);
            newBullet.transform.Rotate(new Vector3(0, 0, randomAccuracy));
            newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.up * bulletSpeed;

            Destroy(newBullet, 15);
        }
        

    }
}

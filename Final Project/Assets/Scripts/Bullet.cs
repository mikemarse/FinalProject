using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            //Will do nothing bc we dont want to hit other bullets
            return;
        }

        if (other.CompareTag("Enemy")) {
            Debug.Log("Killed an ENEMY!");
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player")) {
            Debug.Log("You have died. Press R to restart!");
            //Need to switch scenes here or play a scene where I need to press R to respawn.
            Destroy(gameObject);
            MainCharacter mainCharacter = other.GetComponent<MainCharacter>();
            mainCharacter.SetIsDead(true);
            mainCharacter.GameOver();
            //Probably need to do something better here instead of just disabling the player.
            //Will just use a death animation here and block any movement.
            other.gameObject.SetActive(false);
            return;
        }
        if (other.CompareTag("Pickup")) {
            return;
        }
        //Keep this for now.
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float calmMovement = 2f;
    [SerializeField] public float angerMovement = 7f;
    [SerializeField] LaserPistol laserPistol;

    // Start is called before the first frame update
    public void Move(Vector3 movement, float speed) {
        //rigidBody.velocity = movement * speed;

        //old movement
        //rigidBody.MovePosition(transform.position + (movement * speed) * Time.fixedDeltaTime);


        if (movement != Vector3.zero) {
            movement.Normalize();  // Normalize to get direction
            Vector3 velocity = movement * speed;  // Apply speed to the normalized direction

            rigidBody.velocity = velocity;  // Set velocity on the Rigidbody directly
        }
        else {
            Stop();  // If no movement vector, stop the object
        }
    }

    public void MoveToward(Vector3 position) {
        Move(position - transform.position, angerMovement);
    }

    public void AimTarget(Vector3 aimPos) {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
    }

    public void Stop() {
        rigidBody.velocity = Vector3.zero;
    }

    public void Pursue(Vector3 goalPos) {
        goalPos.z = 0;
        Vector3 direction = goalPos - transform.position;
        Move(direction.normalized, angerMovement);
    }

    public void PatrolMove(Vector3 goalPos) {
        goalPos.z = 0;
        Vector3 direction = goalPos - transform.position;
        Move(direction.normalized, calmMovement);
    }

    public LaserPistol GetLaserPistol() {
        return laserPistol;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float calmMovement = 2f;
    [SerializeField] float angerMovement = 5f;
    [SerializeField] LaserPistol laserPistol;

    // Start is called before the first frame update
    public void Move(Vector3 movement, float speed) {
        //rigidBody.velocity = movement * speed;
        rigidBody.MovePosition(transform.position + (movement * speed) * Time.fixedDeltaTime);
    }

    public void AimTarget(Vector3 aimPos) {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
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

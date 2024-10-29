using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Node currentNode;
    public List<Node> path = new List<Node>();

    [SerializeField] string currentStateString;
    [SerializeField] Enemy myEnemy;
    [SerializeField] MainCharacter mainCharacter;
    private LaserPistol lasterPistol;

    [SerializeField] float sightDistance = 10;
    [SerializeField] float pauseTime = .9f;

    delegate void AIState();
    AIState currentState;



    //TRACKERS -------------------------------------
    private float stateTime;
    private bool justChangedState = false;
    private Vector3 lastKnownPos;

    // Start is called before the first frame update
    void Start()
    {
        changeState(Idle);
    }

    void changeState(AIState newAIState) {
        currentState = newAIState;
        justChangedState = true;
    }

    void Idle() {
        //do nothing;
        if (stateTime == 0) {
            currentStateString = "Idle State";
        }


        if (DetectedCharacter()) {
            changeState(Attack);
            return;
        }
    }

    void CharacterDead() {
        //Do absolutely nothing
    }

    void Attack() {
        //Follow MC

        if (stateTime == 0) {
            currentStateString = "Attack State";
        }


        myEnemy.Pursue(mainCharacter.transform.position);
        myEnemy.AimTarget(mainCharacter.transform.position);

        if (mainCharacter.GetIsDead()) {
            changeState(CharacterDead);
        }

        if (stateTime > 1) {
            myEnemy.GetLaserPistol().Shoot();
        }

        if (myEnemy.GetLaserPistol().GetCurrentAmmo() == 0) {
            myEnemy.GetLaserPistol().Reload();
        }

        if (!DetectedCharacter()) {
            lastKnownPos = mainCharacter.transform.position;
            changeState(GetLastKnownPos);
            return;
        }
    }

    void GetLastKnownPos() {

        if (stateTime == 0) {
            currentStateString = "LastKnownPos State";
        }

        myEnemy.PatrolMove(lastKnownPos);
        myEnemy.AimTarget(lastKnownPos);

        if (mainCharacter.GetIsDead()) {
            changeState(CharacterDead);
        }

        if (DetectedCharacter()) {
            changeState(Attack);
            return;
        }
        
        if (stateTime < 2) {
            return;
        }

        if (Vector3.Distance(myEnemy.transform.position, lastKnownPos) < 1f) {
            changeState(Patrol);
            return;
        }
    }

    Vector3 patrolPos;
    Vector3 patrolPivot;
    void Patrol() {
        //pick a random position (Doesn't have to be random, maybe can set each a transform for each Enemy)

        if (stateTime == 0) {
            currentStateString = "Patrol State";
            //patrolPivot = myEnemy.transform.position;
            patrolPos = myEnemy.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }

        myEnemy.PatrolMove(patrolPos);
        myEnemy.AimTarget(patrolPos);

        if (mainCharacter.GetIsDead()) {
            changeState(CharacterDead);
        }

        if (DetectedCharacter()) {
            changeState(Attack);
            return;
        }

        if (stateTime < 2) {
            return;
        }

        if (Vector3.Distance(myEnemy.transform.position, patrolPos) < 1f) {
            //patrolPos = patrolPivot + new Vector3(Random.Range(-sightDistance, sightDistance), Random.Range(-sightDistance, sightDistance));
            changeState(Patrol);
            return;
        }

    }

    void AITick() {
        if (justChangedState) {
            stateTime = 0;
            justChangedState = false;
        }
        currentState();
        stateTime += Time.deltaTime;
    }

       // Update is called once per frame
    void Update()
    {
        AITick();
    }

    bool DetectedCharacter() {
        return Vector3.Distance(myEnemy.transform.position, mainCharacter.transform.position) < sightDistance;
        //Vector3.Distance(myEnemy.transform.position, mainCharacter.transform.position) > sightDistance
    }
}

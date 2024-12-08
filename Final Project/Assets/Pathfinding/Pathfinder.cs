using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [Header("Config")]
    public LayerMask pathBlockMask;
    public float aStarScanDist = 1;
    public bool spawnMarkers = true;


    [Header("Trackers")]
    public bool findingPath = false;
    public bool reachedDestination = false;

    [Header("Prefabs")]
    public GameObject waypointMarker;

    [Header("Objects")]
    public Transform goal;
    Enemy myEnemy;

    EnemyAI enemyAI;


    // Start is called before the first frame update
    void Start()
    {
        myEnemy = GetComponent<Enemy>();
        enemyAI = GetComponent<EnemyAI>();
        //StartCoroutine(Pathfind());
    }


    IEnumerator Pathfind() {
        bool needPath = true;
        
        while (!reachedDestination) {


            GetAStarToTarget(goal.transform.position);
            yield return new WaitUntil(() =>findingPath == false);
            needPath = false;

            if (spawnMarkers == true) {
                foreach(Vector2 v in aStarPath) {
                Instantiate(waypointMarker, v, Quaternion.identity);
                yield return new WaitForSeconds(.1f);
            }
            }

            foreach(Vector2 v in aStarPath) {
                while (Vector3.Distance(transform.position, v) > 0.1f) {
                    if (CanReachPos(transform.position, v)) {
                        myEnemy.MoveToward(v);
                        if (!enemyAI.DetectedCharacter()) {
                          myEnemy.AimTarget(v);  
                        }
                        else {
                            myEnemy.AimTarget(enemyAI.mainCharacter.transform.position);
                        }
                    } else {
                        needPath = true;
                        break;
                    }
                    yield return new WaitForFixedUpdate();
                }
                if (needPath) {
                    break;
                }
                else {
                    transform.position = v;
                }
            }
            if (!needPath) {
                reachedDestination = true;
            }

            yield return null;
        }

        myEnemy.Stop();
    }

    public void CalculatePath() {
        reachedDestination = false;
        StopCoroutine(Pathfind());
        StartCoroutine(Pathfind());
    }


    void GetAStarToTarget(Vector3 targetPos) {
        findingPath = true;
        aStarPath = new List<Vector2>();
        StartCoroutine(AStarToTargetRoutine(targetPos));
    }

    IEnumerator AStarToTargetRoutine(Vector3 targetPos) {
        Vector3 myPos = transform.position;

        HashSet<Vector2> closedSet = new HashSet<Vector2>();

        HashSet<Vector2> openSet = new HashSet<Vector2>();
        openSet.Add(myPos);

        cameFrom = new Dictionary<Vector2, Vector2>();


        Dictionary<Vector2, float> gScore = new Dictionary<Vector2, float>();
        gScore.Add(myPos, 0);


        Dictionary<Vector2, float> fScore = new Dictionary<Vector2, float>();
        fScore.Add(myPos, HeuristicCostEstimate(myPos, targetPos));

        int maxAttempts = 400;

        while (openSet.Count > 0 && maxAttempts > 0 ) {
            maxAttempts -= 1;


            float minF = int.MaxValue;
            Vector2 minFVector = new Vector2(-1, -1);


            // use a prio queue here instead
            foreach(Vector2 v in openSet) {
                if (fScore[v] < minF) {
                    minF = fScore[v];
                    minFVector = v;
                }
            }

            Vector2 current = minFVector;

            yield return null;



            if (Vector2.Distance(current, targetPos) <= aStarScanDist && CanReachPos(current, targetPos)) {
                ReconstructPath(current);
            }
            else {
                openSet.Remove(current);
                closedSet.Add(current);


                List<Vector2> neighbors = new List<Vector2>();
                for (float x = -1; x < 2; x++) {
                    for (float y = -1; y < 2; y++) {
                        if (x == 0 || y == 0) {
                            if(CanReachPos(current, current + new Vector2(x*aStarScanDist, y*aStarScanDist))) {
                                neighbors.Add(current + new Vector2(x,y)*aStarScanDist);
                            }
                        }
                    }
                }


                foreach(Vector2 neighbor in neighbors) {
                    if (!closedSet.Contains(neighbor)) {
                        float new_gScore = gScore[current] + aStarScanDist;


                        bool worsePath = false;

                        if (!openSet.Contains(neighbor)) {
                            openSet.Add(neighbor);
                        }
                        else if (new_gScore >= gScore[neighbor]) {
                            worsePath = true;
                        }

                        if (!worsePath) {
                            cameFrom[neighbor] = current;
                            gScore[neighbor] = new_gScore;
                            fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, targetPos);
                        }
                    }
                }
            }
        }
        findingPath = false;
    }


    bool CanReachPos(Vector3 startPos, Vector3 endPos) {
        return !Physics2D.Linecast(startPos, endPos, pathBlockMask);
    }

    float HeuristicCostEstimate(Vector2 start, Vector2 goal) {
        return Mathf.Abs(start.x - goal.x) + Mathf.Abs(start.y - goal.y);
    }


    public List<Vector2> aStarPath;
    Dictionary<Vector2, Vector2> cameFrom;
    

    void ReconstructPath(Vector2 current) {
        aStarPath = new List<Vector2>();


        aStarPath.Add(current);
        while(cameFrom.ContainsKey(current)) {
            current = cameFrom[current];
            aStarPath.Add(current);
        }

        aStarPath.Reverse();


        aStarPath.Add(goal.transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            CalculatePath();
        }
    }


    //Whenever the enemy hears a sound, or sees Player who leaves his FOV, setLastKnownPos to goal, then it 
    //will recalcualte the lastKnownPos, I need to find out if this works. Don't know if i pass transform.position or what

    //Maybe I could do something like val = goal.transform.position then I could change replace all those instances
    // with val and then create a public method for setting val.
    public void SetLastKnownPos(Transform lastKnownPos) {
        goal = lastKnownPos;
    }

    public void SetNewGoal(Transform newGoal) {
        goal = newGoal;
    }
}

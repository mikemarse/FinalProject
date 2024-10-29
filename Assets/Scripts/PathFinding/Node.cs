using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Node cameFrom;
    [SerializeField] List<Node> connections;
    public float gScore;
    public float hScore;

    public float FScore() {
        return gScore + hScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetGScore() {
        return gScore;
    }

    public float GetHScore() {
        return hScore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Vector3 destination;

    // Starts fish in a position with a destination
    void Start()
    {
        transform.position = PositionOnNavMesh();
        SetDestination();
    }

    // Updates destination
    void Update()
    {
        if (agent.remainingDistance < 0.1f)
            SetDestination();
    }

    // Sets new destination
    void SetDestination()
    {
        agent.SetDestination(PositionOnNavMesh());
    }

    // Returns a position on the navmesh
    Vector3 PositionOnNavMesh()
    {
        return new Vector3(Random.Range(-60.66666f, 60.66666f), 0f, Random.Range(-44f, 44f));
    }
}
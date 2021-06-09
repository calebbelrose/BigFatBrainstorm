using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    // Updates the destination of the shark
    void Update()
    {
        RaycastHit _hit;

        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out _hit, 100f))
            agent.SetDestination(_hit.point);
    }

    // Eats nearby fish
    private void OnTriggerEnter(Collider _other)
    {
        gameManager.EatFish(_other.gameObject);
    }
}

using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float stoppingDistance;

    private Transform target;
    private NavMeshSurface surface;
    private NavMeshAgent agent;

    private void Start()
    {
        this.target = FindFirstObjectByType<Movement>().transform;
        this.agent = GetComponent<NavMeshAgent>();
        this.surface = FindFirstObjectByType<NavMeshSurface>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = this.stoppingDistance;
        surface.BuildNavMesh();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}

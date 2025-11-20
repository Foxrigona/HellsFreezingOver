using NavMeshPlus.Components;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float detectionRadius = 10f;

    private Transform target;
    private NavMeshSurface surface;
    private NavMeshAgent agent;
    private ActorType actorType;

    private void Start()
    {
        setActorType();
        this.agent = GetComponent<NavMeshAgent>();
        this.surface = FindFirstObjectByType<NavMeshSurface>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = this.stoppingDistance;
        surface.BuildNavMesh();
    }

    public void setActorType()
    {
        this.actorType = GetComponent<Health>().getActorType();
        this.target = findTarget();
    }

    private Transform findTarget()
    {
        if (actorType == ActorType.Rebel) return FindFirstObjectByType<Movement>().transform;
        else
        {
            Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Rebel"));
            if (targetColliders.Length == 0) return null;
            return targetColliders[Random.Range(0, targetColliders.Length)].transform;
        }
    }

    private void Update()
    {
        if (target == null) target = findTarget();
        if (target != null) agent.SetDestination(target.position);
    }
}

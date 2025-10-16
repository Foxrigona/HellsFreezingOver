using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshBaker : MonoBehaviour
{
    public UnityEvent<Collider2D, bool> rebakeMesh = new UnityEvent<Collider2D, bool>();
    private NavMeshSurface surface;

    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        rebakeMesh.AddListener(rebake);
    }

    private void rebake(Collider2D collider, bool colliderEnabled)
    {
        if(collider != null) collider.enabled = colliderEnabled;
        surface.BuildNavMesh();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public abstract class EssenceCollector : MonoBehaviour
{
    EnemyKillHandler communicator;
    [SerializeField] private int essenceNeeded = 0;
    [SerializeField] private int essenceFed = 0;
    public void Start()
    {
        this.communicator = FindFirstObjectByType<EnemyKillHandler>();
        GetComponent<Collider2D>().layerOverridePriority = 2;
    }
    public void Update()
    {
        if(Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D[] hit = Physics2D.RaycastAll(mousePosition, Vector2.zero);
            
            foreach(RaycastHit2D target in hit)
            {
                if (target.transform == this.transform)
                {
                    essenceFed += communicator.removeEssenceAmount(this.essenceNeeded - essenceFed);
                    if (this.essenceFed == essenceNeeded) performAction();
                }
            }
            
        }
    }

    protected abstract void performAction();
}

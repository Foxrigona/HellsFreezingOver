using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public abstract class EssenceCollector : MonoBehaviour
{
    EnemyKillHandler communicator;
    [SerializeField] protected int essenceNeeded = 0;
    [SerializeField] protected int essenceFed = 0;
    [SerializeField] protected TextMeshProUGUI value;
    public void Start()
    {
        value = GetComponentInChildren<TextMeshProUGUI>();
        this.communicator = FindFirstObjectByType<EnemyKillHandler>();
        GetComponent<Collider2D>().layerOverridePriority = 2;
        this.updateVisuals();
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
                    this.updateVisuals();
                    if (this.essenceFed == essenceNeeded) performAction();
                }
            }
            
        }
    }

    protected void updateVisuals()
    {
        value.text = this.essenceFed.ToString()+  "/" + this.essenceNeeded.ToString();
    }

    protected abstract void performAction();
}

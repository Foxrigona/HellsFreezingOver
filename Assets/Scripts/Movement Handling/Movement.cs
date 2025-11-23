using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float dashFactor = 2f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    private InputAction movementReference;
    private InputAction dashReference;
    private Rigidbody2D rb;

    private void Awake()
    {
        this.movementReference = InputSystem.actions.FindAction("Move");
        this.dashReference = InputSystem.actions.FindAction("Sprint");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        UpgradeHandler upgradeHandler = FindFirstObjectByType<UpgradeHandler>();
        upgradeHandler.moveSpeedUpgrade.AddListener(this.increaseSpeed);
        upgradeHandler.dashSpeedUpgrade.AddListener(this.increaseDashSpeed);
    }

    private void Update()
    {
        this.setVelocity();
        if (dashReference.WasPressedThisFrame() && this.canDash) dash();
    }
    private void setVelocity()
    {
        if (isDashing) return;

        Vector2 force = movementReference.ReadValue<Vector2>();
        Vector2 velocity = rb.linearVelocity;

        //Apply the force to the object
        this.rb.AddForce(force * this.acceleration);

        this.rb.linearVelocity = new Vector2(force.x, force.y) * this.maxSpeed;
    }
    //TODO: Allow character to dash(can be teleport) when shift is pressed

    private void dash()
    {
        this.isDashing = true;
        this.canDash = false;
        this.rb.linearVelocity *= this.dashSpeed * this.dashFactor;
        StartCoroutine(startDash());
    }

    public void increaseSpeed(float speed)
    {
        this.maxSpeed += speed;
        NPCMovement[] allies = this.findAllies();
        foreach (NPCMovement ally in findAllies())
        {
            ally.increaseSpeed(speed);
        }
    }

    public float getSpeed()
    {
        return this.maxSpeed;
    }

    public void increaseDashSpeed(float speedIncrement)
    {
        this.dashSpeed += speedIncrement;
    }

    private NPCMovement[] findAllies()
    {
        WildRogueHealth[] health = FindObjectsByType<WildRogueHealth>(FindObjectsSortMode.None);
        List<NPCMovement> allies = new List<NPCMovement>();
        foreach (WildRogueHealth rogue in health)
        {
            if(rogue.getActorType() == ActorType.Rebel) allies.Add(rogue.transform.GetComponent<NPCMovement>());
        }
        return allies.ToArray();
    }

    IEnumerator startDash()
    {
        yield return new WaitForSeconds(this.dashTime);
        this.isDashing = false;
        yield return new WaitForSeconds(dashCooldown - this.dashTime);
        this.canDash = true;        
    }
}

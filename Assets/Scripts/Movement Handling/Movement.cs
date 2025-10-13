using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float dashFactor = 2f;
    [SerializeField] private float dashSpeed = 0.5f;
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

        //Cap the velocity
        if (velocity.magnitude > this.maxSpeed && !isDashing) this.rb.linearVelocity = velocity * this.maxSpeed/velocity.magnitude;

        //Turn the X component to 0 if off and same to Y
        if (force.x == 0f) this.rb.linearVelocityX = 0;
        if (force.y == 0f) this.rb.linearVelocityY = 0;
    }
    //TODO: Allow character to dash(can be teleport) when shift is pressed

    private void dash()
    {
        this.isDashing = true;
        this.canDash = false;
        this.rb.linearVelocity *= this.maxSpeed * this.dashFactor;
        StartCoroutine(startDash());
    }

    IEnumerator startDash()
    {
        yield return new WaitForSeconds(dashSpeed);
        this.isDashing = false;
        yield return new WaitForSeconds(dashCooldown - dashSpeed);
        this.canDash = true;        
    }
}

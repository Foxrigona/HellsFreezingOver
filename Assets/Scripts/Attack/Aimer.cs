using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aimer : MonoBehaviour
{
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private GameObject bullet;
    private float currentCooldown;

    private void Start()
    {
        this.currentCooldown = this.cooldown;
        StartCoroutine(startShooting());
    }

    private void shoot()
    {
        Vector3 aimDirection = getAimDirection(this.transform.position);
        Instantiate(bullet, this.transform.position + aimDirection, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, aimDirection)));
    }

    public static Vector2 getAimDirection(Vector2 playerPosition, Vector2 aimPosition)
    {
        Vector2 directionVector = (aimPosition - playerPosition).normalized;
        return directionVector;
    }
    public static Vector2 getAimDirection(Vector2 playerPosition)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 directionVector = (mousePosition - playerPosition).normalized;
        return directionVector;
    }

    private IEnumerator startShooting()
    {
        while (true)
        {
            shoot();
            yield return new WaitForSeconds(cooldown);
        }
    }
}

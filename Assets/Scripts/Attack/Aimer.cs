using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aimer : MonoBehaviour
{
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AbilityScriptableObject bulletData;
    [SerializeField] private float distanceFromPlayer = 0.5f;

    [SerializeField] private int bulletsPerShot = 1;
    [SerializeField] private float offsetAngle;


    private void Start()
    {
        StartCoroutine(startShooting());

        UpgradeHandler upgradeHandler = FindFirstObjectByType<UpgradeHandler>();
        upgradeHandler.bulletCountUpgrade.AddListener(increaseBulletCount);
        upgradeHandler.bulletCooldownUpgrade.AddListener(decreaseTimerCount);
    }

    private void shoot(float offsetAngle)
    {
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, offsetAngle));
        Vector3 aimDirection = rotationMatrix.MultiplyPoint(getAimDirection(this.transform.position));

        GameObject bullet = Instantiate(projectilePrefab, this.transform.position + aimDirection * distanceFromPlayer, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, aimDirection)));

        bullet.GetComponent<Projectile>().setAbilityInformation(bulletData, ActorType.Rebel);
        bullet.GetComponent<SpriteRenderer>().size = new Vector2(0.1f,0.5f);
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

    private void increaseBulletCount(int bulletIncrement)
    {
        this.bulletsPerShot += bulletIncrement;
    }

    private void decreaseTimerCount(float divisionAmount)
    {
        this.cooldown /= divisionAmount;
    }

    private IEnumerator startShooting()
    {
        while (true)
        {
            for(int i = 0; i < this.bulletsPerShot; i++)
            {
                //Returns the bulletCount divided by 2
                float x = (float)i / 2;
                //Returns the highest integer to the x value
                float y = Mathf.Ceil(x);
                //Checks if x is a whole number
                bool z = x % 1 == 0;

                //Checks whether to shoot bullet from left or right side of character
                int factor;
                if (z == false) factor = -1;
                else factor = 1;

                //Shoots the bullet
                shoot(offsetAngle * y * factor);
            }
            yield return new WaitForSeconds(this.cooldown);
        }
    }
}

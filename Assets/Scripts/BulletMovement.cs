using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;

    private void Update()
    {
        this.transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }
}

using System.Collections;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;
    [SerializeField] private float despawnTime = 5f;

    private void Start()
    {
        StartCoroutine(destroyBullet(this.despawnTime));
    }

    private void Update()
    {
        this.transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    private IEnumerator destroyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}

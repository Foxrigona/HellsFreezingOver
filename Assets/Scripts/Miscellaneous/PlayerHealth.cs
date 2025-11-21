using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] private float damageCooldown = 1f;
    private bool isInvincible = false;
    private PlayerVisualizer playerVisualizer;

    public void Start()
    {
        playerVisualizer = GetComponentInChildren<PlayerVisualizer>();
    }

    public override void kill()
    {
        SceneManager.LoadScene(2);
    }

    public override void decreaseHealth(float damage)
    {
        if (!isInvincible)
        {
            base.decreaseHealth(damage);
            StartCoroutine(startDamageCooldown());
            playerVisualizer.visualizeDamage();
        }
            
    }

    private IEnumerator startDamageCooldown()
    {
        this.isInvincible = true;
        yield return new WaitForSeconds(damageCooldown);
        this.isInvincible = false;
    }
}

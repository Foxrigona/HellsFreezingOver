using System.Collections;
using UnityEngine;

public class PlayerVisualizer : MonoBehaviour
{
    [SerializeField] private float damageDuration;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color damagedColor;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    public void visualizeDamage()
    {
        StartCoroutine(startDamageVisualzation());
    }

    private IEnumerator startDamageVisualzation()
    {
        this.playerSprite.color = damagedColor;
        yield return new WaitForSeconds(damageDuration);
        this.playerSprite.color = baseColor;
    } 
}

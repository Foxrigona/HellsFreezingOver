using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int health;
    public int damage;
    public float hitDelay;
    public float speed;
    public int demonEssenceValue;
}

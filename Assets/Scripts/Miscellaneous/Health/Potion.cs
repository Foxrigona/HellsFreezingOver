using UnityEngine;

public class Potion : MonoBehaviour
{
    public static int healingValue = 40;

    public static void recoverHealth(Health health)
    {
        health.recoverHealth(healingValue);
    }

}

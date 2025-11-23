using UnityEngine;
using UnityEngine.Events;

public class UpgradeHandler : MonoBehaviour
{
    public UnityEvent<int> bulletCountUpgrade;
    public UnityEvent<float> bulletCooldownUpgrade;
    public UnityEvent<float> moveSpeedUpgrade;
    public UnityEvent<float> dashSpeedUpgrade;

    private void Awake()
    {
        bulletCountUpgrade = new UnityEvent<int>();
        bulletCooldownUpgrade = new UnityEvent<float>();
    }
}

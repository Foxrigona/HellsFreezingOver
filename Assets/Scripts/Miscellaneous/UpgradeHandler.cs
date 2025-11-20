using UnityEngine;
using UnityEngine.Events;

public class UpgradeHandler : MonoBehaviour
{
    public UnityEvent<int> bulletCountUpgrade;
    public UnityEvent<float> bulletCooldownUpgrade;

    private void Awake()
    {
        bulletCountUpgrade = new UnityEvent<int>();
        bulletCooldownUpgrade = new UnityEvent<float>();
    }
}

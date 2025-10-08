using UnityEngine;

public class SetAim : MonoBehaviour
{
    [SerializeField] private float aimDistance = 2f;
    private void Update()
    {
        this.transform.localPosition = Aimer.getAimDirection(this.transform.parent.position) * this.aimDistance;
    }
}

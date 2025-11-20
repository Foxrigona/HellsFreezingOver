using UnityEngine;

public class NormalHealth :Health
{
    public override void kill()
    {
        Destroy(this.gameObject);
    }
}

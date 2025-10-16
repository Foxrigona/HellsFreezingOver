using UnityEngine;

public class NormalHealth :Health
{
    public override void kill()
    {
        Debug.Log("NORMAL ENEMY DEAD");
        Destroy(this.gameObject);
    }
}

using UnityEngine;

public class WildRogueHealth : Health
{
    override public void kill()
    {
        Debug.Log("GRRRRR");
        Destroy(this.gameObject);
    }
}

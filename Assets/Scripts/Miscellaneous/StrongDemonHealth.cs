using UnityEngine;

public class StrongDemonHealth : Health
{
    [SerializeField] GameObject scroll;
    override public void kill()
    {
        Instantiate(scroll, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

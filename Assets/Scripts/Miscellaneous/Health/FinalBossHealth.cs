using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossHealth : Health
{
    public override void kill()
    {
        SceneManager.LoadScene(3);
    }
}

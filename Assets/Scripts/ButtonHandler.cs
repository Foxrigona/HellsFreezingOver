using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public static void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public static void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void quitGame()
    {
        Application.Quit();
    }

    public void instructions()
    {
        SceneManager.LoadScene(4);
    }
}

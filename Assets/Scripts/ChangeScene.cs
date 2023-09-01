using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    /*
     0 - Main Menu
     1 - Game
     2 - Win Screen
     */
    public void startGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress");

        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress");

        Application.Quit();
    }

    public void mainMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress");

        SceneManager.LoadScene(0);
    }

    public void winScreen()
    {
        SceneManager.LoadScene(2);
    }
}

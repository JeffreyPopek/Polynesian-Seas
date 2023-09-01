using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

  public void Begin(){
    SceneManager.LoadScene("MainScene");
  }

  public void Exit(){
    Application.Quit();
  }

  public void MainMenu(){
    SceneManager.LoadScene("MainMenu");
  }

  public void Controls(){
    SceneManager.LoadScene("ControlsScene");
  }
}

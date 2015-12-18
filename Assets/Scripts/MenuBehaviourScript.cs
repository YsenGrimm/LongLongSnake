using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuBehaviourScript : MonoBehaviour {

    public void StartGame()
    {
		SceneManager.LoadScene ("Game");
    }

    public void QuitGame()
    {

        Application.Quit();
    }
}

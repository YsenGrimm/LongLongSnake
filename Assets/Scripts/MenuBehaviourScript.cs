using UnityEngine;
using System.Collections;

public class MenuBehaviourScript : MonoBehaviour {

    string menuLevelName = "FancyMenu";
    string firstLevelName = "Home";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Application.LoadLevel(firstLevelName);
    }

    public void End()
    {

        Application.Quit();
    }

    public void MainMenu()
    {
        Application.LoadLevel(menuLevelName);
    }

    public void RetryLevel()
    {
        Application.LoadLevel(firstLevelName);
    }
}

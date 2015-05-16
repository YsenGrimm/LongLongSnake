using UnityEngine;
using System.Collections;

public class MenuBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Application.LoadLevel("FancyControls");
    }

    public void End()
    {
        if (Application.isEditor)
            UnityEditor.EditorApplication.Exit(0);

        Application.Quit();
    }
}

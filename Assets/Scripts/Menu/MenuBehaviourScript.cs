using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviourScript : MonoBehaviour 
{

    public ButtonStyler[] AnimatedButtons;

    private int activeButton = 0;
    private int buttonCount = 2;
    private bool buttonUpDown;
    private bool buttonDownDown;

    public void Start() {
        setButtonUpdate(0);
    }

    public void Update() {
        // reset on "button up"
        if (Input.GetAxisRaw("Vertical") >= 0 && buttonDownDown) {
            buttonDownDown = false;
        }

        if (Input.GetAxisRaw("Vertical") <= 0 && buttonUpDown) {
            buttonUpDown = false;
        }

        // up
        if (Input.GetAxisRaw("Vertical") > 0 && !buttonUpDown)
        {
            activeButton = (activeButton + 1) > buttonCount - 1 ? 0 : activeButton + 1;
            setButtonUpdate(activeButton);
            buttonUpDown = true;
        }

        // down
        if (Input.GetAxisRaw("Vertical") < 0 && !buttonDownDown)
        {
            activeButton = (activeButton - 1) < 0 ? buttonCount - 1 : activeButton - 1;
            setButtonUpdate(activeButton);
            buttonDownDown = true;
        }

        if (Input.GetButtonDown("Submit")) {
            switch (activeButton)
            {
                case 0:
                    StartGame();
                    break;
                case 1:
                    QuitGame();
                    break;
            }
        }
    }

    void setButtonUpdate(int buttonIdx) {
        foreach (var button in AnimatedButtons) {
            button.NotActive();
        }

        AnimatedButtons[buttonIdx].Active();
    }

    public void StartGame() {
		SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        Application.Quit();
    }
}

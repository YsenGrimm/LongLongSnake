using UnityEngine;
using UnityEngine.UI;

public class ButtonStyler : MonoBehaviour {

	private Image button;
	private Text text;

	public void Awake() {
		button = GetComponent<Image>();
		text = GetComponentInChildren<Text>();
	}
	
	public void NotActive() {
		button.color = new Color(1, 1, 1, 0.5f);
		text.color = new Color(0, 0, 0, 0.5f);
	}

	public void Active() {
		button.color = new Color(1, 1, 1, 1);
		text.color = new Color(0, 0, 0, 1);
	}
}

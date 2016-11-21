using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

	public List<PlayerArea> playerSpaces;
	public Image greenButton;
	public Image redButton;
	public Text greenText;
	public Text redText;
	bool vipSet = false;

	void Awake(){
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonWasHit;
	}

	public void ButtonWasHit(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.a) {
			if (vipSet) {
				if (playerSpaces [player].isVIP) {
					ScreenHandler.instance.CreateScreen ("menuscreen", true);
				}
			} else {
				vipSet = true;
				playerSpaces [player].ToggleVIP (true);
				redText.gameObject.SetActive (true);
				greenText.text = "Start Game";
				redButton.color = new Color (redButton.color.r, redButton.color.g, redButton.color.b, 1);
			}
		}
		if (button == InputHandler.Buttons.b) {
			if (playerSpaces [player].isVIP) {
				vipSet = false;
				playerSpaces [player].ToggleVIP (false);
				redText.gameObject.SetActive (false);
				greenText.text = "Press GREEN to become the VIP";
				redButton.color = new Color (redButton.color.r, redButton.color.g, redButton.color.b, .5f);
			}
		}
	}
}
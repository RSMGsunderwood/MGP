using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MainMenuScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;				//Reference to player spaces
	public Image yellowButton;							//Reference to yellow button
	public TextMeshProUGUI yellowText;					//Reference to yellow button text
	public ScrollRect menuScroll;						//Reference to the scrolling menu
	public Color selectedText, notSelectedText;			//Colors used for selected and not select text
	public TextMeshProUGUI[] menuTexts;					//Reference to texts in the menu
	int selected = 1;									//Index of menu selection
	//Initializes player spaces, menu selection, and button event subscription
	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerSpaces [i].TogglePlaying (true);
				if (GameHandler.instance.players [i].isVIP) {
					playerSpaces [i].ToggleVIP (true);
				}
			}
		}
		menuTexts [1].color = selectedText;
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}
	//Unsubscribes from button pressed when destroyed
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonWasHit;
	}

	public void Enable(){
		OnEnable ();
	}

	public void Disable(){
		OnDisable ();
	}

	public override void OnBack ()
	{
		
	}
	//When enabled, set as current
	public override void OnEnable()
	{
		gameObject.SetActive (true);
		if(currentScreen&&currentScreen!=this)
			currentScreen.OnDisable();
		currentScreen = this;
	}
	//Sets as inactive on disable
	public override void OnDisable ()
	{
		gameObject.SetActive (false);
	}
	//Button input handler
	public void ButtonWasHit(int player, InputHandler.Buttons button){
		//Goes back to title screen
		if (button == InputHandler.Buttons.y) {
			if (playerSpaces [player].isVIP) {
				ScreenHandler.instance.CreateScreen ("titlescreen", true);
			}
		}
		//Lets VIP move meenu
		if (button == InputHandler.Buttons.b) {
			if (playerSpaces [player].isVIP) {
				if (selected < menuTexts.Length-1) {
					menuTexts [selected].color = notSelectedText;
					selected++;
					menuTexts [selected].color = selectedText;
					menuScroll.horizontalNormalizedPosition = (selected/(float)(menuTexts.Length-1));
				}
			}
		}
		if (button == InputHandler.Buttons.x) {
			if (playerSpaces [player].isVIP) {
				if (selected > 0) {
					menuTexts [selected].color = notSelectedText;
					selected--;
					menuTexts [selected].color = selectedText;
					menuScroll.horizontalNormalizedPosition = (selected/(float)(menuTexts.Length-1));
				}
			}
		}
		//Lets VIP make menu selection
		if (button == InputHandler.Buttons.a) {
			if (playerSpaces [player].isVIP) {
				if (selected == 1) {
					ScreenHandler.instance.CreateScreen ("playscreen", true);
				}
			}
		}
	}
}
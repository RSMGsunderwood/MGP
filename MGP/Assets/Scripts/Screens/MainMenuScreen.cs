using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public Image yellowButton;
	public Text yellowText;
	public ScrollRect menuScroll;
	public Color selectedText, notSelectedText;
	public Text[] menuTexts;
	int selected = 1;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isVIP) {
				playerSpaces [i].ToggleVIP (true);
			}
		}
		menuTexts [1].color = selectedText;
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonWasHit;
	}

	void Update(){
		
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

	public override void OnEnable()
	{
		gameObject.SetActive (true);
		if(currentScreen&&currentScreen!=this)
			currentScreen.OnDisable();
		currentScreen = this;
	}

	public override void OnDisable ()
	{
		gameObject.SetActive (false);
	}

	public void ButtonWasHit(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {
			if (playerSpaces [player].isVIP) {
				ScreenHandler.instance.CreateScreen ("titlescreen", true);
			}
		}
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
		if (button == InputHandler.Buttons.a) {
			if (playerSpaces [player].isVIP) {
				if (selected == 1) {
					ScreenHandler.instance.CreateScreen ("playscreen", true);
				}
			}
		}
	}
}
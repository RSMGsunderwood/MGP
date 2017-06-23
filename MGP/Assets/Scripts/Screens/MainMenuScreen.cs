using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public Image yellowButton;
	public Text yellowText;
	public ScrollRect menuScroll;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isVIP) {
				playerSpaces [i].ToggleVIP (true);
			}
		}
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
}
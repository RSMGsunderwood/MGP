using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameTheJukes : MinigameMain {

	public List<TheJukes_CharControl> charReference = new List<TheJukes_CharControl>();
	public TextMeshProUGUI winnerText;
	List<TheJukes_CharControl> charControls = new List<TheJukes_CharControl>();
	[HideInInspector] int livingPlayers = 0;

	//Initialize game on startup
	void Start(){
		for (int i = 0; i < 4; i++) {
			int normalPlayer = 1;
			if (GameHandler.instance.players [i].isPlaying) {
				if (GameHandler.instance.players [i].isTheEnemy) {
					charControls.Add(charReference[0]);
					charControls [i].isGodzilla = true;
				} else {
					charControls.Add(charReference[normalPlayer]);
					normalPlayer++;
					livingPlayers++;
				}
				charControls [i].SetColor (GameHandler.instance.players [i].playerColor);
			}
		}
		MiniGameScreen.TimeOut += this.TimeOut;
		InputHandler.ButtonPressed += this.ButtonPress;
		InputHandler.ButtonReleased += this.ButtonRelease;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		MiniGameScreen.TimeOut -= this.TimeOut;
		InputHandler.ButtonPressed -= this.ButtonPress;
		InputHandler.ButtonReleased -= this.ButtonRelease;
	}

	public void PlayerKilled(){
		livingPlayers--;
		if (livingPlayers <= 0) {
			string winningPlayer = "";
			for (int i = 0; i < 4; i++) {
				charControls [i].Kill ();
				if (charControls [i].isGodzilla) {
					winningPlayer = GameHandler.instance.players [i].playerName;
				}
			}
			winnerText.text = winningPlayer + " Wins!";
			StartCoroutine ("WinningRoutine");
		}
	}

	public void TimeOut(){
		string winningPlayers = "";
		for (int i = 0; i < 4; i++) {
			charControls [i].Kill ();
			if (!charControls [i].isGodzilla) {
				winningPlayers += GameHandler.instance.players [i].playerName + "  ";
			}
		}
		winnerText.text = winningPlayers + "Wins!";
		StartCoroutine ("WinningRoutine");
	}

	IEnumerator WinningRoutine(){
		StartCoroutine ("PulseText");
		yield return new WaitForSeconds (5.0f);
	}

	IEnumerator PulseText(){
		RectTransform rect = winnerText.GetComponent<RectTransform> ();
		float tween = 0;
		for (float i = 0; i < 10; i += Time.deltaTime) {
			tween = ((Mathf.Sin (Time.time) + 1) / 8) + (7f/8f);
			rect.localScale = new Vector3 (tween, tween, tween);
			yield return null;
		}
	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {
			charControls[player].PlayerInput(new Vector2(0,100));
		}
		if (button == InputHandler.Buttons.b) {
			charControls[player].PlayerInput(new Vector2(100,0));
		}
		if (button == InputHandler.Buttons.x) {
			charControls[player].PlayerInput(new Vector2(-100,0));
		}
		if (button == InputHandler.Buttons.a) {
			charControls[player].PlayerInput(new Vector2(0,-100));
		}
	}

	//Button release override
	public override void ButtonRelease(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {
			charControls[player].PlayerInput(new Vector2(0,-100));
		}
		if (button == InputHandler.Buttons.b) {
			charControls[player].PlayerInput(new Vector2(-100,0));
		}
		if (button == InputHandler.Buttons.x) {
			charControls[player].PlayerInput(new Vector2(100,0));
		}
		if (button == InputHandler.Buttons.a) {
			charControls[player].PlayerInput(new Vector2(0,100));
		}
	}
}

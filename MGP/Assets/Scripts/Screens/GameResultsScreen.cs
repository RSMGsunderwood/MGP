using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultsScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public TextMeshProUGUI titleText;
	public List<GameObject> playerResults;
	public List<TextMeshProUGUI> playerScores;
	public List<TextMeshProUGUI> playerTimes;
	public TextMeshProUGUI greenBText;
	public TextMeshProUGUI winningPlayer;
	bool resultsDone = false;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerSpaces [i].TogglePlaying (true);
				if (GameHandler.instance.players [i].isVIP) {
					playerSpaces [i].ToggleVIP (true);
				}
				playerSpaces [i].SetColor (GameHandler.instance.players [i].playerColor);
				playerSpaces [i].playerName.text = GameHandler.instance.players [i].playerName;
			} else {
				playerSpaces [i].playerName.text = "";
			}
		}
		StartCoroutine ("ResultsDisplay");
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

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
		}
		if (button == InputHandler.Buttons.b) {
		}
		if (button == InputHandler.Buttons.x) {
		}
		if (button == InputHandler.Buttons.a) {
			if (!resultsDone) {
				SkipResultsDisplay ();
			} else {
				ScreenHandler.instance.CreateScreen ("playscreen", true);
			}
		}
	}

	IEnumerator ResultsDisplay(){
		yield return new WaitForSeconds (3.0f);
		titleText.text = "Results:";
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerScores [i].text = GameHandler.instance.players [i].pointScore.ToString();
				playerTimes [i].text = GameHandler.instance.players [i].timeScore.ToString("F2");
				playerResults [i].SetActive (true);
				yield return new WaitForSeconds (1.0f);
			}
			yield return null;
		}
		FinishResults ();
	}

	public void SkipResultsDisplay(){
		StopCoroutine ("ResultsDisplay");
		titleText.text = "Results:";
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerScores [i].text = GameHandler.instance.players [i].pointScore.ToString ();
				playerTimes [i].text = GameHandler.instance.players [i].timeScore.ToString ("F2");
				playerResults [i].SetActive (true);
			}
		}
		FinishResults ();
	}

	void FinishResults(){
		resultsDone = true;
		if (GameHandler.instance.winningPlayer != null) {
			winningPlayer.text = GameHandler.instance.winningPlayer.playerName + " Wins!";
		} else {
			winningPlayer.text = "Draw!";
		}
		winningPlayer.gameObject.SetActive (true);
		greenBText.text = "Back To Menu";
	}
}
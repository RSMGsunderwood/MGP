using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultsScreen : BaseScreen {

	public TextMeshProUGUI titleText;				//Reference to title text
	public List<GameObject> playerResults;			//Reference to player result holders
	public List<TextMeshProUGUI> playerScores;		//Reference to player score texts
	public List<TextMeshProUGUI> playerTimes;		//Reference to player time texts
	public TextMeshProUGUI greenBText;				//Reference to green button text
	public TextMeshProUGUI winningPlayer;			//Reference to winning player texts
	bool resultsDone = false;

	//Initializes player area and start result coroutine
	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				GameHandler.instance.playerSpaces [i].TogglePlaying (true, false);
				if (GameHandler.instance.players [i].isVIP) {
					GameHandler.instance.playerSpaces [i].ToggleVIP (true);
				}
				GameHandler.instance.playerSpaces [i].SetColor (GameHandler.instance.players [i].playerColor);
				GameHandler.instance.playerSpaces [i].playerName.text = GameHandler.instance.players [i].playerName;
			} else {
				GameHandler.instance.playerSpaces [i].playerName.text = "";
			}
		}
		StartCoroutine ("ResultsDisplay");
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}
	//Unsubscribes from button press event when destroyed
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
		if (button == InputHandler.Buttons.y) {
		}
		if (button == InputHandler.Buttons.b) {
		}
		if (button == InputHandler.Buttons.x) {
		}
		if (button == InputHandler.Buttons.a) {
			//Skips to end of results if those are still going.  Otherwise shoots players back to play menu
			if (GameHandler.instance.players [player].isVIP) {
				if (!resultsDone) {
					SkipResultsDisplay ();
				} else {
					ScreenHandler.instance.CreateScreen ("playscreen", true);
				}
			}
		}
	}
	//Coroutine for displaying results.  Goes through player scores then announces winner
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
	//Stops the result display coroutine and displays all the information instantly
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
	//Shows what player wins or if no one wins.  Then changes "A" button to going back to the play menu
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
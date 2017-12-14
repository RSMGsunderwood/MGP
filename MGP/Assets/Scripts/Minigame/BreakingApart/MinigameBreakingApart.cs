using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBreakingApart : MinigameMain {

	public Color rCol, bCol, yCol, gCol;
	public List<BreakingApartPlayerZone> playerZones;
	public List<GameObject> playerLabels;
	public bool roundInProgress = false;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerLabels [i].SetActive (true);
				playerZones [i].gameObject.SetActive (true);
				playerZones [i].SetStrings ();
				playerZones [i].mGame = this;
			}
		}
		StartCoroutine ("GameRoutine");
		InputHandler.ButtonPressed += this.ButtonPress;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	IEnumerator GameRoutine(){
		#region Round 1
		yield return new WaitForSeconds (.5f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.currentOrder = 0;
			z.finished = false;
			z.currentRound = z.round1S;
			if(z.gameObject.activeInHierarchy)
				z.StartCoroutine(z.StartRound());
		}
		yield return new WaitForSeconds (5f);
		roundInProgress = false;
		foreach (BreakingApartPlayerZone z in playerZones) {
			if(z.gameObject.activeInHierarchy)
				z.StartCoroutine(z.EndRound());
		}
		#endregion

		#region Round 2
		yield return new WaitForSeconds (2f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.currentOrder = 0;
			z.finished = false;
			z.currentRound = z.round2S;
			if(z.gameObject.activeInHierarchy)
				z.StartCoroutine(z.StartRound());
		}
		yield return new WaitForSeconds (7f);
		roundInProgress = false;
		foreach (BreakingApartPlayerZone z in playerZones) {
			if(z.gameObject.activeInHierarchy)
				z.StartCoroutine(z.EndRound());
		}
		#endregion

		#region Round 3
		yield return new WaitForSeconds (2f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.currentOrder = 0;
			z.finished = false;
			z.currentRound = z.round3S;
			if(z.gameObject.activeInHierarchy)
				z.StartCoroutine(z.StartRound());
		}
		yield return new WaitForSeconds (6f);
		roundInProgress = false;
		foreach (BreakingApartPlayerZone z in playerZones) {
			if(z.gameObject.activeInHierarchy)
				z.StartCoroutine(z.EndRound());
		}
		#endregion
		yield return new WaitForSeconds (2f);
		GameHandler.instance.CalculateWinner ();
		ScreenHandler.instance.CreateScreen ("resultsscreen", true);
	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		if (roundInProgress) {
			string bInput = button.ToString ().ToLower ();
			if (playerZones [player].currentOrder < playerZones [player].currentRound.Count) {
				if (playerZones [player].buttons [playerZones [player].currentOrder].CheckButton (bInput)) {
					playerZones [player].ButtonPressed (true);
					GameHandler.instance.players [player].pointScore++;
				} else {
					playerZones [player].ButtonPressed (false);
					playerZones [player].currentOrder = 99;
					GameHandler.instance.players [player].pointScore--;
				}
			} else if(!playerZones [player].finished) {
				GameHandler.instance.players [player].pointScore--;
			}
		}
	}

	public override void ButtonRelease(int player, InputHandler.Buttons button){
	}

}
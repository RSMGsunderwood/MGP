using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBreakingApart : MinigameMain {

	public Color rCol, bCol, yCol, gCol;
	public List<BreakingApartPlayerZone> playerZones;
	bool roundInProgress = false;

	void Awake(){
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.SetStrings ();
			z.mGame = this;
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
		yield return new WaitForSeconds (.25f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.StartCoroutine(z.MoveCurtain(false));
		}
		yield return new WaitForSeconds (.5f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.currentOrder = 0;
			z.finished = false;
			z.currentRound = z.round1S;
			z.StartCoroutine(z.StartRound());
		}
		roundInProgress = true;
		yield return new WaitForSeconds (5f);
		roundInProgress = false;
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.StartCoroutine(z.EndRound());
		}
		#endregion

		#region Round 2
		yield return new WaitForSeconds (2f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.StartCoroutine(z.MoveCurtain(false));
		}
		yield return new WaitForSeconds (.5f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.currentOrder = 0;
			z.finished = false;
			z.currentRound = z.round2S;
			z.StartCoroutine(z.StartRound());
		}
		roundInProgress = true;
		yield return new WaitForSeconds (7f);
		roundInProgress = false;
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.StartCoroutine(z.EndRound());
		}
		#endregion

		#region Round 3
		yield return new WaitForSeconds (2f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.StartCoroutine(z.MoveCurtain(false));
		}
		yield return new WaitForSeconds (.5f);
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.currentOrder = 0;
			z.finished = false;
			z.currentRound = z.round3S;
			z.StartCoroutine(z.StartRound());
		}
		roundInProgress = true;
		yield return new WaitForSeconds (6f);
		roundInProgress = false;
		foreach (BreakingApartPlayerZone z in playerZones) {
			z.StartCoroutine(z.EndRound());
		}
		#endregion
	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		if (roundInProgress) {
			string bInput = button.ToString ().ToLower ();
			if (playerZones [player].currentOrder < playerZones [player].currentRound.Count) {
				if (playerZones [player].buttons [playerZones [player].currentOrder].CheckButton (bInput)) {
					playerZones [player].currentOrder++;
					GameHandler.instance.players [player].pointScore++;
					if (playerZones [player].currentOrder >= playerZones [player].currentRound.Count) {
						playerZones [player].finished = true;

					}
				} else {
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
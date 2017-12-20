using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameRhymeRhyme : MinigameMain {

	public List<RhymeRhymePlayerHUDS> playerHUDS = new List<RhymeRhymePlayerHUDS>();
	public Transform timerPrompt;
	public Color rCol, bCol, yCol, gCol;
	public float tempo;
	public float speed;
	float spawnAngle = 0;
	public Animation ani;

	public Transform buttonPromptHolder;

	public Stack<RhymeRyhmeInputDisplay> buttonPrompts = new Stack<RhymeRyhmeInputDisplay>();

	public RhymeRyhmeInputDisplay buttonBeingSucked = null;

	void Awake(){
		foreach (Transform child in buttonPromptHolder) {
			buttonPrompts.Push(child.GetComponent<RhymeRyhmeInputDisplay>());
			child.GetComponent<RhymeRyhmeInputDisplay> ().SetTran ();
			child.gameObject.SetActive(false);
		}
		for (int i = 0; i < playerHUDS.Count; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerHUDS [i].playerName.text = GameHandler.instance.players [i].playerName;
				playerHUDS [i].gameObject.SetActive (true);
			}
		}
		ani ["Song1"].speed = (1f / 900f)*tempo;
		ani.Play ();
		InputHandler.ButtonPressed += this.ButtonPress;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	public void Finished(){
		GameHandler.instance.CalculateWinner ();
		ScreenHandler.instance.CreateScreen ("resultsscreen", true);
	}

	public void ButtonTrigger(string s){
		//parse color
		Color bColor = Color.black;
		switch (s) {
		case("a"):
			bColor = gCol;
			break;
		case("b"):
			bColor = rCol;
			break;
		case("y"):
			bColor = yCol;
			break;
		case("x"):
			bColor = bCol;
			break;
		default:
			bColor = Color.black;
			break;
		}

		Vector3 spawnVec = Vector3.zero;

		spawnAngle += Random.Range (30, 330);

		if (spawnAngle >= 360)
			spawnAngle -= 360;


		spawnVec = new Vector3(Mathf.Cos(spawnAngle)*500,Mathf.Sin(spawnAngle)*500,0);

		RhymeRyhmeInputDisplay button = buttonPrompts.Pop ();
		button.SetButton (bColor, s, spawnVec);
	}

	public void ShowTime(){
		StopCoroutine ("ShowTimePrompt");
		StartCoroutine ("ShowTimePrompt");
	}

	IEnumerator ShowTimePrompt(){
		timerPrompt.localScale = new Vector3 (2, 2, 2);
		timerPrompt.gameObject.SetActive (true);
		float temp = 0;
		for (float i = 0; i < .65f; i += Time.deltaTime) {
			temp = Mathf.Lerp (2, 1, i / .65f);
			timerPrompt.localScale = new Vector3 (temp, temp, temp);
			yield return null;
		}
		timerPrompt.localScale = new Vector3 (.65f, .65f, .65f);
		timerPrompt.gameObject.SetActive (false);
	}

	public override void ButtonPress(int player, InputHandler.Buttons button){
		bool pass = false;
		if (buttonBeingSucked != null) {
			if (GameHandler.instance.players [player].isPlaying && !playerHUDS [player].finishedInput) {
				if (buttonBeingSucked.buttonText.text.ToLower () == button.ToString ().ToLower ()) {
					pass = true;
				} else {
					pass = false;
				}
				if (pass) {
					playerHUDS [player].ShowFeedback (buttonBeingSucked.buttonImage.color, Color.black, buttonBeingSucked.buttonText.text);
					GameHandler.instance.players [player].pointScore++;
				} else {
					playerHUDS [player].ShowFeedback (Color.white, Color.red, "X");
				}
			}
		}
	}

	public override void ButtonRelease(int player, InputHandler.Buttons button){
		
	}

}
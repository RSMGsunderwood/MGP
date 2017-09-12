using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameScreen : BaseScreen {

	public RectTransform textTrans;									//Reference to text which animates early
	public TextMeshProUGUI mName;									//Reference to minigame title text
	public TextMeshProUGUI mRules;									//Reference to minigame rules text
	public GameObject descriptionPopup;								//Reference to description popup gameobject
	public TextMeshProUGUI descriptionText;							//Reference to minigame description text
	public TextMeshProUGUI countdownText;							//Reference to countdown text
	public TextMeshProUGUI timerText;								//Reference to minigame timer text
	public List<GameObject> buttons = new List<GameObject>();		//Reference to button gameobject
	public Slider timerUI;											//Reference to minigame timer slider
	bool gameStarted = false;										//Bool that switches when minigame begins
	public delegate void TimeAction();
	public static event TimeAction TimeOut;
	//Initializes player spaces and pulls minigame metadata from the metadata object
	void Awake(){
		StartCoroutine ("TitleTween");
		for (int i = 0; i < 4; i++) {
			GameHandler.instance.players [i].pointScore = 0;
			GameHandler.instance.players [i].timeScore = 00.00f;
			if (GameHandler.instance.players [i].isPlaying) {
				GameHandler.instance.playerSpaces [i].TogglePlaying (true,false);
				if (GameHandler.instance.players [i].isVIP) {
					GameHandler.instance.playerSpaces [i].ToggleVIP (true);
				}
				GameHandler.instance.playerSpaces [i].SetColor (GameHandler.instance.players [i].playerColor);
				GameHandler.instance.playerSpaces [i].playerName.text = GameHandler.instance.players [i].playerName;
			} else {
				GameHandler.instance.playerSpaces [i].playerName.text = "";
			}
		}
		mName.text = GameHandler.instance.chosenGame.name;
		string temp = GameHandler.instance.chosenGame.rulesDescription;
		temp = temp.Replace("\\n","\n");
		mRules.text = temp;
		temp = GameHandler.instance.chosenGame.extDescription;
		temp = temp.Replace("\\n","\n");
		descriptionText.text = temp;
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
		//Hides popup if any button is pressed while it's active
		if (descriptionPopup.activeInHierarchy) {
			if (GameHandler.instance.players [player].isVIP && !gameStarted) {
				descriptionPopup.SetActive (false);
			}
		} else {
			if (button == InputHandler.Buttons.y) {
			}
			if (button == InputHandler.Buttons.b) {
			}
			//VIP can show description popup
			if (button == InputHandler.Buttons.x) {
				if (GameHandler.instance.players [player].isVIP && !gameStarted) {
					descriptionPopup.SetActive (true);
				}
			}
			//VIP can start game
			if (button == InputHandler.Buttons.a) {
				if (GameHandler.instance.players [player].isVIP && !gameStarted) {
					gameStarted = true;
					textTrans.gameObject.SetActive (false);
					if (GameHandler.instance.chosenGame.hidePlayerAreas) {
						GameHandler.instance.StartCoroutine(GameHandler.instance.areaAnimate(true));
					}
					StartCoroutine ("CountDownStart");
				}
			}
		}
	}
	//Countdown coroutine.  Starts game after and shows timer if metadata says it should.
	IEnumerator CountDownStart(){
		for (float x = 3; x > -1; x--) {
			for (float i = 0; i < 1; i += Time.deltaTime) {
				if (x != 0) {
					countdownText.text = x.ToString ();
				} else {
					countdownText.text = "Go!";
				}
				yield return null;
			}
		}
		countdownText.text = "";
		GameHandler.instance.chosenGameGO = Instantiate (GameHandler.instance.chosenGame.mPrefab);
		foreach (GameObject temp in buttons) {
			temp.SetActive (false);
		}
		if (GameHandler.instance.chosenGame.usesTimer) {
			GameHandler.instance.timer = GameHandler.instance.chosenGame.timer;
			timerUI.gameObject.SetActive (GameHandler.instance.chosenGame.visibleTimer);
			timerText.gameObject.SetActive (GameHandler.instance.chosenGame.visibleTimer);
			StartCoroutine ("TimerRoutine");
		}
	}
	//Animates the title to show rules
	IEnumerator TitleTween(){
		yield return new WaitForSeconds (5.0f);
		float h1 = textTrans.anchoredPosition.y + 65;
		for (float i = 0; i < 2; i += Time.deltaTime) {
			float tween = Mathf.Lerp (textTrans.anchoredPosition.y, h1, i / 2);
			textTrans.anchoredPosition = new Vector2 (textTrans.anchoredPosition.x, tween);
			tween = Mathf.Lerp (0, 1, i / 2);
			mRules.color = new Color (0, 0, 0, tween);
			yield return null;
		}
	}
	//Timer runs while minigame is active
	IEnumerator TimerRoutine(){
		for (float i = GameHandler.instance.chosenGame.timer; i > 0; i -= Time.deltaTime) {
			if (GameHandler.instance.chosenGame.visibleTimer) {
				timerUI.value = i / GameHandler.instance.chosenGame.timer;
				timerText.text = i.ToString ("F2");
			}
			GameHandler.instance.timer = GameHandler.instance.chosenGame.timer - i;
			yield return null;
		}
		timerText.text = "00.00";
		GameHandler.instance.CalculateWinner ();
		if (TimeOut != null) {
			MiniGameScreen.TimeOut ();
		}
		//ScreenHandler.instance.CreateScreen ("resultsscreen", true);

	}
}

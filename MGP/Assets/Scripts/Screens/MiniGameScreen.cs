using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public RectTransform textTrans;
	public TextMeshProUGUI mName;
	public TextMeshProUGUI mRules;
	public GameObject descriptionPopup;
	public TextMeshProUGUI descriptionText;
	public TextMeshProUGUI countdownText;
	public TextMeshProUGUI timerText;
	public List<GameObject> buttons = new List<GameObject>();
	public Slider timerUI;
	bool gameStarted = false;

	void Awake(){
		StartCoroutine ("TitleTween");
		for (int i = 0; i < 4; i++) {
			GameHandler.instance.players [i].pointScore = 0;
			GameHandler.instance.players [i].timeScore = 00.00f;
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
		mName.text = GameHandler.instance.chosenGame.name;
		string temp = GameHandler.instance.chosenGame.rulesDescription;
		temp = temp.Replace("\\n","\n");
		mRules.text = temp;
		temp = GameHandler.instance.chosenGame.extDescription;
		temp = temp.Replace("\\n","\n");
		descriptionText.text = temp;
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
		if (descriptionPopup.activeInHierarchy) {
			if (GameHandler.instance.players [player].isVIP && !gameStarted) {
				descriptionPopup.SetActive (false);
			}
		} else {
			if (button == InputHandler.Buttons.y) {
			}
			if (button == InputHandler.Buttons.b) {
			}
			if (button == InputHandler.Buttons.x) {
				if (GameHandler.instance.players [player].isVIP && !gameStarted) {
					descriptionPopup.SetActive (true);
				}
			}
			if (button == InputHandler.Buttons.a) {
				if (GameHandler.instance.players [player].isVIP && !gameStarted) {
					gameStarted = true;
					textTrans.gameObject.SetActive (false);
					StartCoroutine ("CountDownStart");
				}
			}
		}
	}

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
		ScreenHandler.instance.CreateScreen ("resultsscreen", true);
	}
}

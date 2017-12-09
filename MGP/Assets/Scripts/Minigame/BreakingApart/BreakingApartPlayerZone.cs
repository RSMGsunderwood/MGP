using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreakingApartPlayerZone : MonoBehaviour {

	public RectTransform curtain;
	public RectTransform zoomOb;
	public RectTransform logLeft;
	public RectTransform logRight;
	public AnimationCurve logAnimCurve;
	public List<BreakingApartInputButton> buttons;
	[HideInInspector] public int currentOrder;
	[HideInInspector] public MinigameBreakingApart mGame;
	[HideInInspector] public List<string> currentRound;
	[HideInInspector] public List<string> round1S;
	[HideInInspector] public List<string> round2S;
	[HideInInspector] public List<string> round3S;
	[HideInInspector] public bool finished = false;

	public void SetStrings(){
		//Set first round order: 5 buttons
		//First add (x) amount of button "A", then the rest will be button "Y".
		int amountS = Random.Range(1,5);
		for (int i = 0; i < amountS; i++) {
			round1S.Add ("a");
		}
		//Now we add the inverse amount of "Y" to add up to 5 buttons.
		amountS=5-amountS;
		for (int i = 0; i < amountS; i++) {
			round1S.Add ("y");
		}

		//Set second round order: 7 buttons
		//First add (x) amount of buttons "A", leaving room for at least 1 of each other button.
		amountS = Random.Range(1,6);
		for (int i = 0; i < amountS; i++) {
			round2S.Add ("a");
		}
		//Now we add amount of "Y", leaving at least room for at least 1 of "B".
		amountS = Random.Range (1, (7 - amountS));
		for (int i = 0; i < amountS; i++) {
			round2S.Add ("y");
		}
		//Remaining amount left is added as "B"
		amountS = 7-round2S.Count;
		for (int i = 0; i < amountS; i++) {
			round2S.Add ("b");
		}

		//Set third round order: 7 buttons
		//Same as round 2 but now we need to compensate for a fourth button.
		amountS = Random.Range(1,5);
		for (int i = 0; i < amountS; i++) {
			round3S.Add ("a");
		}
		//Now we add amount of "Y", leaving at least room for at least 1 of "B" and 1 of "X".
		amountS = Random.Range (1, (7 - (round3S.Count+1)));
		for (int i = 0; i < amountS; i++) {
			round3S.Add ("y");
		}
		//Add amount of "B", leaving room for "X".
		amountS = Random.Range (1, (7 - (round3S.Count+1)));
		for (int i = 0; i < amountS; i++) {
			round3S.Add ("b");
		}
		//Rest is added as "X".
		amountS = 7-round2S.Count;
		for (int i = 0; i < amountS; i++) {
			round3S.Add ("x");
		}
		//Now let's shuffle these lists up!
		//Using Fisher Yates shuffle
		for(int i=0;i<3;i++){
			List<string> aList;
			if (i == 0) {
				aList = round1S;
			}else if(i==1){
				aList = round2S;
			}else{
				aList = round3S;
			}

			System.Random _random = new System.Random ();

			string myS;

			int n = aList.Count;
			for (int x = 0; x < n; x++)
			{
				int r = x + (int)(_random.NextDouble() * (n - x));
				myS = aList[r];
				aList[r] = aList[i];
				aList[i] = myS;
			}
			//Now we assign the shuffle to the original list, and we're done!
			if (i == 0) {
				round1S = aList;
			}else if(i==1){
				round2S = aList;
			}else{
				round3S = aList;
			}
		}
	}

	public IEnumerator StartRound(){
		float lerpTo = 1.4f;
		float lerpTemp = 1f;
		for (float x = 0; x < .5f; x += Time.deltaTime) {
			lerpTemp = Mathf.SmoothStep (0, 250, x / .5f);
			curtain.localPosition = new Vector3 (0, lerpTemp, 0);
			yield return null;
		}
		curtain.localPosition = new Vector3 (0, 250f, 0);
		curtain.gameObject.SetActive (false);
		lerpTo = 1.4f;
		yield return new WaitForSeconds (1f);
		for (float x = 0; x < .5f; x += Time.deltaTime) {
			lerpTemp = Mathf.SmoothStep (1f, lerpTo, x / .5f);
			zoomOb.localScale = new Vector3(lerpTemp, lerpTemp, lerpTemp);
			yield return null;
		}
		zoomOb.localScale = new Vector3(lerpTo, lerpTo, lerpTo);
		lerpTemp = 0;
		for (int i = 0; i < currentRound.Count; i++) {
			buttons [i].buttonImage.color = new Color (buttons [i].buttonImage.color.r, buttons [i].buttonImage.color.g, buttons [i].buttonImage.color.b, 0);
			buttons [i].buttonText.color = new Color (buttons [i].buttonText.color.r, buttons [i].buttonText.color.g, buttons [i].buttonText.color.b, 0);
			buttons [i].buttonImage.gameObject.SetActive (true);
			buttons [i].buttonText.gameObject.SetActive (true);
			SetButton (buttons[i], round1S [i].ToLower());
			for (float x = 0; x < .2f; x += Time.deltaTime) {
				lerpTemp = Mathf.SmoothStep (0, 1, x / .2f);
				buttons [i].buttonImage.color = new Color (buttons [i].buttonImage.color.r, buttons [i].buttonImage.color.g, buttons [i].buttonImage.color.b, lerpTemp);
				buttons [i].buttonText.color = new Color (buttons [i].buttonText.color.r, buttons [i].buttonText.color.g, buttons [i].buttonText.color.b, lerpTemp);
				yield return null;
			}
			buttons [i].buttonImage.color = new Color (buttons [i].buttonImage.color.r, buttons [i].buttonImage.color.g, buttons [i].buttonImage.color.b, 1);
			buttons [i].buttonText.color = new Color (buttons [i].buttonText.color.r, buttons [i].buttonText.color.g, buttons [i].buttonText.color.b, 1);
		}
	}

	public IEnumerator SplitLog(){
		float tempLerp = 1f;
		for (float x = 0; x < 1f; x += Time.deltaTime) {
			tempLerp = Mathf.Lerp(0,90, logAnimCurve.Evaluate (Mathf.Lerp (0, 1, x / 1f)));
			logLeft.localEulerAngles = new Vector3 (0, 0, tempLerp);
			logRight.localEulerAngles = new Vector3 (0, 0, -tempLerp);
			yield return null;
		}
		logLeft.localEulerAngles = new Vector3 (0, 0, 90);
		logRight.localEulerAngles = new Vector3 (0, 0, -90);
	}

	public IEnumerator EndRound(){
		curtain.gameObject.SetActive (true);
		float lerpTemp = 1f;
		for (float x = 0; x < .5f; x += Time.deltaTime) {
			lerpTemp = Mathf.SmoothStep (250, 0, x / .5f);
			curtain.localPosition = new Vector3 (0, lerpTemp, 0);
			yield return null;
		}
		curtain.localPosition = Vector3.zero;
		for (int i = 0; i < buttons.Count; i++) {
			buttons [i].buttonImage.color = new Color (buttons [i].buttonImage.color.r, buttons [i].buttonImage.color.g, buttons [i].buttonImage.color.b, 0);
			buttons [i].buttonText.color = new Color (buttons [i].buttonText.color.r, buttons [i].buttonText.color.g, buttons [i].buttonText.color.b, 0);
		}
		zoomOb.localScale = Vector3.one;
	}

	void SetButton(BreakingApartInputButton button, string b){
		string bText = b.ToUpper();
		Color bColor = Color.red;
		switch (b) {
			case("a"):
				bColor = mGame.gCol;
				break;
			case("b"):
				bColor = mGame.rCol;
				break;
			case("y"):
				bColor = mGame.yCol;
				break;
			case("x"):
				bColor = mGame.bCol;
				break;
			default:
				bColor = Color.black;
				break;
		}
		button.SetButton (bText, bColor);
	}

	public IEnumerator MoveCurtain(bool down){
		float yTween = 250;
		float tweenTo = 250;
		if (down) {
			tweenTo = 0;
		}
		for (float x = 0; x < .5f; x += Time.deltaTime) {
			yTween = Mathf.Lerp (curtain.anchoredPosition.y, tweenTo, x / .5f);
			curtain.anchoredPosition = new Vector2 (curtain.anchoredPosition.x, yTween);
			yield return null;
		}
		curtain.anchoredPosition = new Vector2 (curtain.anchoredPosition.x, tweenTo);
	}

	public void ButtonPressed(bool correct){
		if (correct) {
			StartCoroutine (ShowCorrect (buttons [currentOrder].buttonRight.gameObject));
			currentOrder++;
			if (currentOrder >= currentRound.Count) {
				finished = true;
			}
		} else {
			StartCoroutine (ShowCorrect (buttons [currentOrder].buttonWrong.gameObject));
		}
	}

	public IEnumerator ShowCorrect(GameObject GO){
		GO.gameObject.SetActive (true);
		yield return new WaitForSeconds (1f);
		GO.gameObject.SetActive (false);
	}

}

[System.Serializable]
public class BreakingApartInputButton{

	public Image buttonImage;
	public Image buttonRight;
	public Image buttonWrong;
	public TextMeshProUGUI buttonText;

	public void SetButton(string buttonT, Color buttonC){
		buttonImage.color = buttonC;
		buttonText.text = buttonT;
	}

	public bool CheckButton(string buttonP){
		return buttonText.text.ToLower () == buttonP.ToLower ();
	}

}
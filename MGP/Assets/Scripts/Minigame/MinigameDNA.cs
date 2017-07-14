using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameDNA : MinigameMain {

	public Transform textHolder;
	public List<RectTransform> pIndicators = new List<RectTransform>();
	List<TextMeshProUGUI> dnaString = new List<TextMeshProUGUI>();
	string playString;
	List<int> pProgress = new List<int>();

	void Awake(){
		playString = "";
		string[] dna = new string [] {"CG","GC","AT","TA"};
		List<int> rChoices = new List<int> ();
		List<int> choices = new List<int> ();
		for (int i = 0; i < 4; i++) {
			rChoices.Add (i);
			choices.Add (0);
			pProgress.Add (0);
			pIndicators [i].GetComponent<Image> ().color = GameHandler.instance.players [i].playerColor;
		}
		for (int i = 0; i < 20; i++) {
			dnaString.Add(textHolder.GetChild(i*2).GetComponent<TextMeshProUGUI>());
			dnaString.Add(textHolder.GetChild((i*2)+1).GetComponent<TextMeshProUGUI>());
			int m = Random.Range (0, rChoices.Count);
			playString += dna [rChoices[m]];
			dnaString [playString.Length - 2].text = playString [playString.Length - 2].ToString();
			dnaString [playString.Length - 1].text = playString [playString.Length - 1].ToString();
			choices [m]++;
			if (choices [m] == 5) {
				rChoices.Remove (m);
				choices.Remove (m);
			}
		}
		InputHandler.ButtonPressed += this.ButtonPress;
	}

	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	public override void OnWin ()
	{
		
	}

	public override void ButtonPress(int player, InputHandler.Buttons button){
		bool passed = false;
		//G
		if (button == InputHandler.Buttons.y) {
			if (playString[pProgress [player]].ToString() == "G") {
				passed = true;
			}
		}
		//C
		if (button == InputHandler.Buttons.b) {
			if (playString[pProgress [player]].ToString() == "C") {
				passed = true;
			}
		}
		//A
		if (button == InputHandler.Buttons.x) {
			if (playString[pProgress [player]].ToString() == "A") {
				passed = true;
			}
		}
		//T
		if (button == InputHandler.Buttons.a) {
			if (playString[pProgress [player]].ToString() == "T") {
				passed = true;
			}
		}
		if (passed) {
			pProgress [player]++;
			pIndicators [player].anchoredPosition = new Vector2(dnaString [pProgress[player]].GetComponent<RectTransform>().anchoredPosition.x, dnaString [pProgress[player]].GetComponent<RectTransform>().anchoredPosition.y -15-(2*player) );
		}
	}
}
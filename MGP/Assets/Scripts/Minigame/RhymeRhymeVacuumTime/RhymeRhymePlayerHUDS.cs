using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhymeRhymePlayerHUDS : MonoBehaviour {

	public TextMeshProUGUI playerName;
	public Image buttonColor;
	public TextMeshProUGUI buttonText;
	public bool finishedInput;

	public void ShowFeedback(Color bColor, Color tColor, string s){
		finishedInput = true;
		buttonColor.color = bColor;
		buttonText.color = tColor;
		buttonText.text = s;
	}

	public void TurnOffFeedback(){
		finishedInput = false;
		buttonColor.color = Color.white;
		buttonText.text = "";
		buttonText.color = Color.black;
	}
}
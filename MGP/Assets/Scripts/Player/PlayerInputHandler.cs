using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour {

	List<GameObject> textOptions = new List<GameObject>();
	List<GameObject> colorOptions = new List<GameObject>();
	public GameObject playerNameInputGO, playerColorGO;
	public Text nameInput;
	string playerName = "";
	public RectTransform textOptionsHolder, colorOptionsHolder;
	public Color selectedColor, normalColor;
	GameObject selectedTextOption, selectedColorOption;
	float textStartx = 0, textEndX = 0, colorStartx = 0, colorEndx = 0;

	void Awake(){
		//Setup for text input infinite scrolling
		float tempPos = textStartx= -300;
		foreach (Transform child in textOptionsHolder) {
			RectTransform temp = child.GetComponent<RectTransform> ();
			textOptions.Add (child.gameObject);
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x + tempPos, temp.anchoredPosition.y);
			tempPos += 20;
		}
		textEndX = tempPos -20;
		selectedTextOption = textOptions [15];
		textOptions[15].GetComponent<Text> ().color = selectedColor;
		//Setup for color input infinite scrolling
		tempPos = colorStartx = -140;
		foreach (Transform child in colorOptionsHolder) {
			RectTransform temp = child.GetComponent<RectTransform> ();
			colorOptions.Add (child.gameObject);
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x + tempPos, temp.anchoredPosition.y);
			tempPos += 40;
		}
		colorEndx = tempPos - 40;
		playerColorGO.SetActive (false);
	}

	public void ScrollTextLeft(){
		//If we're in the name input area, scroll text items to the left
		if (playerNameInputGO.activeInHierarchy) {
			foreach (GameObject ob in textOptions) {
				RectTransform temp = ob.GetComponent<RectTransform> ();
				temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x - 20, temp.anchoredPosition.y);
				if (ob.GetComponent<Text> () != null) {
					ob.GetComponent<Text> ().color = normalColor;
				} else {
					ob.GetComponent<Image> ().color = normalColor;
				}
			}
			textOptions [0].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (textEndX, textOptions [0].GetComponent<RectTransform> ().anchoredPosition.y);
			GameObject tempOb = textOptions [0];
			textOptions.Remove (tempOb);
			textOptions.Add (tempOb);
			selectedTextOption = textOptions [15];
			if (textOptions [15].GetComponent<Text> () != null) {
				textOptions [15].GetComponent<Text> ().color = selectedColor;
			} else {
				textOptions [15].GetComponent<Image> ().color = selectedColor;
			}
		}
		//If we're in the Color input area, scroll text items to the right
		if (playerColorGO.activeInHierarchy) {
			foreach (GameObject ob in colorOptions) {
				RectTransform temp = ob.GetComponent<RectTransform> ();
				temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x - 40, temp.anchoredPosition.y);
				if (ob.GetComponent<Text> () != null) {
					ob.GetComponent<Text> ().color = normalColor;
				}
			}
			textOptions [0].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (textEndX, textOptions [0].GetComponent<RectTransform> ().anchoredPosition.y);
			GameObject tempOb = textOptions [0];
			textOptions.Remove (tempOb);
			textOptions.Add (tempOb);
			selectedTextOption = textOptions [15];
			if (textOptions [15].GetComponent<Text> () != null) {
				textOptions [15].GetComponent<Text> ().color = selectedColor;
			} else {
				textOptions [15].GetComponent<Image> ().color = selectedColor;
			}
		}
	}

	public void ScrollTextRight(){
		foreach (GameObject ob in textOptions) {
			RectTransform temp = ob.GetComponent<RectTransform> ();
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x + 20, temp.anchoredPosition.y);
			if (ob.GetComponent<Text> () != null) {
				ob.GetComponent<Text> ().color = normalColor;
			} else {
				ob.GetComponent<Image> ().color = normalColor;
			}
		}
		textOptions [textOptions.Count-1].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (textStartx, textOptions [textOptions.Count-1].GetComponent<RectTransform> ().anchoredPosition.y);
		GameObject tempOb = textOptions[textOptions.Count-1];
		textOptions.Remove (tempOb);
		textOptions.Insert (0, tempOb);
		selectedTextOption = textOptions [15];
		if (textOptions[15].GetComponent<Text> () != null) {
			textOptions[15].GetComponent<Text> ().color = selectedColor;
		} else {
			textOptions[15].GetComponent<Image> ().color = selectedColor;
		}
	}

	public void SelectText(int player){
		if (playerNameInputGO.gameObject.activeInHierarchy) {
			if (selectedTextOption.GetComponent<Text> () != null && playerName.Length < 8) {
				string temp = "";
				playerName += selectedTextOption.GetComponent<Text> ().text;
				for (int i = 0; i < 8; i++) {
					if (i > 0) {
						temp += " ";
					}
					if (i < playerName.Length - 1) {
						temp += playerName [i];
					} else {
						temp += "_";
					}
				}
				nameInput.text = temp;
			} else {
				GameHandler.instance.players [player].playerName = playerName;
				playerNameInputGO.SetActive (false);
				playerColorGO.SetActive (true);
			}
		}

	}
}
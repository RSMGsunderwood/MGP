using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayScreenChoiceHandler : MonoBehaviour {

	List<GameObject> textOptions = new List<GameObject>();					//All the options the player can cycle through for text settings
	List<GameObject> colorOptions = new List<GameObject>();					//All the options the player can cycle through for color settings
	public GameObject playerNameInputGO, playerColorGO;						//Holders for text options and color options
	public TextMeshProUGUI nameInput, nameInColor;							//Name text shown while inputting name and choosing color
	string[] playerName = new string[] {"","","",""};						//Player names
	public RectTransform textOptionsHolder, colorOptionsHolder;				//Transform holding all options so they can be set easily
	public Color selectedColor, normalColor;								//Color used for selected and normal options
	public GameObject selectedTextOption, selectedColorOption;				//What the player is currently selecting for text and color
	float textStartx = 0, textEndX = 0, colorStartx = 0, colorEndx = 0;		//Variables we'll use to dictate beginning and end transform points for inifinite scrolling

	//Initializes text/color options
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
		textOptions[15].GetComponent<TextMeshProUGUI> ().color = selectedColor;
		//Setup for color input infinite scrolling
		tempPos = colorStartx = -180;
		foreach (Transform child in colorOptionsHolder) {
			RectTransform temp = child.GetComponent<RectTransform> ();
			colorOptions.Add (child.gameObject);
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x + tempPos, temp.anchoredPosition.y);
			tempPos += 60;
		}
		colorEndx = tempPos - 60;
		selectedColorOption = colorOptions [3];
		playerColorGO.SetActive (false);
	}

	//Scrolls text to the left
	public void ScrollTextLeft(PlayerArea pArea){
		ScrollText (false, pArea);
	}

	//Scrolls text to the right
	public void ScrollTextRight(PlayerArea pArea){
		ScrollText (true, pArea);
	}

	/// <summary>
	/// Universal void used for scrolling options (left and right currently)
	/// </summary>
	/// <param name="right">If set true, scrolls to the right</param>
	/// <param name="pArea">Which player area is controlling this input</param>
	void ScrollText(bool right, PlayerArea pArea){
		//Text scrolling variables
		float obMove = -20;							//How far the scrollable item moves when it scrolls.
		float whereObMoveTo = textEndX;				//Where the scrollable item will move to when it hits the edge.
		int whichObMove = 0;						//Which scrollable item will be moved because it is scrolled off.
		int obInsert = textOptions.Count - 1;		//Where to insert the scrollable item in the list after movement.
		int middleSelect = 15;						//What scrollable item to select once movement is finished.
		List<GameObject> options = textOptions;		//Which list we are moving around.
		if (right) {
			obMove = 20;
			whereObMoveTo = textStartx;
			whichObMove = options.Count - 1;
			obInsert = 0;
		}
		//Color scrolling variables
		if (playerColorGO.gameObject.activeInHierarchy) {
			obMove = -60;
			whereObMoveTo = colorEndx;
			whichObMove = 0;
			obInsert = colorOptions.Count - 1;
			middleSelect = 3;
			options = colorOptions;
			if (right) {
				obMove = 60;
				whereObMoveTo = colorStartx;
				whichObMove = options.Count - 1;
				obInsert = 0;
			}
		}
		//Scrolling logic
		foreach (GameObject ob in options) {
			RectTransform temp = ob.GetComponent<RectTransform> ();
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x + obMove, temp.anchoredPosition.y);
			if (playerNameInputGO.gameObject.activeInHierarchy) {
				if (ob.GetComponent<TextMeshProUGUI> () != null) {
					ob.GetComponent<TextMeshProUGUI> ().color = normalColor;
				} else {
					ob.GetComponent<Image> ().color = normalColor;
				}
			}
		}
		options [whichObMove].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (whereObMoveTo, options [whichObMove].GetComponent<RectTransform> ().anchoredPosition.y);
		GameObject tempOb = options [whichObMove];
		options.Remove (tempOb);
		options.Insert (obInsert, tempOb);
		if(playerNameInputGO.gameObject.activeInHierarchy)
			selectedTextOption = options [middleSelect];
		if (playerColorGO.gameObject.activeInHierarchy)
			selectedColorOption = options [middleSelect];
		if (playerNameInputGO.gameObject.activeInHierarchy) {
			if (options [middleSelect].GetComponent<TextMeshProUGUI> () != null) {
				options [middleSelect].GetComponent<TextMeshProUGUI> ().color = selectedColor;
			} else {
				options [middleSelect].GetComponent<Image> ().color = selectedColor;
			}
		} else if (playerColorGO.gameObject.activeInHierarchy) {
			pArea.SetColor(options[middleSelect].GetComponent<TextMeshProUGUI>().color);
		}
	}

	/// <summary>
	/// Player accepts whatever option they currently have selected
	/// </summary>
	/// <param name="player">Which number player is this</param>
	/// <param name="playerArea">Which player area is this</param>
	public void SelectText(int player, PlayerArea playerArea){
		//If the name input is active, look at what was selected
		//If a text object was the selection, they are entering their name.  Only goes up to 8 characters
		//If an image was selected, the player chose to finish their name.
		if (playerNameInputGO.gameObject.activeInHierarchy) {
			if (selectedTextOption.GetComponent<TextMeshProUGUI> () != null && playerName[player].Length < 8) {
				string temp = "";
				playerName[player] += selectedTextOption.GetComponent<TextMeshProUGUI> ().text;
				for (int i = 0; i < 8; i++) {
					if (i > 0) {
						temp += " ";
					}
					if (i < playerName[player].Length) {
						temp += playerName[player][i];
					} else {
						temp += "_";
					}
				}
				nameInput.text = temp;
			} else if(selectedTextOption.GetComponent<Image>() != null) {
				GameHandler.instance.players [player].playerName = playerName[player];
				playerArea.SetName (playerName [player]);
				nameInColor.text = playerName [player] + "\n"+"Choose a Color";
				playerNameInputGO.SetActive (false);
				playerColorGO.SetActive (true);
				playerArea.SetColor(selectedColorOption.GetComponent<TextMeshProUGUI>().color);
			}
		//If the color input is active, they are choosing a color.
		//Any input is accepted as final input
		}else if (playerColorGO.gameObject.activeInHierarchy) {
			GameHandler.instance.players [player].playerColor = selectedColorOption.GetComponent<TextMeshProUGUI> ().color;
			playerColorGO.SetActive (false);
		}
	}
}
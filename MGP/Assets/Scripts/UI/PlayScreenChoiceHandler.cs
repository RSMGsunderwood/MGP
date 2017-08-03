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
	int selectedTextOption = 15, selectedColorOption = 2;					//What the player is currently selecting for text and color
	int textSpacing = 5, colorSpacing = 5;

	//Initializes text/color options
	void Awake(){
		float tempX = 0;

		foreach (Transform child in textOptionsHolder) {
			textOptions.Add (child.gameObject);
		}

		for (int i = selectedTextOption; i >= 0; i--) {
			RectTransform temp = textOptions [i].GetComponent<RectTransform>();
			if (i == selectedTextOption) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX-((temp.rect.width/2)), temp.anchoredPosition.y);
				tempX -= (temp.rect.width / 2);
			}
			tempX -= (temp.rect.width / 2) + textSpacing;
		}
		tempX = 0;
		for (int i = selectedTextOption; i < textOptions.Count; i++) {
			RectTransform temp = textOptions [i].GetComponent<RectTransform>();
			if (i == selectedTextOption) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX+temp.rect.width/2, temp.anchoredPosition.y);
				tempX += (temp.rect.width / 2);
			}
			tempX += (temp.rect.width / 2) + textSpacing;
		}
		textOptions[selectedTextOption].GetComponent<TextMeshProUGUI> ().color = selectedColor;

		foreach (Transform child in colorOptionsHolder) {
			colorOptions.Add (child.gameObject);
		}
		tempX = 0;
		for (int i = selectedColorOption; i >= 0; i--) {
			RectTransform temp = colorOptions [i].GetComponent<RectTransform>();
			if (i == selectedColorOption) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX-((temp.rect.width/2)), temp.anchoredPosition.y);
				tempX -= (temp.rect.width / 2);
			}
			tempX -= (temp.rect.width / 2) + colorSpacing;
		}
		tempX = 0;
		for (int i = selectedColorOption; i < colorOptions.Count; i++) {
			RectTransform temp = colorOptions [i].GetComponent<RectTransform>();
			if (i == selectedColorOption) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX+temp.rect.width/2, temp.anchoredPosition.y);
				tempX += (temp.rect.width / 2);
			}
			tempX += (temp.rect.width / 2) + colorSpacing;
		}
		colorOptions[selectedColorOption].GetComponent<TextMeshProUGUI> ().color = selectedColor;

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
	float moveAmount = 0;
	/// <summary>
	/// Universal void used for scrolling options (left and right currently)
	/// </summary>
	/// <param name="right">If set true, scrolls to the right</param>
	/// <param name="pArea">Which player area is controlling this input</param>
	void ScrollText(bool right, PlayerArea pArea){
		List<GameObject> options = textOptions;
		int selectedOb = selectedTextOption;
		int spacing = textSpacing;
		if (playerColorGO.gameObject.activeInHierarchy){
			options = colorOptions;
			selectedOb = selectedColorOption;
			spacing = colorSpacing;
		}
		GameObject tempOb = options [0];
		int obInsert = options.Count - 1;
		if (right) {
			tempOb = options [options.Count-1];
			obInsert = 0;
		}

		options.Remove (tempOb);
		options.Insert (obInsert, tempOb);
		float tempX = 0;
		for (int i = selectedOb; i >= 0; i--) {
			RectTransform temp = options [i].GetComponent<RectTransform>();
			if (i == selectedOb) {
				moveAmount = temp.anchoredPosition.x;
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX-temp.rect.width/2, temp.anchoredPosition.y);
				tempX -= (temp.rect.width / 2);
			}
			tempX -= (temp.rect.width / 2) + spacing;
			if (playerNameInputGO.gameObject.activeInHierarchy) {
				if (options [i].GetComponent<TextMeshProUGUI> () != null) {
					options [i].GetComponent<TextMeshProUGUI> ().color = normalColor;
				} else {
					options [i].GetComponent<Image> ().color = normalColor;
				}
			}
		}
		tempX = 0;
		for (int i = selectedOb; i < options.Count; i++) {
			RectTransform temp = options [i].GetComponent<RectTransform>();
			if (i == selectedOb) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX+temp.rect.width/2, temp.anchoredPosition.y);
				tempX += (temp.rect.width / 2);
			}
			tempX += (temp.rect.width / 2) + spacing;
			if (playerNameInputGO.gameObject.activeInHierarchy) {
				if (options [i].GetComponent<TextMeshProUGUI> () != null) {
					options [i].GetComponent<TextMeshProUGUI> ().color = normalColor;
				} else {
					options [i].GetComponent<Image> ().color = normalColor;
				}
			}
		}
		if (playerNameInputGO.gameObject.activeInHierarchy) {
			if (options [selectedOb].GetComponent<TextMeshProUGUI> () != null) {
				options [selectedOb].GetComponent<TextMeshProUGUI> ().color = selectedColor;
			} else {
				options [selectedOb].GetComponent<Image> ().color = selectedColor;
			}
		} else if(playerColorGO.gameObject.activeInHierarchy) {
			pArea.SetColor(colorOptions[selectedOb].GetComponent<TextMeshProUGUI>().color);
		}
		StopCoroutine ("moveItems");
		StartCoroutine ("moveItems");
	}

	IEnumerator moveItems(){
		float xTween = 0;
		RectTransform holder = textOptionsHolder;
		if (playerColorGO.gameObject.activeInHierarchy){
			holder = colorOptionsHolder;
		}

		holder.anchoredPosition = new Vector2 (holder.anchoredPosition.x + moveAmount, holder.anchoredPosition.y);
		for (float i = 0; i < .5f; i += Time.deltaTime) {
			xTween = Mathf.Lerp (holder.anchoredPosition.x, 0, i / .5f);
			holder.anchoredPosition = new Vector2 (xTween, holder.anchoredPosition.y);
			yield return null;
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
			if (textOptions[selectedTextOption].GetComponent<TextMeshProUGUI> () != null && playerName[player].Length < 8) {
				string temp = "";
				playerName[player] += textOptions[selectedTextOption].GetComponent<TextMeshProUGUI> ().text;
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
			} else if(textOptions[selectedTextOption].GetComponent<Image>() != null) {
				GameHandler.instance.players [player].playerName = playerName[player];
				playerArea.SetName (playerName [player]);
				nameInColor.text = playerName [player] + "\n"+"Choose a Color";
				playerNameInputGO.SetActive (false);
				playerColorGO.SetActive (true);
				playerArea.SetColor(colorOptions[selectedColorOption].GetComponent<TextMeshProUGUI>().color);
			}
		//If the color input is active, they are choosing a color.
		//Any input is accepted as final input
		}else if (playerColorGO.gameObject.activeInHierarchy) {
			GameHandler.instance.players [player].playerColor = colorOptions[selectedColorOption].GetComponent<TextMeshProUGUI> ().color;
			playerColorGO.SetActive (false);
		}
	}
}
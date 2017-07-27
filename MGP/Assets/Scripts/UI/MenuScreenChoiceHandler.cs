using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScreenChoiceHandler : MonoBehaviour {
	
	List<GameObject> menuOptions = new List<GameObject>();					//All the options the player can cycle through for the menu
	public RectTransform menuOptionsHolder;									//Transform holding all menu options so they can be set easily
	public Color selectedColor, normalColor;								//Color used for selected and normal options
	int selectedMenuOption = 2;												//What the player is currently selecting for the menu
	float moveAmount = 0;

	//Initializes menu options
	void Awake(){
		//Setup for menu infinite scrolling
		float tempX = 0;

		foreach (Transform child in menuOptionsHolder) {
			menuOptions.Add (child.gameObject);
		}
		for (int i = selectedMenuOption; i >= 0; i--) {
			RectTransform temp = menuOptions [i].GetComponent<RectTransform>();
			if (i == selectedMenuOption) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX-((temp.rect.width/2)), temp.anchoredPosition.y);
				tempX -= (temp.rect.width / 2);
			}
			tempX -= (temp.rect.width / 2) + 40;
		}
		tempX = 0;
		for (int i = selectedMenuOption; i < menuOptions.Count; i++) {
			RectTransform temp = menuOptions [i].GetComponent<RectTransform>();
			if (i == selectedMenuOption) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX+temp.rect.width/2, temp.anchoredPosition.y);
				tempX += (temp.rect.width / 2);
			}
			tempX += (temp.rect.width / 2) + 40;
		}
		menuOptions[2].GetComponent<TextMeshProUGUI> ().color = selectedColor;
	}

	//Scrolls text to the left
	public void ScrollTextLeft(){
		ScrollText (false);
	}

	//Scrolls text to the right
	public void ScrollTextRight(){
		ScrollText (true);
	}

	/// <summary>
	/// Universal void used for scrolling options (left and right currently)
	/// </summary>
	/// <param name="right">If set true, scrolls to the right</param>
	/// <param name="pArea">Which player area is controlling this input</param>
	void ScrollText(bool right){
		GameObject tempOb = menuOptions [0];
		int obInsert = menuOptions.Count - 1;
		if (right) {
			tempOb = menuOptions [menuOptions.Count-1];
			obInsert = 0;
		}
		menuOptions.Remove (tempOb);
		menuOptions.Insert (obInsert, tempOb);
		float tempX = 0;
		for (int i = selectedMenuOption; i >= 0; i--) {
			RectTransform temp = menuOptions [i].GetComponent<RectTransform>();
			if (i == selectedMenuOption) {
				moveAmount = temp.anchoredPosition.x;
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX-temp.rect.width/2, temp.anchoredPosition.y);
				tempX -= (temp.rect.width / 2);
			}
			tempX -= (temp.rect.width / 2) + 40;
			menuOptions[i].GetComponent<TextMeshProUGUI> ().color = normalColor;
		}
		tempX = 0;
		for (int i = selectedMenuOption; i < menuOptions.Count; i++) {
			RectTransform temp = menuOptions [i].GetComponent<RectTransform>();
			if (i == selectedMenuOption) {
				temp.anchoredPosition = new Vector2 (0, temp.anchoredPosition.y);
			} else {
				temp.anchoredPosition = new Vector2 (tempX+temp.rect.width/2, temp.anchoredPosition.y);
				tempX += (temp.rect.width / 2);
			}
			tempX += (temp.rect.width / 2) + 40;
			menuOptions[i].GetComponent<TextMeshProUGUI> ().color = normalColor;
		}
		menuOptions[2].GetComponent<TextMeshProUGUI> ().color = selectedColor;
		StopCoroutine ("moveItems");
		StartCoroutine ("moveItems");
	}

	IEnumerator moveItems(){
		float xTween = 0;
		menuOptionsHolder.anchoredPosition = new Vector2 (menuOptionsHolder.anchoredPosition.x + moveAmount, menuOptionsHolder.anchoredPosition.y);
		for (float i = 0; i < .5f; i += Time.deltaTime) {
			xTween = Mathf.Lerp (menuOptionsHolder.anchoredPosition.x, 0, i / .5f);
			menuOptionsHolder.anchoredPosition = new Vector2 (xTween, menuOptionsHolder.anchoredPosition.y);
			yield return null;
		}

	}

	/// <summary>
	/// Player accepts whatever option they currently have selected
	/// </summary>
	public void SelectText(){
		string textStr = menuOptions[selectedMenuOption].GetComponent<TextMeshProUGUI> ().text.ToLower();
		switch (textStr) {
			case "options":
				ScreenHandler.instance.CreateScreen ("optionsscreen", true);
				break;
			case "quit":
				Application.Quit();
				break;
			case "play":
				ScreenHandler.instance.CreateScreen ("playscreen", true);
				break;
			case "rules":
				ScreenHandler.instance.CreateScreen ("rulesscreen", true);
				break;
			default:
				Debug.Log ("Something weird selected HALP");
				break;
		}
	}
}
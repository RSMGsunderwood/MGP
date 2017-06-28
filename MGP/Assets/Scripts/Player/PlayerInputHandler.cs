using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour {

	List<GameObject> options = new List<GameObject>();
	public RectTransform textOptionsHolder;
	public Color selectedColor, normalColor;
	GameObject selectedOption;
	float startx = 0;
	float endx = 0;

	void Awake(){
		float tempPos = -300;
		startx = tempPos;
		foreach (Transform child in textOptionsHolder) {
			RectTransform temp = child.GetComponent<RectTransform> ();
			options.Add (child.gameObject);
			if (child.GetComponent<Text> () != null) {
				if (child.GetComponent<Text> ().text == "A") {
					selectedOption = child.gameObject;
					child.GetComponent<Text> ().color = selectedColor;
				}
			}
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x + tempPos, temp.anchoredPosition.y);
			tempPos += 20;
		}
		endx = tempPos -20;
	}

	public void ScrollTextLeft(){
		foreach (GameObject ob in options) {
			RectTransform temp = ob.GetComponent<RectTransform> ();
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x - 20, temp.anchoredPosition.y);
		}
		options [0].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (endx, options [0].GetComponent<RectTransform> ().anchoredPosition.y);
		GameObject tempOb = options[0];
		options.Remove (tempOb);
		options.Add (tempOb);
	}

	public void ScrollTextRight(){
		foreach (GameObject ob in options) {
			RectTransform temp = ob.GetComponent<RectTransform> ();
			temp.anchoredPosition = new Vector2 (temp.anchoredPosition.x + 20, temp.anchoredPosition.y);
		}
		options [options.Count-1].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (startx, options [options.Count-1].GetComponent<RectTransform> ().anchoredPosition.y);
		GameObject tempOb = options[options.Count-1];
		options.Remove (tempOb);
		options.Insert (0, tempOb);
		
	}

	public void SelectText(){
		
	}
}
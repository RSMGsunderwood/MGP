using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour {

	public List<Text> textOptions = new List<Text>();
	public RectTransform textOptionsHolder;

	void Awake(){
		foreach (Transform child in textOptionsHolder) {
			RectTransform temp = child.GetComponent<RectTransform> ();

		}
	}
}
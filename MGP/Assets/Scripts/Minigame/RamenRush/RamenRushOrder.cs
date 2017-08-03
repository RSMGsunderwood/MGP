using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RamenRushOrder : MonoBehaviour {

	public TextMeshProUGUI soupText;
	public List<Image> soupChecks;
	public TextMeshProUGUI meatText;
	public List<Image> meatChecks;
	public TextMeshProUGUI toppingText;
	public List<Image> toppingChecks;
	[HideInInspector] public Color defaultCheck;
	[HideInInspector] public bool available;
	[HideInInspector] public MinigameRamenRush mgScript;

	void Awake(){
		defaultCheck = soupChecks [0].color;
		available = true;
	}

	public void AssignOrder(){
		soupText.text = mgScript.soups [Random.Range (0, mgScript.soups.Count)];
		meatText.text = mgScript.meats [Random.Range (0, mgScript.meats.Count)];
		toppingText.text = mgScript.toppings [Random.Range (0, mgScript.toppings.Count)];
	}

	float yTween = 0;
	public void Animate(float yAnim){
		yTween = yAnim;
		StartCoroutine ("AnimateRoutine");
	}

	public void Reset(){
		available = false;
		StartCoroutine ("ResetRoutine");
	}

	public void DisableCheck(int player){
		soupChecks [player].color = defaultCheck;
		meatChecks [player].color = defaultCheck;
		toppingChecks [player].color = defaultCheck;
	}

	IEnumerator AnimateRoutine(){
		float tween = 0;
		RectTransform tempTran = null;
		tempTran = this.GetComponent<RectTransform> ();
		for (float x = 0; x < .25f; x += Time.deltaTime) {
			tween = Mathf.Lerp (tempTran.anchoredPosition.y, yTween, x / .25f);
			tempTran.anchoredPosition = new Vector2 (tempTran.anchoredPosition.x, tween);
			yield return null;
		}
		tempTran.anchoredPosition = new Vector2 (tempTran.anchoredPosition.x, yTween);
	}

	IEnumerator ResetRoutine(){
		Animate (110f);
		yield return new WaitForSeconds (1f);
		AssignOrder ();
		Animate (0f);
		available = true;
	}
}

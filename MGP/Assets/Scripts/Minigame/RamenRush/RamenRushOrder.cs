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
	public List<Image> noodleChecks;
	[HideInInspector] public Color defaultCheck;

	public void AssignOrder(string soup, string meat, string topping){
		defaultCheck = soupChecks [0].color;
		soupText.text = soup;
		meatText.text = meat;
		toppingText.text = topping;
	}

	float yTween = 0;
	public void Animate(float yAnim){
		yTween = yAnim;
		StartCoroutine ("AnimateRoutine");
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
}

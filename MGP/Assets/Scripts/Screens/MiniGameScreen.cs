using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameScreen : BaseScreen {

	public RectTransform mNo;
	public RectTransform mTitle;

	void Awake(){
		StartCoroutine ("TitleTween");
	}

	public void Enable(){
		OnEnable ();
	}

	public void Disable(){
		OnDisable ();
	}

	public override void OnBack ()
	{

	}

	public override void OnEnable()
	{
		gameObject.SetActive (true);
		if(currentScreen&&currentScreen!=this)
			currentScreen.OnDisable();
		currentScreen = this;
	}

	public override void OnDisable ()
	{
		gameObject.SetActive (false);
	}

	IEnumerator TitleTween(){
		yield return new WaitForSeconds (5.0f);
		float h1 = mNo.anchoredPosition.y + 65;
		float h2 = mTitle.anchoredPosition.y + 65;
		for (float i = 0; i < 2; i += Time.deltaTime) {
			float tween = Mathf.Lerp (mNo.anchoredPosition.y, h1, i / 2);
			mNo.anchoredPosition = new Vector2 (mNo.anchoredPosition.x, tween);
			tween = Mathf.Lerp (mTitle.anchoredPosition.y, h2, i / 2);
			mTitle.anchoredPosition = new Vector2 (mTitle.anchoredPosition.x, tween);
			yield return null;
		}
		mNo.gameObject.SetActive (false);
	}
}

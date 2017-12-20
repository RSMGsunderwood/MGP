using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhymeRyhmeInputDisplay : MonoBehaviour {

	public Image buttonImage;
	public TextMeshProUGUI buttonText;
	public MinigameRhymeRhyme mGame;
	Vector3 movedTo;
	RectTransform rTran;

	public void SetTran(){
		rTran = this.GetComponent<RectTransform> ();
	}

	public void SetButton(Color c, string s, Vector3 v){
		buttonImage.color = c;
		buttonText.text = s.ToUpper();
		rTran.localPosition = v;
		movedTo = v;
		gameObject.SetActive (true);
		StartCoroutine ("AnimateButton");
	}

	IEnumerator AnimateButton(){
		Vector3 temp = Vector3.zero;
		float speed = (1f * mGame.speed);
		for (float i = 0; i < speed; i += Time.deltaTime) {
			temp = Vector3.Lerp (movedTo, Vector3.zero, i / speed);
			rTran.localPosition = temp;
			yield return null;
		}
		rTran.localPosition = Vector3.zero;
		mGame.buttonBeingSucked = this;
		for (int i = 0; i < mGame.playerHUDS.Count; i++) {
			mGame.playerHUDS [i].TurnOffFeedback ();
		}
		mGame.ShowTime ();
		yield return new WaitForSeconds (.65f);
		if (mGame.buttonBeingSucked == this)
			mGame.buttonBeingSucked = null;
		mGame.buttonPrompts.Push (this);
		this.gameObject.SetActive (false);
	}

}
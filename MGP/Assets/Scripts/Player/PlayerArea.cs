using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerArea : MonoBehaviour {

	public GameObject notPlayingBG;				//Default BG used when player isn't active
	public GameObject playingBg;				//Active player BG
	public GameObject vipImage;					//Icon that shows up for VIPs
	public GameObject readyUpText;				//Text telling player to ready up
	public TextMeshProUGUI playerName;			//Name of player text
	public bool isVIP;							//Is this player VIP?

	/// <summary>
	/// Toggles VIP for this player area
	/// </summary>
	/// <param name="vipSet">If set to true, player is turned into VIP</param>
	public void ToggleVIP(bool vipSet = false){
		isVIP = vipSet;
		vipImage.SetActive (vipSet);
	}

	/// <summary>
	/// Toggles if the player is playing
	/// </summary>
	/// <param name="playing">If set to true, player is toggled to playing</param>
	public void TogglePlaying(bool playing, bool readyUp){
		if (!playingBg.activeInHierarchy && playing) {
			StartCoroutine ("JoinAnimation");
		}
		playingBg.SetActive (playing);
		notPlayingBG.SetActive (!playing);
		readyUpText.SetActive (readyUp);
	}

	/// <summary>
	/// Sets the player's name text
	/// </summary>
	/// <param name="nameSet">The string to set as the player's name</param>
	public void SetName(string nameSet){
		playerName.text = nameSet;
	}

	/// <summary>
	/// Sets the player's color.
	/// </summary>
	/// <param name="c">Color to set for the player</param>
	public void SetColor(Color c){
		playingBg.GetComponent<Image> ().color = c;
		vipImage.GetComponent<Image> ().color = c;
	}

	IEnumerator JoinAnimation(){
		RectTransform rect = this.GetComponent<RectTransform> ();
		float tween = 1f;
		float time = .15f;
		for (float i = 0; i < time; i += Time.deltaTime) {
			if (i <= (time/2f)) {
				tween = Mathf.Lerp (1f, 1.3f, i/(time/2f));
				rect.localScale = new Vector3 (tween, tween, tween);
			} else {
				tween = Mathf.Lerp (1f, 1.3f, (time-i)/(time/2f));
				rect.localScale = new Vector3 (tween, tween, tween);
			}
			yield return null;
		}
		rect.localScale = Vector3.one;
	}

}
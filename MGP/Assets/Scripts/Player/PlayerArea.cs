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
	public void TogglePlaying(bool playing = false){
		playingBg.SetActive (playing);
		notPlayingBG.SetActive (!playing);
		if (readyUpText != null)
			readyUpText.SetActive (!playing);
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
}
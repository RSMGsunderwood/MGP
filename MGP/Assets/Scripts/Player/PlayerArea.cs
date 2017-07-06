using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerArea : MonoBehaviour {

	public GameObject notPlayingBG;
	public GameObject playingBg;
	public GameObject vipImage;
	public GameObject readyUpText;
	public TextMeshProUGUI playerName;
	public bool isVIP;

	public void ToggleVIP(bool vipSet = false){
		isVIP = vipSet;
		vipImage.SetActive (vipSet);
	}

	public void TogglePlaying(bool playing = false){
		playingBg.SetActive (playing);
		notPlayingBG.SetActive (!playing);
		if (readyUpText != null)
			readyUpText.SetActive (!playing);
	}

	public void SetName(string nameSet){
		playerName.text = nameSet;
	}

	public void SetColor(Color c){
		playingBg.GetComponent<Image> ().color = c;
		vipImage.GetComponent<Image> ().color = c;
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerArea : MonoBehaviour {

	public GameObject normalBG;
	public GameObject vipBG;
	public GameObject readyUpText;
	public TextMeshProUGUI playerName;
	public bool isVIP;

	public void ToggleVIP(bool vipSet = false){
		isVIP = vipSet;
		normalBG.SetActive (!vipSet);
		vipBG.SetActive (vipSet);
	}

	public void SetName(string nameSet){
		playerName.text = nameSet;
	}
}
using UnityEngine;
using System.Collections;

public class PlayerArea : MonoBehaviour {

	public GameObject normalBG;
	public GameObject vipBG;
	public bool isVIP;

	public void ToggleVIP(bool vipSet = false){
		isVIP = vipSet;
		normalBG.SetActive (!vipSet);
		vipBG.SetActive (vipSet);
	}
}
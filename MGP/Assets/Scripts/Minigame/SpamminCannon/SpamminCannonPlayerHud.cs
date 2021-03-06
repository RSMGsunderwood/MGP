﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpamminCannonPlayerHud : MonoBehaviour {

	public Image gaugeFill;
	public RectTransform cannonTran;
	float currentPower;

	void Update(){
		if (GameHandler.instance.timer > 0) {
			currentPower -= Time.deltaTime/2f;
			Mathf.Clamp01 (currentPower);
			gaugeFill.fillAmount = currentPower;
		}
	}

	public void AddToGauge(int player){
		currentPower += (1 - (currentPower/1.3f)) * (.2f);
		Mathf.Clamp01 (currentPower);
		GameHandler.instance.players [player].pointScore++;
		StopCoroutine ("CannonShake");
		StartCoroutine ("CannonShake");
	}

	IEnumerator CannonShake(){
		float temp = 0;
		for (float i = 0; i < .1f; i += Time.deltaTime) {
			temp = Mathf.Sin (Time.time * 10f) * (100f*(.1f-i));
			cannonTran.localEulerAngles = new Vector3 (0, 0, temp);
			yield return null;
		}
		cannonTran.localEulerAngles = Vector3.zero;
	}
}
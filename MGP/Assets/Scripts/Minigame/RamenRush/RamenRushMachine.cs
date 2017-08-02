using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RamenRushMachine : MonoBehaviour {

	public TextMeshProUGUI currentChoice;
	public List<GameObject> sets;
	int setChoice = 0;
	int noodleHit =0;
	[HideInInspector] public MinigameRamenRush mgScript;

	public void PlayerInput(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {
			if (sets[1].activeInHierarchy) {
				mgScript.pOrder[player].soupChoice = setChoice;
				setChoice = 0;
				sets [1].SetActive (false);
				sets [2].SetActive (true);
				mgScript.PlayerProgress (player, 1);
				currentChoice.text = mgScript.meats [setChoice];
			} else if (sets[2].activeInHierarchy) {
				setChoice++;
				if (setChoice > mgScript.meats.Count - 1) {
					setChoice = 0;
				}
				currentChoice.text = mgScript.meats [setChoice];
			}
		}
		if (button == InputHandler.Buttons.b) {
			if (sets[2].activeInHierarchy) {
				mgScript.pOrder[player].meatChoice = setChoice;
				setChoice = 0;
				sets [2].SetActive (false);
				sets [3].SetActive (true);
				mgScript.PlayerProgress (player, 2);
				currentChoice.text = mgScript.toppings [setChoice];
			} else if (sets[3].activeInHierarchy) {
				setChoice++;
				if (setChoice > mgScript.toppings.Count - 1) {
					setChoice = 0;
				}
				currentChoice.text = mgScript.toppings [setChoice];
			}
		}
		if (button == InputHandler.Buttons.x) {
			if (sets[0].activeInHierarchy) {
				sets [0].SetActive (false);
				sets [1].SetActive (true);
				currentChoice.text = mgScript.soups [setChoice];
			} else if (sets[1].activeInHierarchy) {
				setChoice++;
				if (setChoice > mgScript.soups.Count - 1) {
					setChoice = 0;
				}
				currentChoice.text = mgScript.soups [setChoice];
			}
		}
		if (button == InputHandler.Buttons.a) {
			if (sets[3].activeInHierarchy) {
				mgScript.pOrder[player].toppingChoice = setChoice;
				setChoice = 0;
				noodleHit = 0;
				sets [3].SetActive (false);
				sets [4].SetActive (true);
				mgScript.PlayerProgress (player, 3);
				currentChoice.text = "";
			} else if (sets[4].activeInHierarchy) {
				noodleHit++;
				if (noodleHit >= 6) {
					noodleHit = 0;
					mgScript.PlayerProgress (player, 3);
					sets [4].SetActive (false);
					sets [0].SetActive (true);
				}
			}
		}
	}
		
}
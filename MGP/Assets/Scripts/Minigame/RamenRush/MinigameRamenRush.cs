using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameRamenRush : MinigameMain {

	public List<RamenRushOrder> ramenOrders;
	public List<RamenRushMachine> ramenMachines;
	public RectTransform cheatSheet;
	public List<string> soups;
	public List<int> soupP;
	public List<string> meats;
	public List<int> meatP;
	public List<string> toppings;
	public List<int> toppingP;
	public List<PlayerOrder> pOrder = new List<PlayerOrder>();


	//Initialize game on startup
	void Awake(){
		//Sets up orders
		string tempSoup = "";
		string tempMeat = "";
		string tempTopping = "";
		for (int i = 0; i < ramenOrders.Count; i++) {
			tempSoup = soups [Random.Range (0, soups.Count)];
			tempMeat = meats [Random.Range (0, meats.Count)];
			tempTopping = toppings [Random.Range (0, toppings.Count)];
			ramenOrders [i].AssignOrder (tempSoup, tempMeat, tempTopping);
		}
		for (int i = 0; i < 4; i++) {
			PlayerOrder playerO = new PlayerOrder();
			bool temp = true;
			if (GameHandler.instance.players [i].isPlaying) {
				ramenMachines [i].gameObject.SetActive (true);
			}
			for (int x = 0; x < ramenOrders.Count; x++) {
				playerO.matching.Add (temp);
			}
			ramenMachines [i].mgScript = this;
			pOrder.Add (playerO);
		}
		//Subscribe to button inputs
		InputHandler.ButtonPressed += this.ButtonPress;
		StartCoroutine ("ShowAllOrders");
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		ramenMachines [player].PlayerInput (player, button);
	}

	public void PlayerProgress(int player, int progress){
		for (int i = 0; i < ramenOrders.Count; i++) {
			if (pOrder [player].matching [i]) {
				switch (progress) {
				case 1:
					if (ramenOrders [i].soupText.text.ToString () == soups [pOrder [player].soupChoice]) {
						ramenOrders [i].soupChecks [player].color = GameHandler.instance.players [player].playerColor;
					} else {
						pOrder [player].matching[i] = false;
					}
					break;
				case 2:
					if (ramenOrders [i].meatText.text.ToString () == meats [pOrder [player].meatChoice]) {
						ramenOrders [i].meatChecks [player].color = GameHandler.instance.players [player].playerColor;
					} else {
						pOrder [player].matching[i] = false;
					}
					break;
				case 3:
					if (ramenOrders [i].toppingText.text.ToString () == toppings [pOrder [player].toppingChoice]) {
						ramenOrders [i].toppingChecks [player].color = GameHandler.instance.players [player].playerColor;
					} else {
						pOrder [player].matching[i] = false;
					}
					break;
				case 4:
					if (ramenOrders [i].toppingText.text.ToString () == toppings [pOrder [player].toppingChoice]) {
						int playerScore = 0;
						playerScore += soupP [pOrder [player].soupChoice];
						playerScore += meatP [pOrder [player].meatChoice];
						playerScore += toppingP [pOrder [player].toppingChoice];
						GameHandler.instance.players [player].pointScore += playerScore;
						progress = 0;
					}
					break;
				default:
					break;
				}
			}
		}
	}

	IEnumerator ShowAllOrders(){
		float tween = 0;
		RectTransform tempTran = null;
		for (int i = 0; i < ramenOrders.Count; i++) {
			ramenOrders [i].Animate (0);
			yield return new WaitForSeconds (.1f);
		}
		tempTran = cheatSheet;
		for (float x = 0; x < .25f; x += Time.deltaTime) {
			tween = Mathf.Lerp (tempTran.anchoredPosition.x, 332.58f, x / .25f);
			tempTran.anchoredPosition = new Vector2 (tween, tempTran.anchoredPosition.y);
			yield return null;
		}
		cheatSheet.anchoredPosition = new Vector2 (332.58f, tempTran.anchoredPosition.y);
	}

}

public class PlayerOrder{
	public int soupChoice;
	public int meatChoice;
	public int toppingChoice;
	public List<bool> matching = new List<bool>();
}
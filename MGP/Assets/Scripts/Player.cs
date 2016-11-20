using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string playerName;
	public enum PlayerNumber{
		Player1,
		Player2,
		Player3,
		Player4
	};
	public PlayerNumber playerNumber;

	void Awake(){
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

	public void ButtonWasHit(int player, InputHandler.Buttons button){
		Debug.Log (player + " " + button);
	}
}

using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	public static GameHandler instance; //static instance for global reference
	public Player[] players = new Player[4]; //The player data we'll use the entire game
	public Minigames minigames;
	[HideInInspector] public Player winningPlayer;
	[HideInInspector] public Minigame chosenGame;
	[HideInInspector] public float timer;
	[HideInInspector] public GameObject chosenGameGO;

	void Awake(){
		instance = this;
		for(int i=0;i<4;i++)
			players[i] = new Player();
		players [0].playerNumber = Player.PlayerNumber.Player1;
		players [1].playerNumber = Player.PlayerNumber.Player2;
		players [2].playerNumber = Player.PlayerNumber.Player3;
		players [3].playerNumber = Player.PlayerNumber.Player4;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void MakeVIP(int player){
		for (int i = 0; i < 4; i++) {
			players [i].isVIP = false;
			if (i == player) {
				players [i].isVIP = true;
			}
		}
	}

	public void ChooseGame(int game, bool random){
		if (random) {
			chosenGame = PickRandomGame ();
		} else {
			chosenGame = minigames.games [game];
		}
	}

	public Minigame PickRandomGame(){
		int r = Random.Range (0, minigames.games.Count);
		return minigames.games [r];
	}

	public void CalculateWinner(){
		int scoreTemp = 0;
		float timeTemp = 9999;
		bool draw = false;
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].pointScore > 0) {
				if (GameHandler.instance.players [i].pointScore > scoreTemp) {
					GameHandler.instance.winningPlayer = GameHandler.instance.players [i];
					scoreTemp = GameHandler.instance.players [i].pointScore;
					timeTemp = GameHandler.instance.players [i].timeScore;
					draw = false;
				} else {
					if (GameHandler.instance.players [i].pointScore == scoreTemp) {
						if (GameHandler.instance.players [i].timeScore < timeTemp) {
							GameHandler.instance.winningPlayer = GameHandler.instance.players [i];
							scoreTemp = GameHandler.instance.players [i].pointScore;
							timeTemp = GameHandler.instance.players [i].timeScore;
							draw = false;
						} else {
							if (GameHandler.instance.players [i].timeScore == timeTemp) {
								draw = true;
							}
						}
					}
				}
			}
		}

		if (draw) {
			GameHandler.instance.winningPlayer = null;
		}
	}

}
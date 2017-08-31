using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

	public static GameHandler instance; 							//static instance for global reference
	public Player[] players = new Player[4]; 						//The player data we'll use the entire game
	public Minigames minigames;										//Prefab reference containing minigame metadata
	public List<PlayerArea> playerSpaces = new List<PlayerArea>();	//Global reference to static player areas.
	[HideInInspector] public Player winningPlayer;					//Player who won the last game
	[HideInInspector] public Minigame chosenGame;					//Minigame that's being played
	[HideInInspector] public float timer;							//Global timer we can reference during minigames
	[HideInInspector] public GameObject chosenGameGO;				//Minigame GO we are using

	//Initial setup on launch
	//Sets instance and initializes player setup
	void Awake(){
		instance = this;
		for(int i=0;i<4;i++)
			players[i] = new Player();
		players [0].playerNumber = Player.PlayerNumber.Player1;
		players [1].playerNumber = Player.PlayerNumber.Player2;
		players [2].playerNumber = Player.PlayerNumber.Player3;
		players [3].playerNumber = Player.PlayerNumber.Player4;
		StartCoroutine (areaAnimate(true));
	}

	//Simple way of quitting game.  Hit ESC to GTFO.
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	/// <summary>
	/// Makes a player VIP
	/// </summary>
	/// <param name="player">Player number to make VIP</param>
	public void MakeVIP(int player){
		for (int i = 0; i < 4; i++) {
			players [i].isVIP = false;
			if (i == player) {
				players [i].isVIP = true;
			}
		}
	}

	/// <summary>
	/// Chooses a minigame
	/// </summary>
	/// <param name="game">Game index number to choose from</param>
	/// <param name="random">If set to true then a random game will be picked</param>
	public void ChooseGame(int game, bool random){
		if (random) {
			chosenGame = PickRandomGame ();
		} else {
			chosenGame = minigames.games [game];
		}
	}

	/// <summary>
	/// Picks a random minigame
	/// </summary>
	/// <returns>Returns a random minigame from the minigame list</returns>
	public Minigame PickRandomGame(){
		int r = Random.Range (0, minigames.games.Count);
		return minigames.games [r];
	}

	/// <summary>
	/// Chooses a winner based on score and then time.
	/// </summary>
	public void CalculateWinner(){
		//Temp values to compare score and time.
		int scoreTemp = 0;
		float timeTemp = 9999;
		bool draw = false;
		for (int i = 0; i < 4; i++) {
			//First sees if player at least has a point
			if (GameHandler.instance.players [i].pointScore > 0) {
				//If they have the most points so far then they are currently the top player
				if (GameHandler.instance.players [i].pointScore > scoreTemp) {
					GameHandler.instance.winningPlayer = GameHandler.instance.players [i];
					scoreTemp = GameHandler.instance.players [i].pointScore;
					timeTemp = GameHandler.instance.players [i].timeScore;
					draw = false;
				} else {
				//If they're tied for points we need to copmare time instead
					if (GameHandler.instance.players [i].pointScore == scoreTemp) {
						//If their time is the shortest then they win
						if (GameHandler.instance.players [i].timeScore < timeTemp) {
							GameHandler.instance.winningPlayer = GameHandler.instance.players [i];
							scoreTemp = GameHandler.instance.players [i].pointScore;
							timeTemp = GameHandler.instance.players [i].timeScore;
							draw = false;
						} else {
							//If somehow the players are tied on score AND time then no one wins! (yay)
							if (GameHandler.instance.players [i].timeScore == timeTemp) {
								draw = true;
							}
						}
					}
				}
			}
		}

		if (draw) {
			//Draws mean that there is no winning player
			GameHandler.instance.winningPlayer = null;
		}
	}


	public IEnumerator areaAnimate(bool down){
		float yTween = 0;
		float tweenTo = -175f;
		if (down) {
			tweenTo = -280f;
		}
		RectTransform playerTran = null;
		yield return new WaitForSeconds (.5f);
		for (int i = 0; i < 4; i++) {
			playerTran = playerSpaces [i].GetComponent<RectTransform> ();
			for (float x = 0; x < .25f; x += Time.deltaTime) {
				yTween = Mathf.Lerp (playerTran.anchoredPosition.y, tweenTo, x / .25f);
				playerTran.anchoredPosition = new Vector2 (playerTran.anchoredPosition.x, yTween);
				yield return null;
			}
			playerTran.anchoredPosition = new Vector2 (playerTran.anchoredPosition.x, tweenTo);
		}
	}
}
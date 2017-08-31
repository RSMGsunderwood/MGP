using UnityEngine;
using System.Collections;

public class Player {

	public string playerName;				//Player's name
	public Color playerColor;				//Color used to represent player
	public enum PlayerNumber{				//Player numbers
		Player1,
		Player2,
		Player3,
		Player4
	};
	public PlayerNumber playerNumber;		//Which player number is this player?
	public bool isVIP = false;				//VIP toggle
	public bool isPlaying = false;			//Is this player currently playing?
	public bool isTheEnemy = false;			//Is this the player versus 3 others?
	public int pointScore;					//Points used for minigame
	public float timeScore;					//Time used for minigame
}
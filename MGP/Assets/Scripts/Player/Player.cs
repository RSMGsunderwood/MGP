using UnityEngine;
using System.Collections;

public class Player {

	public string playerName;
	public Color playerColor;
	public enum PlayerNumber{
		Player1,
		Player2,
		Player3,
		Player4
	};
	public PlayerNumber playerNumber;
	public bool isVIP = false;
	public bool isPlaying = false;
}
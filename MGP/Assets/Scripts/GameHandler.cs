﻿using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	public static GameHandler instance; //static instance for global reference
	public Player[] players = new Player[4]; //The player data we'll use the entire game

	void Awake(){
		instance = this;
		players [0].playerNumber = Player.PlayerNumber.Player1;
		players [1].playerNumber = Player.PlayerNumber.Player2;
		players [2].playerNumber = Player.PlayerNumber.Player3;
		players [3].playerNumber = Player.PlayerNumber.Player4;
	}



}
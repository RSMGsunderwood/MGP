using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class MinigameMain : MonoBehaviour {

	//Abstract void for button presses
	public abstract void ButtonPress (int player, InputHandler.Buttons button);
	//Abstract void for button releases
	public abstract void ButtonRelease (int player, InputHandler.Buttons button);

}
//Minigame class with metadata and gameobject reference
[System.Serializable]
public class Minigame {
	public string name;					//Name of the minigame
	public enum Gametype{				//Types of minigames
		soloVersus,
		teamVersus,
		OneVersusThree
	};
	public enum Tags{					//Metadata descriptive tags
		speed,
		awareness,
		accuracy
	};
	public Gametype gametype;			//What type of minigame is this?
	public float timer;					//Timer for this minigame
	public bool usesTimer;				//Does this minigame use a timer?
	public bool visibleTimer;			//Is the timer for this minigame visible?
	public bool hidePlayerAreas;		//Do we want to hide the Player Area UI?
	public List<Tags> tags;				//What kind of tags apply to this game?
	public string rulesDescription;		//Rules for this game
	public string extDescription;		//Description for this game
	public GameObject mPrefab;			//Minigame prefab for this game
}
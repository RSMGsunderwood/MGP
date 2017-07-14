using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class MinigameMain : MonoBehaviour {

	public abstract void OnWin ();

	public abstract void ButtonPress (int player, InputHandler.Buttons button);

}

[System.Serializable]
public class Minigame {
	public string name;
	public enum Gametype{
		soloVersus,
		teamVersus
	};
	public enum Tags{
		speed,
		awareness
	};
	public Gametype gametype;
	public float timer;
	public bool usesTimer;
	public bool visibleTimer;
	public List<Tags> tags;
	public string rulesDescription;
	public string extDescription;
	public GameObject mPrefab;
}
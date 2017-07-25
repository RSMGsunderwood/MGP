using UnityEngine;
using System.Collections;

public class ScreenHandler : MonoBehaviour {

	public static ScreenHandler instance;

	public GameObject[] screens;			//All screens used in game
	public GameObject prevScreen;			//Previously shown screen gameobject
	public string prevScreenName;			//Previously shown screen name
	public GameObject currentScreen;		//Currently shown screen gameobject
	public string currentScreenName;		//Currently shown screen name

	//Assigns instance and creates titlescreen
	void Start(){
		instance = this;
		CreateScreen ("titlescreen");
	}

	/// <summary>
	/// Creates a screen based on name passed through parameter
	/// </summary>
	/// <param name="screenName">Name of the screen prefab to instantiate</param>
	/// <param name="destroyPrev">If set to <c>true</c>, then the previous screen will be destroyed</param>
	public void CreateScreen(string screenName ="titlescreen", bool destroyPrev = true){
		GameObject newScreen = null;


		if (screenName.ToLower () == "titlescreen") {
			newScreen = screens [0];
		} else if (screenName.ToLower () == "menuscreen") {
			newScreen = screens [1];
		} else if (screenName.ToLower () == "playscreen") {
			newScreen = screens [2];
		} else if (screenName.ToLower () == "minigamescreen") {
			newScreen = screens [3];
		} else if (screenName.ToLower () == "resultsscreen") {
			newScreen = screens [4];
		} else if (screenName.ToLower () == "rulesscreen") {
			newScreen = screens [5];
		}

		newScreen = Instantiate (newScreen);

		if (currentScreen != null) {
			prevScreen = currentScreen;
			prevScreenName = currentScreenName;
		}

		currentScreen = newScreen;
		currentScreenName = screenName;

		if (prevScreen != null && destroyPrev) {
			GameObject.Destroy (prevScreen);
		}

		if (GameHandler.instance.chosenGameGO != null) {
			GameObject.Destroy (GameHandler.instance.chosenGameGO);
		}

	}

}
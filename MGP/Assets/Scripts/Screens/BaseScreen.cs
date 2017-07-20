using UnityEngine;
using System.Collections;

//Base screen script with necessary functionality
public abstract class BaseScreen : MonoBehaviour {
	//Activated when going back
	public abstract void OnBack ();
	//Activated when screen is enabled
	public abstract void OnEnable ();
	//Activated when screen is disabled
	public abstract void OnDisable();
	//reference to current screen
	public static BaseScreen currentScreen;

}

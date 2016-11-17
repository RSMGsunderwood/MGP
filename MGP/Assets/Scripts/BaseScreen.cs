using UnityEngine;
using System.Collections;

//Base screen script with necessary functionality
public abstract class BaseScreen : MonoBehaviour {

	public abstract void OnBack ();

	public abstract void OnEnable ();

	public abstract void OnDisable();

	public static BaseScreen currentScreen;

}

using UnityEngine;
using System.Collections;

public class GameMenuScreen : BaseScreen {

	//Activates enable
	public void Enable(){
		OnEnable ();
	}
	//Activates disable
	public void Disable(){
		OnDisable ();
	}
	//On back call (unused)
	public override void OnBack ()
	{
		
	}
	//Enable override
	public override void OnEnable()
	{
		gameObject.SetActive (true);
		if(currentScreen&&currentScreen!=this)
			currentScreen.OnDisable();
		currentScreen = this;
	}
	//Disable override
	public override void OnDisable ()
	{
		gameObject.SetActive (false);
	}



}
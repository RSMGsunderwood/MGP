using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#if !UNITY_EDITOR_OSX
using XInputDotNetPure;				//This plugin only works on windows
#endif

public class InputHandler : MonoBehaviour {

	public static InputHandler instance;											//Static instance for global reference
	#if !UNITY_EDITOR_OSX
	GamePadState[] states = new GamePadState[4];									//Current state of controller - Windows only
	GamePadState[] prevState = new GamePadState[4];									//Last state of controller - windows only
	#endif
	public enum Buttons{															//Buttons we use in game
		a,
		b,
		x,
		y
	};

	public delegate void EventHandler(int player, InputHandler.Buttons button);		//Delegate for button presses that can be subscribed to
	public static event EventHandler ButtonPressed;									//Static event for other classes to subscribe to

	//Assign instance on launch
	void Awake(){
		instance = this;
	}

	//This is where we'll check for inputs for all players
	void Update()
	{
		//Only check if a button is actually being pressed
		if(ButtonPressed!=null)
			for (int i = 0; i < 4; i++) {
			//xInput section
			#if !UNITY_EDITOR_OSX
			PlayerIndex getIndex = (PlayerIndex)i;
			GamePadState testState = GamePad.GetState (getIndex);
			if (testState.IsConnected) {
				prevState [i] = states [i];
				states [i] = GamePad.GetState (getIndex);
				if (prevState [i].Buttons.A == ButtonState.Released && states [i].Buttons.A == ButtonState.Pressed) {
					InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
				}
				if (prevState [i].Buttons.B == ButtonState.Released && states [i].Buttons.B == ButtonState.Pressed) {
					InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
				}
				if (prevState [i].Buttons.X == ButtonState.Released && states [i].Buttons.X == ButtonState.Pressed) {
					InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
				}
				if (prevState [i].Buttons.Y == ButtonState.Released && states [i].Buttons.Y == ButtonState.Pressed) {
					InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
				}
			} else {
			#else
				//Normal joystick support (only 1 player currently)
				if(i==0){
				if(Input.GetKeyDown(KeyCode.JoystickButton16)){
					InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
				}else if(Input.GetKeyDown(KeyCode.JoystickButton17)){
					InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
				}else if(Input.GetKeyDown(KeyCode.JoystickButton18)){
					InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
				}else if(Input.GetKeyDown(KeyCode.JoystickButton19)){
					InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
				}
				}
			#endif
				if (i == 0) {
					//Keyboard support
					if (Input.GetKeyDown ("a")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					} else if (Input.GetKeyDown ("s")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					} else if (Input.GetKeyDown ("d")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					} else if (Input.GetKeyDown ("w")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				} else if (i == 1) {
					if (Input.GetKeyDown ("f")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					} else if (Input.GetKeyDown ("g")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					} else if (Input.GetKeyDown ("h")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					} else if (Input.GetKeyDown ("t")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				} else if (i == 2) {
					if (Input.GetKeyDown ("j")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					} else if (Input.GetKeyDown ("k")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					} else if (Input.GetKeyDown ("l")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					} else if (Input.GetKeyDown ("i")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				} else {
					if (Input.GetKeyDown (KeyCode.Keypad4)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					} else if (Input.GetKeyDown (KeyCode.Keypad5)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					} else if (Input.GetKeyDown (KeyCode.Keypad6)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					} else if (Input.GetKeyDown (KeyCode.Keypad8)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				}
			#if !UNITY_EDITOR_OSX
			}
			#endif
		}
	}
}
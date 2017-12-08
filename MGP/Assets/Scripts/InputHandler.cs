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
	public static event EventHandler ButtonPressed;									//Static event for other classes to subscribe to for button presses
	public static event EventHandler ButtonReleased;								//Static event for other classes to subscribe to for button release

	//Assign instance on launch
	void Awake(){
		instance = this;
	}

	//This is where we'll check for inputs for all players
	void Update()
	{
		#if !UNITY_EDITOR_OSX
		GamePadState testState;
		for (int i = 0; i < 4; i++) {
			PlayerIndex getIndex = (PlayerIndex)i;
			testState = GamePad.GetState (getIndex);
			if(testState.IsConnected){
				prevState [i] = states [i];
				states [i] = GamePad.GetState (getIndex);
			}
		}
		#endif
		//Only check if a button is actually being pressed or released
		if(ButtonPressed!=null)
			for (int i = 0; i < 4; i++) {
			//xInput section
			#if !UNITY_EDITOR_OSX
				PlayerIndex getIndex = (PlayerIndex)i;
				testState = GamePad.GetState (getIndex);
			if (testState.IsConnected) {
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
					}
					if(Input.GetKeyDown(KeyCode.JoystickButton17)){
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					}
					if(Input.GetKeyDown(KeyCode.JoystickButton18)){
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					}
					if(Input.GetKeyDown(KeyCode.JoystickButton19)){
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				}
			#endif
				if (i == 0) {
					//Keyboard support
					if (Input.GetKeyDown ("a")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					}
					if (Input.GetKeyDown ("s")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					}
					if (Input.GetKeyDown ("d")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					}
					if (Input.GetKeyDown ("w")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				} else if (i == 1) {
					if (Input.GetKeyDown ("f")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					}
					if (Input.GetKeyDown ("g")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					}
					if (Input.GetKeyDown ("h")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					}
					if (Input.GetKeyDown ("t")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				} else if (i == 2) {
					if (Input.GetKeyDown ("j")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					}
					if (Input.GetKeyDown ("k")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					}
					if (Input.GetKeyDown ("l")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					}
					if (Input.GetKeyDown ("i")) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				} else {
					if (Input.GetKeyDown (KeyCode.Keypad4)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.x);
					}
					if (Input.GetKeyDown (KeyCode.Keypad5)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.a);
					}
					if (Input.GetKeyDown (KeyCode.Keypad6)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.b);
					}
					if (Input.GetKeyDown (KeyCode.Keypad8)) {
						InputHandler.ButtonPressed (i, InputHandler.Buttons.y);
					}
				}
			#if !UNITY_EDITOR_OSX
			}
			#endif
		}
		if(ButtonReleased!=null)
			for (int i = 0; i < 4; i++) {
				//xInput section
				#if !UNITY_EDITOR_OSX
				PlayerIndex getIndex = (PlayerIndex)i;
				testState = GamePad.GetState (getIndex);
				if (testState.IsConnected) {
					if(prevState [i].Buttons.A == ButtonState.Pressed && states [i].Buttons.A == ButtonState.Released) {
						InputHandler.ButtonReleased (i, InputHandler.Buttons.a);
					}
					if(prevState [i].Buttons.B == ButtonState.Pressed && states [i].Buttons.B == ButtonState.Released) {
						InputHandler.ButtonReleased (i, InputHandler.Buttons.b);
					}
					if(prevState [i].Buttons.X == ButtonState.Pressed && states [i].Buttons.X == ButtonState.Released) {
						InputHandler.ButtonReleased (i, InputHandler.Buttons.x);
					}
					if(prevState [i].Buttons.Y == ButtonState.Pressed && states [i].Buttons.Y == ButtonState.Released) {
						InputHandler.ButtonReleased (i, InputHandler.Buttons.y);
					}
				} else {
				#else
				//Normal joystick support (only 1 player currently)
				if(i==0){
					if(Input.GetKeyUp(KeyCode.JoystickButton16)){
						InputHandler.ButtonReleased (i, InputHandler.Buttons.a);
					}
					if(Input.GetKeyUp(KeyCode.JoystickButton17)){
						InputHandler.ButtonReleased (i, InputHandler.Buttons.b);
					}
					if(Input.GetKeyUp(KeyCode.JoystickButton18)){
						InputHandler.ButtonReleased (i, InputHandler.Buttons.x);
					}
					if(Input.GetKeyUp(KeyCode.JoystickButton19)){
						InputHandler.ButtonReleased (i, InputHandler.Buttons.y);
					}
				}
				#endif
					if (i == 0) {
						//Keyboard support
						if (Input.GetKeyUp ("a")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.x);
						}
						if (Input.GetKeyUp ("s")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.a);
						}
						if (Input.GetKeyUp ("d")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.b);
						}
						if (Input.GetKeyUp ("w")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.y);
						}
					} else if (i == 1) {
						if (Input.GetKeyUp ("f")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.x);
						}
						if (Input.GetKeyUp ("g")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.a);
						}
						if (Input.GetKeyUp ("h")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.b);
						}
						if (Input.GetKeyUp ("t")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.y);
						}
					} else if (i == 2) {
						if (Input.GetKeyUp ("j")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.x);
						}
						if (Input.GetKeyUp ("k")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.a);
						}
						if (Input.GetKeyUp ("l")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.b);
						}
						if (Input.GetKeyUp ("i")) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.y);
						}
					} else {
						if (Input.GetKeyUp (KeyCode.Keypad4)) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.x);
						}
						if (Input.GetKeyUp (KeyCode.Keypad5)) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.a);
						}
						if (Input.GetKeyUp (KeyCode.Keypad6)) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.b);
						}
						if (Input.GetKeyUp (KeyCode.Keypad8)) {
							InputHandler.ButtonReleased (i, InputHandler.Buttons.y);
						}
					}
					#if !UNITY_EDITOR_OSX
				}
					#endif
			}
	}
}
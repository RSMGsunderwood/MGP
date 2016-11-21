using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#if !UNITY_EDITOR_OSX
using XInputDotNetPure;
#endif

public class InputHandler : MonoBehaviour {

	public static InputHandler instance;
	#if !UNITY_EDITOR_OSX
	GamePadState[] states = new GamePadState[4];
	GamePadState[] prevState = new GamePadState[4];
	#endif
	public enum Buttons{
		a,
		b,
		x,
		y
	};

	public delegate void EventHandler(int player, InputHandler.Buttons button);
	public static event EventHandler ButtonPressed;

	void Awake(){
		instance = this;
	}

	void Update()
	{
		for (int i = 0; i < 4; i++) {
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
			} else { //Keyboard support lul
			#endif
				if (i == 0) {
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
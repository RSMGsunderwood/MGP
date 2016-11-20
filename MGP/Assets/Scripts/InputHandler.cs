using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class InputHandler : MonoBehaviour {

	public static InputHandler inputHandler;

	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState[] states = new GamePadState[4];
	GamePadState[] prevState = new GamePadState[4];

	public enum Buttons{
		a,
		b,
		x,
		y
	};

	public delegate void EventHandler(int player, InputHandler.Buttons button);
	public static event EventHandler ButtonPressed;

	void Awake(){
		inputHandler = this;
	}

	void Update()
	{
		for (int i = 0; i < 4; i++) {
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
			}
		}
		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected ans use it
		/*if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}

		prevState = state;
		state = GamePad.GetState(playerIndex);*/

		// Detect if a button was released this frame
		/*if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
		{
			GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}*/

		// Set vibration according to triggers
		//GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

		// Make the current object turn
		//transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
	}

	/*void OnGUI()
	{
		string text = "";
		text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
		text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
		text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
		text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
		text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
		text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
		text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
	}*/


}

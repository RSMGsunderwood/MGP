using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheJukes_CharControl : MonoBehaviour {

	Vector2 movingVec = Vector2.zero;
	Vector2 tweenVec;
	Rigidbody2D rBody;

	void Awake(){
		rBody = this.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		rBody.AddForce (movingVec*100);
		rBody.velocity = Vector2.zero;
	}

	public void PlayerInput(Vector2 direction){
		movingVec = new Vector2 (movingVec.x + direction.x, movingVec.y + direction.y);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheJukes_CharControl : MonoBehaviour {

	Vector2 movingVec = Vector2.zero;
	Vector2 tweenVec;
	Rigidbody2D rBody;
	CircleCollider2D cCol;
	Image imgRef;
	[HideInInspector] MinigameTheJukes minigameRef;
	bool alive = true;
	public bool isGodzilla = false;
	float speed = 100;

	void Awake(){
		imgRef = this.GetComponent<Image> ();
		cCol = this.GetComponent<CircleCollider2D> ();
		rBody = this.GetComponent<Rigidbody2D> ();
	}

	public void SetColor(Color c){
		imgRef.color = c;	
	}

	void FixedUpdate () {
		if (alive) {
			if (isGodzilla) {
				rBody.AddForce (movingVec * (speed*1.2f));
			} else {
				rBody.AddForce (movingVec * speed);
			}
		}
		rBody.velocity = Vector2.zero;
	}

	public void PlayerInput(Vector2 direction){
		movingVec = new Vector2 (movingVec.x + direction.x, movingVec.y + direction.y);
	}

	public void Kill(){
		alive = false;
		cCol.enabled = false;
	}

	void OnCollisionEnter2D(Collision2D c){
		if (isGodzilla) {
			if (c.gameObject.GetComponent<TheJukes_CharControl> () != null) {
				c.gameObject.GetComponent<TheJukes_CharControl> ().Kill ();
				c.gameObject.GetComponent<TheJukes_CharControl> ().SetColor (Color.grey);
				minigameRef.PlayerKilled ();
			}
		}
	}
}
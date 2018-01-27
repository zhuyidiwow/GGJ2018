using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
	
	private Player player;
	
	private float currentAngle;
	private float lastFrameAngle; // counterclockwise from x axis, in deg
	private string horiAxis;
	private string verAxis;

	private void Start() {
		player = transform.parent.GetComponent<Player>();
		horiAxis = "Wheel" + player.GetPlayerNo() + "Hori";
		verAxis = "Wheel" + player.GetPlayerNo() + "Ver";
	}

	private void Update() {
		float x = Input.GetAxis(horiAxis);
		float y = Input.GetAxis(verAxis);
		if (Utilities.Math.GetMagnitude(new Vector2(x, y)) > player.InputThreshold) {
			currentAngle = Utilities.Math.GetAngle(x, y);
			float dAngle = currentAngle - lastFrameAngle; // counterclock wise, in deg
			transform.Rotate(0f, 0f, dAngle);
			lastFrameAngle = currentAngle;
		}
	}
}

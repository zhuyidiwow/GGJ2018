using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
	
	public float Distance;
	public float AngleInDeg;

	private Player player;
	
	private string horiAxis;
	private string verAxis;
	
	private void Start() {
		player = transform.parent.GetComponent<Player>();
		horiAxis = "Pointer" + player.GetPlayerNo() + "Hori";
		verAxis = "Pointer" + player.GetPlayerNo() + "Ver";
	}

	private void Update() {
		
		if (player.ShouldReceiveInput) {
			if (GetInputMagnitude() > player.InputThreshold) {
				AngleInDeg = GetAngle();	
				float angleInRad = AngleInDeg * Mathf.Deg2Rad;
				Vector3 posOffset = Distance * new Vector3(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad), 0f);
				transform.position = player.transform.position + posOffset;
				transform.rotation = Quaternion.Euler(0, 0, AngleInDeg);
			}
		}
		
	}

	private float GetInputMagnitude() {
		return Mathf.Sqrt(Mathf.Pow(Input.GetAxis(verAxis), 2) + Mathf.Pow(Input.GetAxis(horiAxis), 2));
	}

	// In Deg, from x axis
	private float GetAngle() {
		float x = Input.GetAxis(horiAxis);
		float y = Input.GetAxis(verAxis);
		float angle = Mathf.Atan(y / x);
		angle = angle * Mathf.Rad2Deg; // convert to deg
		if (x < 0 && y > 0) {
			angle += 180f;
		} else if (x < 0 && y < 0) {
			angle += 180f;
		} else if (x > 0 && y < 0) {
			angle += 360f;
		}

		return angle;
	}
}

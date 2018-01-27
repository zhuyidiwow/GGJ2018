using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
	
	public float Distance;
	public float AngleInDeg;

	private Player player;
	
	private string horiAxis;
	private string verAxis;
	private string triggerAxis;
	private bool hasJustFired = false;
	
	private void Start() {
		player = transform.parent.GetComponent<Player>();
		horiAxis = "Pointer" + player.GetPlayerNo() + "Hori";
		verAxis = "Pointer" + player.GetPlayerNo() + "Ver";
		triggerAxis = "P" + player.GetPlayerNo() + "Fire";
	}

	private void Update() {
		
		if (player.ShouldReceiveInput) {
			if (GetInputMagnitude() > player.InputThreshold) {
				AngleInDeg = GetAngle();	
				Move();
			}

			if (GetTriggerDown()) {
				Fire();		
			}
		}
	}

	private void Fire() {
		Vector2 direction = player.transform.position - transform.position;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
		if (hit.collider != null) {
			Slot slot = hit.collider.GetComponent<Slot>();
			//TODO: move food there
			Debug.Log("Shooting food from: " + slot.name);
		}
	}

	private void Move() {
		float angleInRad = AngleInDeg * Mathf.Deg2Rad;
		Vector3 posOffset = Distance * new Vector3(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad), 0f);
		transform.position = player.transform.position + posOffset;
		transform.rotation = Quaternion.Euler(0, 0, AngleInDeg);
	}
	
	private bool GetTriggerDown() {
		
		if (Math.Abs(Input.GetAxis(triggerAxis) - 1) < 0.1f && !hasJustFired) {
			hasJustFired = true;
			return true;
		}
		
		// reset
		if (hasJustFired && Math.Abs(Input.GetAxis(triggerAxis) - 0) < 0.1f) {
			hasJustFired = false;
		}

		return false;
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

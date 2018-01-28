using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
	
	public float Distance;

	public bool UseAbsolotueControl = false;
	public AudioClip ShootClip;
	
	private Player player;
	
	private string horiAxis;
	private string verAxis;
	private string triggerAxis;
	private bool hasJustFired = false;
	private bool isNewRotation = true;
	private float currentAngle;
	private float lastFrameAngle;
	private AudioSource audioSource;
	
	private void Start() {
		player = transform.parent.GetComponent<Player>();
		horiAxis = "Pointer" + player.GetPlayerNo() + "Hori";
		verAxis = "Pointer" + player.GetPlayerNo() + "Ver";
		triggerAxis = "P" + player.GetPlayerNo() + "Fire";

		audioSource = GetComponent<AudioSource>();
	}

	private void Update() {
		
		if (player.ShouldReceiveInput) {
			if (UseAbsolotueControl) {
				float x = Input.GetAxis(horiAxis);
				float y = Input.GetAxis(verAxis);
				if (Utilities.Math.GetMagnitude(new Vector2(x, y)) > player.InputThreshold) {
					currentAngle = Utilities.Math.GetAngle(x, y);
					Move();
				}
			} else {

				float x = Input.GetAxis(horiAxis);
				float y = Input.GetAxis(verAxis);
				if (Utilities.Math.GetMagnitude(new Vector2(x, y)) > player.InputThreshold) {

					currentAngle = Utilities.Math.GetAngle(x, y);
					if (isNewRotation) {
						isNewRotation = false;
						lastFrameAngle = currentAngle;
					}

					float dAngle = currentAngle - lastFrameAngle; // counterclock wise, in deg
					transform.RotateAround(player.transform.position, Vector3.forward, dAngle);
					lastFrameAngle = currentAngle;
				} else {
					isNewRotation = true;
				}
			}

			if (GetTriggerDown() && !GameManager.Instance.IsGameOver) {
				Fire();
			}
		}
	}

	private void Fire() {
		Vector2 direction = player.transform.position - transform.position;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100f, LayerMask.GetMask("Wheel"));
		if (hit.collider != null) {
			Slot slot = hit.collider.GetComponent<Slot>();
			if (slot.IsHoldingFood) {
				slot.ShootFood(this);
				Utilities.Audio.PlayAudio(audioSource, ShootClip);
			}
		}
	}

	private void Move() {
		float angleInRad = currentAngle * Mathf.Deg2Rad;
		Vector3 posOffset = Distance * new Vector3(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad), 0f);
		transform.position = player.transform.position + posOffset;
		transform.rotation = Quaternion.Euler(0, 0, currentAngle - 90f);
	}
	
	private bool GetTriggerDown() {
		
		if (Math.Abs(Input.GetAxis(triggerAxis) - 1) < 0.1f && !hasJustFired) {
			//hasJustFired = true;
			return true;
		}
		
		// reset
		if (hasJustFired && Math.Abs(Input.GetAxis(triggerAxis) - 0) < 0.1f) {
			hasJustFired = false;
		}

		return false;
	}
	
	


}

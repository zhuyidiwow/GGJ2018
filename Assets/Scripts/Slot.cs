using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

	public bool IsHoldingFood = false;
	private Transform objectPos;
	private Food food;
	private Player player;
	
	private void Start() {
		player = transform.parent.parent.parent.GetComponent<Player>();
		objectPos = transform.Find("Object Position");
	}

	private void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.CompareTag("Food") || other.gameObject.CompareTag("Giant Carrot")) {
			Food incomingFood = other.GetComponent<Food>();
			if (IsHoldingFood) {
				incomingFood.Drop(player.GetPlayerNo());
			}
			else if (!IsHoldingFood) {
				if (!incomingFood.IsCaught() && !incomingFood.GetShotState()) {
					// catch food
					IsHoldingFood = true;
					food = incomingFood;
					incomingFood.MoveToSlot(this);
				}
			}
		}
	}

	public Transform GetObjectSlotTransform() {
		return objectPos;
	}

	public void ShootFood(Pointer pointer) {
		food.transform.position = pointer.transform.position;
		food.transform.parent = GameObject.Find("Food Generator").transform;
		food.Shoot(pointer.transform.position - transform.parent.parent.position, player.GetPlayerNo());
		
		food = null;
		IsHoldingFood = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Slot : MonoBehaviour {

	public bool IsHoldingFood = false;
	private Transform objectPos;
	private Food food;

	private void Start() {
		objectPos = transform.Find("Object Position");
	}

	private void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.CompareTag("Food")) {
			Food incomingFood = other.GetComponent<Food>();
			if (IsHoldingFood) {
				incomingFood.Drop();
			}
			else if (!IsHoldingFood) {
				IsHoldingFood = true;
				incomingFood.MoveToSlot(this);
				food = incomingFood;
			}
		}
	}

	public Transform GetObjectSlotTransform() {
		return objectPos;
	}

	//TODO: change state of food so it won't be received again
	public void ShootFood(Pointer pointer) {
		food.Shoot(pointer.transform.position - GetObjectSlotTransform().position);
		food.transform.parent = GameObject.Find("FoodContainer").transform;
		food = null;
		IsHoldingFood = false;
	}
}

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
		food.transform.parent = GameObject.Find("FoodContainer").transform;
		food.Shoot(pointer.transform.position - transform.parent.parent.position);
		
		food = null;
		IsHoldingFood = false;
	}
}

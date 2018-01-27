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
			//TODO: make sure that the food won't be reflected, when it just sit in the wheel
			if (IsHoldingFood && incomingFood != food) {
				Vector2 direction = incomingFood.transform.position - transform.parent.parent.position;
				incomingFood.Shoot(direction);
				//TODO: or use drop
				// incomingFood.Drop();
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
		food.transform.parent = null;
		food = null;
		IsHoldingFood = false;
	}
}

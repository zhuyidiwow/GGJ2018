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
			if (!IsHoldingFood) {
				IsHoldingFood = true;
				incomingFood.MoveToSlot(this);
				food = incomingFood;
			} else if (IsHoldingFood) {
				incomingFood.Drop();
			}
		}
	}

	public Transform GetObjectSlotTransform() {
		return objectPos;
	}
}

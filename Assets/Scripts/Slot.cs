using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Slot : MonoBehaviour {

	private bool isHoldingFood = false;
	private Transform objectPos;

	private void Start() {
		objectPos = transform.Find("Object Position");
	}

	private void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.CompareTag("Food")) {
			Food food = other.GetComponent<Food>();
			if (!isHoldingFood) {
				isHoldingFood = true;
				food.MoveToSlot(this);
			} else if (isHoldingFood) {
				food.Shoot(-food.MovingDirection);
			}
		}
	}

	public Transform GetObjectSlotTransform() {
		return objectPos;
	}
}

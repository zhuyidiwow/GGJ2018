using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

	private bool isHoldingFood = false;
	private Transform objectPos;

	private void Start() {
		objectPos = transform.Find("Object Position");
	}

	private void OnTriggerEnter2D(Collider2D other) {
		
		if (!isHoldingFood && other.gameObject.CompareTag("Food")) {
			isHoldingFood = true;
			Food food = other.GetComponent<Food>();
			food.transform.parent = this.transform;
			food.transform.position = objectPos.transform.position;
		}
	}
}

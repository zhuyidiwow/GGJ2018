using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
	
	public float Distance;
	public float AngleInDeg;

	private Player player;

	private void Start() {
		player = transform.parent.GetComponent<Player>();
	}

	private void Update() {
		if (player.ShouldReceiveInput) {
			float angleInRad = AngleInDeg * Mathf.Deg2Rad;
			Vector3 posOffset = Distance * new Vector3(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad), 0f);
			transform.position = player.transform.position + posOffset;
			transform.rotation = Quaternion.Euler(0, 0, AngleInDeg);
		}
	}
}

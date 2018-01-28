using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

	public GameObject GiantCarrotPrefab;
	private int CarrotCount;
	
	public void GrowOne() {
		CarrotCount++;
		if (CarrotCount == 3) {
			GrowGiantCarrot();
		} 
	}

	private void GrowGiantCarrot() {
		List<Food> carrots = new List<Food>();
		foreach (CarrotSlot carrotSlot in GetComponentsInChildren<CarrotSlot>()) {
			carrotSlot.ShrinkAllSign();
			carrots.Add(carrotSlot.Carrot);
		}

		StartCoroutine(GrowCarrotCoroutine(carrots));
	}

	private IEnumerator GrowCarrotCoroutine(List<Food> carrots) {
		float elapsedTime = 0f;
		while (elapsedTime < 0.8f) {
			foreach (Food carrot in carrots) {
				carrot.transform.position = Vector3.Lerp(carrot.transform.position, transform.position, Time.deltaTime * 2f);
			}
			yield return null;
			elapsedTime += Time.deltaTime;
		}

		
		foreach (Food carrot in carrots) {
			Destroy(carrot.gameObject);
		}
		
		GameObject giant = Instantiate(GiantCarrotPrefab, transform.position, transform.rotation);

		StartCoroutine(PopCoroutine(giant));
		
		CarrotCount = 0;
		foreach (CarrotSlot carrotSlot in GetComponentsInChildren<CarrotSlot>()) {
			carrotSlot.ResetSlot();
		}
	}
	
	private IEnumerator PopCoroutine(GameObject objectToPop) {
		Vector3 originalScale = objectToPop.transform.localScale;
		float elapsedTime = 0f;
		while (elapsedTime < 0.4f) {
			objectToPop.transform.localScale = Vector3.Lerp(Vector3.zero, 1.3f * originalScale, elapsedTime / 0.4f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		elapsedTime = 0f;
		while (elapsedTime < 0.2f) {
			objectToPop.transform.localScale = Vector3.Lerp(1.3f * originalScale, originalScale, elapsedTime / 0.2f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	
	
}

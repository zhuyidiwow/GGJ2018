using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

	public GameObject GiantCarrotPrefab;
	public Player Player;
	public AudioClip MergeSound;
	
	private int CarrotCount;
	private AudioSource audioSource;

	private void Start() {
		audioSource = GetComponent<AudioSource>();
	}

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
		foreach (Food carrot in carrots) {
			carrot.GetComponent<SpriteRenderer>().sortingOrder = 2;
		}
		
		float elapsedTime = 0f;
		while (elapsedTime < 0.8f) {
			foreach (Food carrot in carrots) {
				carrot.transform.position = Vector3.Lerp(carrot.transform.position, transform.position + Vector3.up * 0.2f, Time.deltaTime * 2f);
			}
			yield return null;
			elapsedTime += Time.deltaTime;
		}

		foreach (Food carrot in carrots) {
			Destroy(carrot.gameObject);
		}
		
		Utilities.Audio.PlayAudio(audioSource, MergeSound);
		
		GameObject giant = Instantiate(GiantCarrotPrefab, transform.position + Vector3.up * 0.2f, transform.rotation);
		giant.transform.parent = null;
		giant.transform.localScale = 0.3f * Vector3.one;
		giant.transform.Rotate(0f, 0f, 30f);
		StartCoroutine(GiantCarrotCoroutine(giant));
		
		CarrotCount = 0;
		foreach (CarrotSlot carrotSlot in GetComponentsInChildren<CarrotSlot>()) {
			carrotSlot.ResetSlot();
		}
	}
	
	private IEnumerator GiantCarrotCoroutine(GameObject giant) {
		Vector3 originalScale = giant.transform.localScale;
		float elapsedTime = 0f;
		while (elapsedTime < 0.4f) {
			giant.transform.localScale = Vector3.Lerp(Vector3.zero, 1.3f * originalScale, elapsedTime / 0.4f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		elapsedTime = 0f;
		while (elapsedTime < 0.2f) {
			giant.transform.localScale = Vector3.Lerp(1.3f * originalScale, originalScale, elapsedTime / 0.2f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		elapsedTime = 0f;
		while (elapsedTime < 0.15f) {
			giant.transform.Translate(Vector3.up * Time.deltaTime * 0.2f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		elapsedTime = 0f;
		while (elapsedTime < 0.3f) {
			giant.transform.Translate(Vector3.down * Time.deltaTime * 0.2f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		elapsedTime = 0f;
		while (elapsedTime < 0.15f) {
			giant.transform.Translate(Vector3.up * Time.deltaTime * 0.2f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		giant.GetComponent<Food>().Initialize(Player.transform, .8f, 0f);
	}
	
	
}
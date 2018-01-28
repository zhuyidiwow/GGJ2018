using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RabitGenerator : MonoBehaviour {

	public GameObject RabbitPrefab;
	public Vector2 XRange;
	public Vector2 YRange;
	public Vector2 InteralRange;
	public int BaseAmount;
	public AnimationCurve GenerateCurve;
	public int MaxLimit;

	private void Start() {
		StartGenerating();
	}

	public void StartGenerating() {
		StartCoroutine(GenerateCoroutine());
	}
	
	private IEnumerator GenerateCoroutine() {
		while (!GameManager.Instance.IsGameOver) {
			if (transform.childCount < MaxLimit) {
				int amount = (int) (BaseAmount * GenerateCurve.Evaluate(GameManager.Instance.GetProgress()));
				amount *= (int) (transform.childCount <= 50 ? Random.Range(1.2f, 2f) : Random.Range(0.8f, 1f));
				Generate(amount);
			}

			yield return new WaitForSeconds(Random.Range(InteralRange.x, InteralRange.y));
		}	
	}
	
	private void Generate(int amount) {
		StartCoroutine(BunchGeneratCoroutine(amount));
	}

	IEnumerator BunchGeneratCoroutine(int amount) {
		for (int i = 0; i < amount; i++) {
			GenerateOne();
			yield return 0.2f;
		}
	}

	private void GenerateOne() {
		float x = Random.Range(XRange.x, XRange.y);
		float y = Random.Range(YRange.x, YRange.y);
		GameObject rabit = Instantiate(RabbitPrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);
		rabit.GetComponentInChildren<SpriteRenderer>().sortingOrder = Random.Range(0, 1000);
		// Rabbit rabbit = o.GetComponent<Rabbit>();
	}
}

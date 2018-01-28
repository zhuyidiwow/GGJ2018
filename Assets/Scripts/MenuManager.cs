using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public static MenuManager Instance;
	public Player P1;
	public Player P2;
	public GameObject Hint;
	public GameObject[] Images;
	public int ShootCount = 6;

	public TextMeshProUGUI P1Count;
	public TextMeshProUGUI P2Count;
	
	private int p1Count;
	private int p2Count;
	
	public SpriteRenderer Fader;

	private bool isP1Ready;
	private bool isP2Ready;
	
	private void Awake() {
		if (Instance == null) Instance = this;
		Fader.color = new Color(0f, 0f, 0f, 0f);
	}

	private void Start() {
		MusicManager.Instance.StartMusic();
		P1Count.text = p1Count + "/" + ShootCount;
		P2Count.text = p2Count + "/" + ShootCount;
		// StartCoroutine(AnimateCoroutine(Hint, 1.3f));
		StartCoroutine(AnimateCoroutine(Images[0], 1.15f));
		StartCoroutine(AnimateCoroutine(Images[1], 1.15f));
	}

	public void Shoot(int pNo) {
		if (pNo == 1) {
			p1Count++;
			StartCoroutine(ShakeCoroutine(P1.transform.GetChild(0).Find("Cannon").gameObject));

			if (p1Count >= 6) {
				isP1Ready = true;
				P1Count.text = "P1 Ready!";
			} else {
				P1Count.text = p1Count + "/" + ShootCount;
			}
		}

		if (pNo == 2) {
			p2Count++;
			StartCoroutine(ShakeCoroutine(P2.transform.GetChild(0).Find("Cannon").gameObject));
			
			if (p2Count >= 6) {
				isP2Ready = true;
				P2Count.text = "P2 Ready!";
			} else {
				P2Count.text = p2Count + "/" + ShootCount;
			}
		}

		if (isP1Ready && isP2Ready) {
			StartCoroutine(StartGameCoroutine());
		}
	}

	private IEnumerator StartGameCoroutine() {
		float elapsedTime = 0;
		Color transparent = new Color(0f, 0f, 0f, 0f);
		Color black = new Color(0f, 0f, 0f, 1f);
		Hint.SetActive(false);
		while (elapsedTime < 3f) {
			Fader.color = Color.Lerp(transparent, black, elapsedTime / 3f);
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		
		SceneManager.LoadScene("Main");
	}

	private IEnumerator ShakeCoroutine(GameObject objectToShake) {
		float elapsedTime = 0f;
		float duration = 0.2f;
		Vector3 targetScale = 5f * Vector3.one;
		while (elapsedTime < duration) {
			objectToShake.transform.localScale = Vector3.Lerp(objectToShake.transform.localScale, targetScale, Time.deltaTime * 8f);
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		
		elapsedTime = 0f;
		duration = 0.1f;
		targetScale = 2.8f * Vector3.one;
		while (elapsedTime < duration) {
			objectToShake.transform.localScale = Vector3.Lerp(objectToShake.transform.localScale, targetScale, Time.deltaTime * 8f);
			yield return null;
			elapsedTime += Time.deltaTime;
		}
	}
	
	IEnumerator AnimateCoroutine(GameObject oToAnimate, float bigScale) {
		float duration = 0.4f;
		float stepTime = Time.deltaTime / 2f;
		
		float startTime;
		float elapsedTime;
		
		Vector3 targetScale = Vector3.one * bigScale;
		while (true) {
			startTime = Time.time;
			elapsedTime = Time.time - startTime;
			while (elapsedTime < duration) {
				oToAnimate.transform.localScale =
					Vector3.Lerp(Vector3.one, targetScale, elapsedTime / duration);
				yield return new WaitForSeconds(stepTime);
				elapsedTime = Time.time - startTime;
			}
			
			startTime = Time.time;
			elapsedTime = Time.time - startTime;
			while (elapsedTime < duration) {
				oToAnimate.transform.localScale =
					Vector3.Lerp(targetScale, Vector3.one, elapsedTime / duration);
				yield return new WaitForSeconds(stepTime);
				elapsedTime = Time.time - startTime;
			}
		}
	}
	
//	private IEnumerator ShakeCoroutine(GameObject objectToShake) {
//		float elapsedTime = 0f;
//		float duration = 0.25f;
//		Vector3 targetScale = 0.6f * Vector3.one;
//		while (elapsedTime < duration) {
//			objectToShake.transform.localScale = Vector3.Lerp(objectToShake.transform.localScale, targetScale, Time.deltaTime * 5f);
//			yield return null;
//			elapsedTime += Time.deltaTime;
//		}
//		
//		elapsedTime = 0f;
//		duration = 0.1f;
//		targetScale = 0.45f * Vector3.one;
//		while (elapsedTime < duration) {
//			objectToShake.transform.localScale = Vector3.Lerp(objectToShake.transform.localScale, targetScale, Time.deltaTime * 7f);
//			yield return null;
//			elapsedTime += Time.deltaTime;
//		}
//	}
}

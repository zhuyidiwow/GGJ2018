using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	[HideInInspector] public Player P1;
	[HideInInspector] public Player P2;
	
	public TextMeshProUGUI P1ScoreText;
	public TextMeshProUGUI P2ScoreText;
	public TextMeshProUGUI CountDownText;
	public TextMeshProUGUI WinText;
	
	[Tooltip("In seconds")]
	public float GameDuration;
	public AnimationCurve AmountCurve;
	[HideInInspector] public bool IsGameOver = false;
	
	private float startTime;
		
	private void Awake() {
		if (Instance == null) Instance = this;
	}

	private void Start() {
		StartGame();
	}

	private void Update() {
		float elapsedTime = Time.time - startTime;
		
		if (elapsedTime >= GameDuration) {
			if (!IsGameOver) TimeUp();		
		} else {
			foreach (FoodGenerator foodGenerator in FindObjectsOfType<FoodGenerator>()) {
				foodGenerator.AmountPerSecond = AmountCurve.Evaluate(elapsedTime / GameDuration);
			}
		}
	}

	public void UpdateScoreText() {
		P1ScoreText.text = P1.Score.ToString();
		P2ScoreText.text = P2.Score.ToString();
	}

	public float GetProgress() {
		return (Time.time - startTime) / GameDuration;
	}
	
	public Player GetPlayer(int pNo) {
		return pNo == 1 ? P1 : P2;
	}
	
	public void StartGame() {
		startTime = Time.time;
		MusicManager.Instance.StartMusic();
		WinText.text = "";
		StartCoroutine(CountDownCoroutine());
	}

	private IEnumerator CountDownCoroutine() {
		while (!IsGameOver) {
			CountDownText.text = ((int)(GameDuration - (Time.time - startTime))).ToString();
			yield return new WaitForSeconds(0.1f);
		}
	}

	private void TimeUp() {
		IsGameOver = true;
		if (P1.Score > P2.Score) {
			P1Win();
		} else if (P1.Score < P2.Score) {
			P2Win();
		} else {
			Tie();
		}

		StartCoroutine(RestartCoroutine());
	}

	IEnumerator RestartCoroutine() {
		float elapsedTime = 0f;
		while (elapsedTime < 10f) {
			CountDownText.text = "Restarting in: " + ((int) (10f - elapsedTime)).ToString() + "s!";
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		SceneManager.LoadScene("Menu");
	}

	private void P1Win() {
		WinText.text = "P1 Wins!";
	}

	private void P2Win() {
		WinText.text = "P2 Wins!";
	}

	private void Tie() {
		WinText.text = "Tie!";
	}
	
	
}
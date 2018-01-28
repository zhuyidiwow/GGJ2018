using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	[HideInInspector] public Player P1;
	[HideInInspector] public Player P2;

	public Text P1ScoreText;
	public Text P2ScoreText;
	
	[Tooltip("In seconds")]
	public float GameDuration;
	public AnimationCurve AmountCurve;
	[HideInInspector] public bool IsGameOver;
	
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

	public void UpdateScore() {
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
	}

	public void TimeUp() {
		IsGameOver = true;
	}
		
	public void Win(EPlayer ePlayer) {
		IsGameOver = true;
	}
	
	
}
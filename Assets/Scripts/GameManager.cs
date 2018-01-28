using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public Player P1;
	public Player P2;
	
	[Tooltip("In seconds")]
	public float GameDuration;
	public AnimationCurve AmountCurve;
	public bool IsGameOver = false;
	
	private float startTime;
		
	private void Awake() {
		if (Instance == null) Instance = this;
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

	public float GetProgress() {
		return (Time.time - startTime) / GameDuration;
	}
	
	public Player GetPlayer(int pNo) {
		return pNo == 1 ? P1 : P2;
	}
	
	public void StartGame() {
		startTime = Time.time;
	}

	public void TimeUp() {
		IsGameOver = true;
	}
		
	public void Win(EPlayer ePlayer) {
		IsGameOver = true;
	}
	
	
}
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public Player P1;
	public Player P2;
	
	[Tooltip("In seconds")]
	public float GameDuration;
	public AnimationCurve AmountCurve;
	
	private float startTime;
	private bool isGameOver = false;
		
	private void Awake() {
		if (Instance == null) Instance = this;
	}

	private void Update() {
		float elapsedTime = Time.time - startTime;
		
		if (elapsedTime >= GameDuration) {
			if (!isGameOver) TimeUp();		
		} else {
			foreach (FoodGenerator foodGenerator in FindObjectsOfType<FoodGenerator>()) {
				foodGenerator.AmountPerSecond = AmountCurve.Evaluate(elapsedTime / GameDuration);
			}
		}
	}
	
	public void StartGame() {
		startTime = Time.time;
	}

	public void TimeUp() {
		isGameOver = true;
	}
		
	public void Win(EPlayer ePlayer) {
		isGameOver = true;
	}
	
	
}
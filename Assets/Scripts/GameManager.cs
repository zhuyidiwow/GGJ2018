using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	[Tooltip("In seconds")]
	public float GameDuration;
	public AnimationCurve AmountCurve;
	
	private float startTime;
	private bool isGameOver = false;
		
	private void Start() {
		if (Instance == null) Instance = this;
	}

	private void Update() {
		float elapsedTime = Time.time - startTime;
		
		if (elapsedTime >= GameDuration) {
			if (!isGameOver) TimeUp();		
		} else {
			FoodGenerator.Instance.AmountPerSecond = AmountCurve.Evaluate(elapsedTime / GameDuration);
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
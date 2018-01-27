using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public float GameDuration;

	private float startTime;
	private bool isGameOver = false;
		
	public void StartGame() {
		startTime = Time.time;
	}

	public void TimeUp() {
		isGameOver = true;
	}
		
	public void Win(EPlayer ePlayer) {
		isGameOver = true;
	}
	
	private void Start() {
		if (Instance == null) Instance = this;
	}

	private void Update() {
		if (Time.time - startTime >= GameDuration) {
			if (!isGameOver) TimeUp();		
		}
	}
}
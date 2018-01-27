using UnityEngine;

public enum EPlayer {
	Left,
	Right
}
	
public class Player : MonoBehaviour {

	public EPlayer EPlayer;
	public int Score = 0;
	public bool ShouldReceiveInput = true;
	public float InputThreshold = 0.5f;

	private void Start() {
		switch (EPlayer) {
			case EPlayer.Left:
				GameManager.Instance.P1 = this;
				break;
			case EPlayer.Right:
				GameManager.Instance.P2 = this;
				break;
			default:
				Debug.LogError("Player num");
				break;
		}
	}

	public void GetScore() {
			
	}

	public int GetPlayerNo() {
		switch (EPlayer) {
			case EPlayer.Left:
				return 1;
			case EPlayer.Right:
				return 2;
			default:
				Debug.LogError("Eplayer error");
				return 0;
		}
	}
}
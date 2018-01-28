using System.Collections.Generic;
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
	public Transform RabbitAreaCenter;
	public Vector2 RabbitAreaSize;
	
	private Queue<Rabbit> rabbits = new Queue<Rabbit>();
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

	public Vector3 GetRabbitAreaCenter() {
		return RabbitAreaCenter.position;
	}
	
	public void GetScore() {
		Score++;
		GameManager.Instance.UpdateScoreText();
	}

	public void AddRabit(Rabbit rabbit) {
		rabbits.Enqueue(rabbit);
		if (rabbits.Count > 10f) {
			Destroy(rabbits.Dequeue().gameObject);
		}
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
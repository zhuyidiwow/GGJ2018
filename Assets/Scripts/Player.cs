using UnityEngine;

public enum EPlayer {
	Left,
	Right
}
	
public class Player : MonoBehaviour {

	public EPlayer EPlayer;
	public int Score = 0;
	public bool ShouldReceiveInput = true;
		
	public void GetScore() {
			
	}
}
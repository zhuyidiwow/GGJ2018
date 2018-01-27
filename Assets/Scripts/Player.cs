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
	public GameObject Wheel;

	private float currentAngle;
	private float lastFrameAngle; // counterclockwise from x axis, in deg
	private string horiAxis;
	private string verAxis;

	private void Start() {
		horiAxis = "Wheel" + GetPlayerNo() + "Hori";
		verAxis = "Wheel" + GetPlayerNo() + "Ver";
	}

	private void Update() {
		float x = Input.GetAxis(horiAxis);
		float y = Input.GetAxis(verAxis);
		if (Utilities.Math.GetMagnitude(new Vector2(x, y)) > InputThreshold) {
			currentAngle = Utilities.Math.GetAngle(x, y);
			float dAngle = currentAngle - lastFrameAngle; // counterclock wise, in deg
			transform.Rotate(0f, 0f, dAngle);
			lastFrameAngle = currentAngle;
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
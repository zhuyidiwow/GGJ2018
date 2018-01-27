using UnityEngine;

namespace Assets.Scripts {
	public enum EPlayer {
		Left,
		Right
	}
	
	public class Player : MonoBehaviour {

		public EPlayer EPlayer;
		public int Score = 0;
		
		public void GetScore() {
			
		}
	}
}

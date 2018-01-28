﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public static MusicManager Instance;
	public AudioClip MainBGM;
	
	private AudioSource audioSource;
	
	private void Start() {
		audioSource = GetComponent<AudioSource>();
		// StartMusic();
	}

	void StartMusic() {
		Utilities.Audio.PlayAudio(audioSource, MainBGM, 1, true);	
	}
}

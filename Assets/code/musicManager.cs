using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class musicManager : MonoBehaviour {
	public static AudioSource bgm;
	public AudioMixer master;
	public float globalMusicVolume;
	public float globalSFXVolume;
	
	void Awake () {
		bgm = GetComponent<AudioSource>();
		DontDestroyOnLoad(gameObject);
	}

	void Update () {
		master.SetFloat("musicVol", globalMusicVolume);
		master.SetFloat("sfxVol", globalSFXVolume);
	}
}

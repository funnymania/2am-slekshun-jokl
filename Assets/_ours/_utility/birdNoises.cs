using UnityEngine;
using System.Collections;

public class birdNoises : MonoBehaviour {
	public AudioClip caw0;
	public AudioClip crack;
	AudioSource noises;

	void Start () 
    {
		noises=GetComponent<AudioSource>();
    }

	void Roar () 
    {
		noises.PlayOneShot(caw0);
	}

	void neckCrack () 
    {
		noises.PlayOneShot(crack);
	}
}

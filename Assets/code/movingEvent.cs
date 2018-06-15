using UnityEngine;
using System.Collections;

public class movingEvent : MonoBehaviour {
	public SpriteRenderer remains;
	public SpriteRenderer myRenderer;
	public Transform placeMe;
	public Sprite clear;
	Transform tr;
	
	void Start(){
		tr=transform;
	}

	public void leaveRemains(){
		placeMe.position=tr.position;
		placeMe.rotation=tr.rotation;
		remains.sprite=myRenderer.sprite;
	}

	public void clearRemains(){
		remains.sprite=clear;
	}
}

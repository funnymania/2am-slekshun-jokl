using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraBlah : MonoBehaviour {
	
	public static Camera sceney;
	public static Camera player;
	public List<Camera> toResize = new List<Camera>();
	GameObject yada;
	public float length;
	public float targetaspect;
	int i;

	void Start () {
		yada=GameObject.Find("Player Camera");
		player=yada.GetComponent<Camera>();
	}
	
	void Update () {
		/*if(EventHandler.eventFlag){
			sceney.enabled = true;
			player.enabled = false;}
		else
		{	sceney.enabled = false;
			player.enabled = true;}*/
		//BlackBars(); OnGUI used to be void BlackBars(). Keep that in mind...
	}
	
	void OnGUI() {
		// determine the game window's current aspect ratio
	    float windowaspect=(float)Screen.width/(float)Screen.height;
	
	    // current viewport height should be scaled by this amount
	    float scaleheight=windowaspect/targetaspect;
		
		// if scaled height is less than current height, add letterbox
		foreach(Camera cam in toResize){
			if (scaleheight<1.0f){  
				Rect rect=cam.rect;

				rect.width=1.0f;
				rect.height=scaleheight;
				rect.x=0;
				rect.y=(1.0f-scaleheight)/2.0f;
				
				cam.rect=rect;}
			else // add pillarbox
			{   float scalewidth=1.0f/scaleheight;

				Rect rect=cam.rect;

				rect.width=scalewidth;
				rect.height=1.0f;
				rect.x=(1.0f-scalewidth)/2.0f;
				rect.y=0;

				cam.rect=rect;}}
	}
}
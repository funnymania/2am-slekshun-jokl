﻿using UnityEngine;
using System.Collections;

public class outerReveal : MonoBehaviour {
	public inmost revealer;

	void OnTriggerEnter(Collider col){
		if(col.transform.name=="4reveal"){Debug.Log("Fdsfds");
			if(revealer.isGoingIn){
				revealer.isGoingIn=false;revealer.workIt=true;}}
	}
}

﻿using UnityEngine;
using System.Collections;

public class batCamInner : MonoBehaviour {
	void OnTriggerEnter (Collider col) {
		if (col.transform.name == "Folwin") {
			if (!battleCameraHell.movingWith)
				battleCameraHell.movingWith = true;
        }
	}
}
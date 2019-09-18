using UnityEngine;
using System.Collections;

public class batCamOuter : MonoBehaviour {
	void OnTriggerEnter (Collider col) {
		if (col.transform.name == "Folwin") {
			if (battleCameraHell.movingWith)
				battleCameraHell.movingWith=false;
        }
	}
}
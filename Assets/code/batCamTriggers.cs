using UnityEngine;
using System.Collections;

public class batCamTriggers : MonoBehaviour {
	public bool inner;
	void OnTriggerEnter (Collider col) {
		if (col.transform.name=="Folwin") {
			battleCameraHell.movingWith =! battleCameraHell.movingWith;
			if (inner) {
				battleCameraHell.sizeFactor = 0.5F;
				battleCameraHell.movingWith =! battleCameraHell.movingWith;
            } else {
            	battleCameraHell.sizeFactor = 1;
                battleCameraHell.movingWith = true;
            }
        }
	}
}
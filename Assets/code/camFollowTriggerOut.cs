using UnityEngine;
using System.Collections;

public class camFollowTriggerOut : MonoBehaviour {
	public bool yFollow = true;
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			if (!CameraTitleScreen.camFollows) {
				CameraTitleScreen.camFollows = true;
                CameraTitleScreen.camFollowsY=yFollow;
            }
        }
	}
}

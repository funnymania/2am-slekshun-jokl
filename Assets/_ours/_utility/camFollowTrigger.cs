using UnityEngine;
using System.Collections;

public class camFollowTrigger : MonoBehaviour {
	public bool yFollow = true;

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			if (CameraTitleScreen.camFollows) {
				CameraTitleScreen.camFollows = false;
				CameraTitleScreen.camFollowsY = yFollow;
            }
        }
	}
}

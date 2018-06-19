using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class eyesOpen : MonoBehaviour {
	public static bool linesAffect = false;
	public List<Renderer> please = new List<Renderer>();
	int i;
	
	void Update () {
		if (linesAffect) {
			if (Input.GetButtonDown("Eyes")) {
				for (i = 0; i < please.Count; i++) {
					please[i].enabled = true;
                }
            } else if (Input.GetButtonUp("Eyes")) {
				for (i = 0; i < please.Count; i++) {
					please[i].enabled = false;
                }
            }
        }
	}
}
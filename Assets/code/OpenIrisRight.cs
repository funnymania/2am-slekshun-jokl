using UnityEngine;
using System.Collections;

public class OpenIrisRight : MonoBehaviour {
	Animator me;

	void Start () {
		me = GetComponent<Animator>();
	}
	
	void Update () {
		if (Input.GetButtonDown("Eyes")) {
			if (me.GetCurrentAnimatorStateInfo(0).IsName("closeIrisR")) 
                me.Play("openIrisR", 0, me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else 
                me.Play("openIrisR");
        }
		else if (Input.GetButtonUp("Eyes")) {
			if (me.GetCurrentAnimatorStateInfo(0).IsName("openIrisR")) 
                me.Play("closeIrisR", 0, me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else 
                me.Play("closeIrisR");
        }
	}
}

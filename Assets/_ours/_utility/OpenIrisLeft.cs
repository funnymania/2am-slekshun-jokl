using UnityEngine;
using System.Collections;

public class OpenIrisLeft : MonoBehaviour {
	Animator me;

	void Start () {
		me = GetComponent<Animator>();
	}
	
	void Update () {
		if (Input.GetButtonDown("Eyes"))
        {
			if (me.GetCurrentAnimatorStateInfo(0).IsName("closeIrisL")) 
                me.Play("openIrisL", 0, me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else 
                me.Play("openIrisL");
        }
		else if (Input.GetButtonUp("Eyes"))
        {
			if (me.GetCurrentAnimatorStateInfo(0).IsName("openIrisL")) 
                me.Play("closeIrisL", 0, me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else
                me.Play("closeIrisL");
        }
	}
}

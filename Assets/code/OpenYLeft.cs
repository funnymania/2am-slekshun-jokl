using UnityEngine;
using System.Collections;

public class OpenYLeft : MonoBehaviour {
	Animator me;

	void Start(){
		me=GetComponent<Animator>();
	}
	
	void Update(){
		if(Input.GetButtonDown("Eyes")){
			if(me.GetCurrentAnimatorStateInfo(0).IsName("closeL")) me.Play("openL",0,me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else me.Play("openL");}
		else if(Input.GetButtonUp("Eyes")){
			if(me.GetCurrentAnimatorStateInfo(0).IsName("openL")) me.Play("closeL",0,me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else me.Play("closeL");}
	}
}

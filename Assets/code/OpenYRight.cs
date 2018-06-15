using UnityEngine;
using System.Collections;

public class OpenYRight : MonoBehaviour {
	Animator me;

	void Start(){
		me=GetComponent<Animator>();
	}
	
	void Update(){
		if(Input.GetButtonDown("Eyes")){
			if(me.GetCurrentAnimatorStateInfo(0).IsName("closeR")) me.Play("openR",0,me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else me.Play("openR");}
		else if(Input.GetButtonUp("Eyes")){
			if(me.GetCurrentAnimatorStateInfo(0).IsName("openR")) me.Play("closeR",0,me.GetCurrentAnimatorStateInfo(0).normalizedTime);
			else me.Play("closeR");}
	}
}

/*
	if(me.GetCurrentAnimatorStateInfo(0).IsName("closed")){
			if(Input.GetButtonDown("Eyes")){
				me.Play("openR");}}
		else if(me.GetCurrentAnimatorStateInfo(0).IsName("openedR"))
		{	if(Input.GetButtonDown("Eyes")){
				me.Play("closeR");}}
*/
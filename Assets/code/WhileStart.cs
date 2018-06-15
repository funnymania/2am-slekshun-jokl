using UnityEngine;
using System.Collections;

public class WhileStart : MonoBehaviour {
	
	void Update () {
		GetComponent<Animation>().CrossFade("IdleBreath",0.4F);
	}
}

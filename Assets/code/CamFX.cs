using UnityEngine;
using System.Collections;

public class CamFX : MonoBehaviour {
	public Transform followMe;
	public bool yes=true;
	Transform tr;
	bool splah=false;
	void Start(){tr=transform;}
	void FixedUpdate() {
		if(!Player.isBattle){
			tr.position=followMe.position;tr.rotation=followMe.rotation;}
		else tr.position=followMe.position;tr.rotation=followMe.rotation;
	}
}
/* camera follows player in rotation around the cylinder */
using UnityEngine;
using System.Collections;

public class followMe : MonoBehaviour {
	public Transform targ;
	Transform tr;
	void Start(){
		tr=transform;}
	
	void FixedUpdate(){
		tr.position=targ.position;
	}
}

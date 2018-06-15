using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class renderOrder : MonoBehaviour {
	public int order;
	Renderer thisUn;
	void Start(){
	}
	
	void Update(){
		thisUn=GetComponent<Renderer>();
		thisUn.sharedMaterial.renderQueue=order+3000;
	}
}

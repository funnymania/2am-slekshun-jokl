using UnityEngine;
using System.Collections;

public class upScript : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		if(col.tag=="Player"){
			Player.canUp=true;	
		}
	}
	
	void OnTriggerExit(Collider col){
		if(col.tag=="Player")
			Player.canUp=false;
	}
}

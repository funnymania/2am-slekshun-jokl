using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class downScript : MonoBehaviour {

	public static bool switchOrNo = true;
	public Transform current;
	public Transform next;
	List<Collider> curColliders = new List<Collider>();
	List<Collider> nextColliders = new List<Collider>();
	Collider tmp;
	
	void Start () {	
        
        foreach (Transform child in current) {
			tmp = child.GetComponent<Collider>();
			if (tmp != null) 
                curColliders.Add(tmp);
        }
		
        foreach (Transform child in next) {
			tmp = child.GetComponent<Collider>();
			if (tmp != null) 
                nextColliders.Add(tmp);
        }
		
	}
	
	void Update () {
		if (switchOrNo && Player.Lshift) {
			foreach (Collider yada in curColliders) {
				yada.enabled = false;
            }
			foreach(Collider yada in nextColliders) {
				yada.enabled = true;
            }
			switchOrNo = false;
        } else if (!switchOrNo && !Player.Lshift) {
			foreach (Collider yada in nextColliders) {
				yada.enabled = false;
            }
			foreach (Collider yada in curColliders) {
				yada.enabled = true;
            }
			switchOrNo = true;
        }
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			Player.canDown = true;	/// This means: if entering line, start fade behaviors. Otherwise, stop.
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
		    Player.canDown = false;
        }
	}
}

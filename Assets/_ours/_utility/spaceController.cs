using UnityEngine;
using System.Collections;

public class spaceController : MonoBehaviour {
	public bool workIt = false;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) 
    {
		if (col.tag == "Player" && !workIt)
        {
			workIt = true;
        }
	}
}

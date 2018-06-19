using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lineNear : MonoBehaviour {

	public List<GameObject> lines = new List<GameObject>();
	public Transform player;
	Transform tr;
	
	void Start () {
		tr = transform;
	}
	
	void Update () {
		if ((player.position - tr.position).sqrMagnitude < 5 || Player.onLine)
        {
			foreach (GameObject blah in lines)
				blah.SetActive(true);
        }
		else if (!Player.onLine)
        {
			foreach(GameObject blah in lines)
				blah.SetActive(false);
        }
	}
}

﻿using UnityEngine;
using System.Collections;

public class innerReveal : MonoBehaviour {
	public inmost revealer;

	void OnTriggerEnter (Collider col)
    {
		if (col.transform.name == "4reveal")
        {
			if (!revealer.isGoingIn)
            {
				revealer.isGoingIn = true;
                revealer.workIt = true;
            }
        }
	}
}

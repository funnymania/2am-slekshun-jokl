using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//    So, since EVERY outmost is associated with one line, 
//    this is used to be the storage container for all things
//    related to its Line. Each object this is attached to has
//    a collider for EVERY outmost associated with a line, 
//    and so, when one is tripped, it turns things on until
//    another is tripped (leaving the line), which is re-tripped
//    when passing through again. 
   
//    Editor script...
   
//    if(name==line) give mesh collider
//    else if(name==floor) tag with 'floor', give mesh collider
//    manually assign wall triggers, goDown tags for moving downward
   
//    jumps
//    slope correction issue - downhill
//    falling through walls?

public class outmost : MonoBehaviour {

	public static bool workIt = false;

	public List <Transform> stumps;
	List <List<Transform>> stumps4fade;
	List <Transform> stumpTmp;
	List <Renderer> fadeMe;

	Transform etc;
	Renderer please;
	Color yada;
	Vector3 col, colTrans, tempo;
	int i, j, toFade;
	float timeAdd, timeAdd2;
	float dist, dist2;
	bool isGoingIn = true;
	bool localWork = false;
	
	void Start () {
		// col=new Vector3(0,0,please.material.color.a);
		// colTrans=Vector3.zero;
		// yada=please.material.color;
		// tr=transform;
		stumps4fade = new List<List<Transform>>();
		stumpTmp = new List<Transform>();
	/// is stump's parent in the sub-list? (same layer?) if so, add to sub-list
	/// else keep going
	/// if not in list at all, add to stumpTmp, add stumpTmp to stump4fades
		foreach (Transform blah in stumps)
        {
			for (i = 0; i < stumps4fade.Count; i++)
            {
				for (j = 0; j<stumps4fade[i].Count;j++)
                {
					if (blah.parent == stumps4fade[i][j].parent)
                    {
						stumps4fade[i].Add(blah);
                        break;
                    }
                }
				if (j != stumps4fade[i].Count)
                {
					break;
                }
			}
			if (i == stumps4fade.Count)
			{	
                stumpTmp.Add(blah);
				stumps4fade.Add(stumpTmp);
				stumpTmp = new List<Transform>();
            }
        } 
	}

// RULE: All Outmost colliders of a line are grouped together with THIS script attached.
// Sprite renderer sorting order
	void Update () {
		if (workIt && localWork)
        {
			for (i = 0; i < stumps4fade.Count; i++)
            {
				if (stumps4fade[i].Count == 1)
                {
					yada = stumps4fade[i][0].parent.GetComponent<Renderer>().material.color;
					yada.a = .28F - (.28F * (stumps4fade[i][0].position - Player.tr.position).sqrMagnitude / 600);
					stumps4fade[i][0].parent.GetComponent<Renderer>().material.color = yada;
                }
				else if (stumps4fade[i].Count > 1)
				{	
                    dist = (stumps4fade[i][0].position - Player.tr.position).sqrMagnitude;
					for (j = 1; j < stumps4fade[i].Count; j++)
                    {
						dist2 = (stumps4fade[i][j].position - Player.tr.position).sqrMagnitude;
						if (dist2 > dist)
                        {
							toFade = j;
							dist = dist2;
                        }
                    }
                    yada = stumps4fade[i][0].parent.GetComponent<Renderer>().material.color;
                    yada.a = .28F - (.28F * (stumps4fade[i][toFade].position - Player.tr.position).sqrMagnitude / 600);
				}
			}
		}
		else if (!workIt) 
        {    
            localWork = false;
        }
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player")
        {
            // This means: if entering line, start fade behaviors. Otherwise, stop.
			if (!localWork)	
				localWork = true;
			else 
                localWork = false;
        }
	}
}

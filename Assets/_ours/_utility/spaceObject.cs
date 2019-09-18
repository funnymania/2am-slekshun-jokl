using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spaceObject : MonoBehaviour {
	public static bool workIt = false;
	public bool isGoingIn;
	
	public Transform me;
	public float fadeTo;
	spaceController papa;
	Renderer please;
	Color yada;
	Vector3 col;
	
	Transform tr;
	Vector3 colTrans, tempo;
	float timeAdd, timeAdd2;
	int i;
	bool holdIt = false;
	
	void Start () {
		tr = transform;
		papa = tr.parent.gameObject.GetComponent<spaceController>();
		please = gameObject.GetComponent<Renderer>();
		col = new Vector3(0, 0, please.material.color.a);
		colTrans = new Vector3(0, 0, fadeTo);
		yada = new Color(1, 1, 1, 1);
		timeAdd = 0;
	}

	void Update () {
		if (papa.workIt)
        {
			if (workIt)
            {
				if (isGoingIn) 
                {
					if (timeAdd<=0)
                    {
						yada = please.material.color;
						yada.a = colTrans.z;
						please.material.color = yada;
						isGoingIn = true;
						workIt = false;
						timeAdd = 0;
						gameObject.layer = 0;
                    }
					else
					{	
                        tempo = Vector3.Slerp(colTrans, col, timeAdd);
						yada.a = tempo.z;
						please.material.color = yada;
						gameObject.layer = 8;
						timeAdd -= (Time.deltaTime * 2);
                    }
                }
				else
				{	
                    if (timeAdd >= 1)
                    {
						yada = please.material.color;
						yada.a = 1;
						please.material.color = yada;
						isGoingIn = true;
						timeAdd = 1;
                    }
					else
					{	
                        tempo = Vector3.Slerp(colTrans, col, timeAdd);
						yada.a = tempo.z;
						please.material.color = yada;
						gameObject.layer = 8;
						timeAdd += (Time.deltaTime * 2);
                    }
                }
            }
			else if ((me.position - tr.position).sqrMagnitude < 400)
            {
				yada.a = .05F + (.9F * (400 - (tr.position - me.position).sqrMagnitude) / 400);
				colTrans.z = yada.a;
				please.material.color = yada;
            }
			else
            {
                please.material.color = new Color(1, 1, 1, .05F);
            }
        }
		else 
		{	
            // This needs to be a fadeout
            please.material.color=new Color(1,1,1,0);
        } 
	}
	
	
	void OnTriggerEnter (Collider col) 
    {
		if (col.tag == "Player" && workIt) 
        {
			isGoingIn =! isGoingIn;
			colTrans.z = .05F + (.9F * (400 - (tr.position - me.position).sqrMagnitude) / 400);
        }
		else if (col.tag == "Player" && !workIt)
        {
			workIt = true;
        }
	}
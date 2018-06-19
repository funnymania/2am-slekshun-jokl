using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class particlemost : MonoBehaviour {
	public GameObject particle;
	public Transform meshes;
	public List<Transform> lines = new List<Transform>();
	public bool isGoingIn;
	public float fadeTo;
	List<Renderer> please = new List<Renderer>();
	Renderer tmp;
	Color yada;
	List<Vector3> col = new List<Vector3>();
	
	Vector3 colTrans, tempo;
	float timeAdd, timeAdd2;
	int i;
	bool workIt = false;
	
	void Start () {
		foreach (Transform child in meshes)
        {
			tmp = child.GetComponent<Renderer>();
			if (tmp != null)
            {
				please.Add(tmp);
				col.Add(new Vector3(0,0,tmp.material.color.a));
            }
        }
		colTrans = new Vector3(0,0,fadeTo);
		timeAdd = 0;
	}

	// Have to make ALL stumped layers attached at 0 opacity default.
	void Update () {
		if (workIt) 
        {
			if (isGoingIn)
            {
				if (timeAdd > 1)
                {
					isGoingIn = false;
					workIt = false;
					timeAdd = 0;
					foreach (Transform blah in lines)
                    {
						blah.gameObject.layer = 8;
                    }
                }
				else
				{	
                    if (timeAdd == 0) 
                        particle.SetActive(true);
					for (i = 0; i < please.Count; i++)
                    {
						yada = please[i].material.color;
						tempo = Vector3.Slerp(col[i], colTrans, timeAdd);
						yada.a = tempo.z;
						please[i].material.color = yada;
                    }
					timeAdd += (Time.deltaTime * 2);
                }
            }
			else
			{	
                if (timeAdd > 1)
                {
					foreach (Transform blah in lines)
                    {
						blah.gameObject.layer = 0;
                    }
					particle.SetActive(false);
					isGoingIn = true;
					workIt = false;
					timeAdd = 0;
                }
				else
				{	
                    for (i = 0; i < please.Count; i++)
                    {
						yada = please[i].material.color;
						tempo = Vector3.Slerp(colTrans, col[i], timeAdd);
						yada.a = tempo.z;
						please[i].material.color = yada;
                    }
					foreach (Transform child in meshes)
                    {
						child.gameObject.layer = 8;
                    }
					timeAdd += (Time.deltaTime * 2);
                }
            }
        }
	}
	
	
	void OnTriggerEnter (Collider col) 
    {
		if (col.tag == "Player" && !workIt)
			workIt = true;
	}
}
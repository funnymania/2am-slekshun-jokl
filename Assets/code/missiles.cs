using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class missiles : MonoBehaviour {
	public List<Transform> singles=new List<Transform>();
	public List<Vector3> pos=new List<Vector3>();
	List<bool> isClose=new List<bool>();
	public Transform target,father;
	public battleManager manageMe;
	Transform tr;
	int i=0;
	bool go=false;
	void Awake(){
		tr=transform;
		target=manageMe.farthestOpp(father,tr.position);
	}

	void Update(){
		for(i=0;i<singles.Count;i++){
			if(!go){
				if((singles[i].position-target.position).sqrMagnitude>4)
					singles[i].LookAt(target.position+pos[i]);
				else go=true;}
			else if((singles[i].position-target.position).sqrMagnitude>64)
				Destroy(gameObject);
			singles[i].position+=singles[i].forward*.12F;}
	}
}

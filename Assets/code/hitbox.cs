using UnityEngine;
using System.Collections;
// STAR SMASH CRAFT: GO!
public class hitbox : MonoBehaviour {
	missiles papi;
	EnemyInfo victim;
	GameObject target;
	int ya;
	void Awake(){
		target=transform.parent.gameObject;
		papi=target.GetComponent<missiles>();
		target=papi.target.gameObject;
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject==target){
				victim=col.gameObject.GetComponent<EnemyInfo>();
				victim.health-=23;
				ya=papi.singles.IndexOf(gameObject.transform);
				papi.singles.RemoveAt(ya);
				papi.pos.RemoveAt(ya);
				Destroy(gameObject);}
	}
}

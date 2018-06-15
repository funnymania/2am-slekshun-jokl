using UnityEngine;
using System.Collections;

public class deathpit : MonoBehaviour {

	public Transform respawnPt;
	public AudioClip haha;
	AudioSource alal;
	
	void Start(){
		alal=GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter(Collider col){
		//if(!enabled) return;
		Debug.Log(transform.parent);
		if(col.tag=="Player"){
			alal.PlayOneShot(haha);
			Player.tr.position=respawnPt.position;
			Player.respawning=true;
			inmost.localRespawn=true;}
	}
}
/* deathpit is virgin of current
   progress is respawns of past
   remove deathpit what came before and what after

respawns holds virgin && after of current, respawns of next   */
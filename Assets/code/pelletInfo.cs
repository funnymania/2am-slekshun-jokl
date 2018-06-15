using UnityEngine;
using System.Collections;

public class pelletInfo : MonoBehaviour {
	public GameObject missiles; //// missile instantiated at tr.position; missiles lock on to certain offset from opponent
	GameObject tmp,claimant;
	Transform tr;
	missiles become;
	EnemyInfo user;
	Renderer moi;
	float timer;
	bool claimed=false,close=false;

	void Start(){
		tr=transform;
		moi=GetComponent<Renderer>();}

	void Update(){
		if(close){
			if(timer>0.7F)
				close=false;
			else
			{	if(Input.GetAxis("Horizontal2")!=0 || Input.GetAxis("Vertical2")!=0){
					if(Input.GetAxis("Horizontal2")==-1){
						///take away health from user *one option is to take resources back*
						tmp=Instantiate(missiles,tr.position,Quaternion.identity) as GameObject;
						become=tmp.GetComponent<missiles>();
						become.father=claimant.transform;
						tmp.SetActive(true);
						user.health-=8;
						Destroy(gameObject);}
					else if(Input.GetAxis("Horizontal2")==1)
						;
					else if(Input.GetButtonDown("Vertical2")){
						if(Input.GetAxis("Vertical2")==1){
							Player.burstCocked=true;
							user.health-=8;
							Destroy(gameObject);}
						else if(Input.GetAxis("Vertical2")==-1)
							;}}
				timer+=Time.deltaTime;}}
	}

	void OnTriggerEnter(Collider col){
		if(!claimed){
			if(col.tag=="Player"){
				claimant=col.gameObject;
				close=true;
				user=col.gameObject.GetComponent<EnemyInfo>();
				moi.material.color=user.batColor;
				claimed=true;}}
		if(col.gameObject==claimant && !close){
				close=true;timer=0;}
	}
}

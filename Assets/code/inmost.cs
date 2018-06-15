using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inmost : MonoBehaviour {
	public static bool localRespawn=true;
	public GameObject deathpit;
	public Camera zoomIn;
	public respawnCont progress;
	public List<GameObject> deathpitRemove=new List<GameObject>();
	public Transform meshes;
	public Transform colliders;
	public bool isGoingIn;
	public bool workIt = false;
	public float FOVIn,FOVOut;
	public float fadeTo;
	Renderer[] please;
	Collider[] yohm;
	Renderer tmp;
	AudioSource blaht;
	Color yada;
	List<Vector3> col=new List<Vector3>();
	
	Transform bengy;
	Vector3 colTrans,tempo,FOVgo,FOVVecIn,FOVVecOut;
	float timeAdd,timeAdd2;
	float FOVcurrent;
	int i,j;
	bool isGoingInInit;
	
	void Start(){
		isGoingInInit=isGoingIn;
		please=meshes.GetComponentsInChildren<Renderer>();
		yohm=colliders.GetComponentsInChildren<Collider>();
		foreach(Renderer blah in please)
			col.Add(new Vector3(0,0,blah.material.color.a));
		colTrans=new Vector3(0,0,fadeTo);
		if(isGoingInInit) timeAdd=1;
		else timeAdd=0;
	}
	/// Have to make ALL stumped layers attached at 0 opacity default.
	void Update(){
		if(Input.GetButtonDown("Eyes")){
			for(i=0;i<please.Length;i++){
				please[i].enabled=false;}}
		else if(Input.GetButtonUp("Eyes")){
			for(i=0;i<please.Length;i++){
				please[i].enabled=true;}}
		if(workIt){
			if(!isGoingIn){
				if(timeAdd<=0){
					for(i=0;i<please.Length;i++){
						yada=please[i].material.color;
						yada.a=0;
						please[i].material.color=yada;}
					//isGoingIn=false;
					workIt=false;
					timeAdd=0;
					foreach(Renderer yada in please)
						yada.gameObject.layer=0;}
				else
				{	if(timeAdd==1){
						foreach(Collider blah in yohm)
							blah.enabled=false;}
					for(i=0;i<please.Length;i++){
						yada=please[i].material.color;
						tempo=Vector3.Slerp(colTrans,col[i],timeAdd);
						yada.a=tempo.z;
						please[i].material.color=yada;}
					timeAdd-=(Time.deltaTime*2);}}
			else
			{	if(timeAdd>=1){
					for(i=0;i<please.Length;i++){
						yada=please[i].material.color;
						yada.a=1;
						please[i].material.color=yada;}
					//isGoingIn=true;
					workIt=false;
					timeAdd=1;}
				else
				{	if(timeAdd==0){
						foreach(Collider blah in yohm)
							blah.enabled=true;}
					for(i=0;i<please.Length;i++){
						yada=please[i].material.color;
						tempo=Vector3.Slerp(colTrans,col[i],timeAdd);
						yada.a=tempo.z;
						please[i].material.color=yada;}
					foreach(Renderer yada in please)
						yada.gameObject.layer=8;
					timeAdd+=(Time.deltaTime*2);}}}
	}
	
	/*void OnTriggerEnter(Collider col){
		if(col.transform.tag=="hurtBox" && workIt){
			isGoingIn=!isGoingIn;}
		else if(col.transform.tag=="hurtBox" && !workIt){
			workIt=true;}
	}*/
}
	/*
	
		color morpher; BEGIN
						/
					   /
					  /
					 /
					/
				   /
			__	  / 
		   /  \  /
		  /	   \/
		  \____/
			  /
			 / \
			/   \
		   /    /
		  /____/
	 ____/
	/	/
   /   /
   \  /
	\/\
	/\ \
   /\ \ \
  /  \ \ \
 /    \ \ \
/ [ _ ]\ \ \
|\		 /|
\ | [_] | /
   .   .
    ^-^
    [_]

*/

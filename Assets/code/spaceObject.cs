using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spaceObject : MonoBehaviour {
	public static bool workIt=false;
	public bool isGoingIn;
	
	public Transform me;
	public float fadeTo;
	spaceController papa;
	Renderer please;
	Color yada;
	Vector3 col;
	
	Transform tr;
	Vector3 colTrans,tempo;
	float timeAdd,timeAdd2;
	int i;
	bool holdIt=false;
	
	void Start(){
		tr=transform;
		papa=tr.parent.gameObject.GetComponent<spaceController>();
		please=gameObject.GetComponent<Renderer>();
		col=new Vector3(0,0,please.material.color.a);
		colTrans=new Vector3(0,0,fadeTo);
		yada=new Color(1,1,1,1);
		timeAdd=0;
	}

	void Update(){
		if(papa.workIt){
			if(workIt){
				if(isGoingIn){
					if(timeAdd<=0){
						yada=please.material.color;
						yada.a=colTrans.z;
						please.material.color=yada;
						isGoingIn=true;
						workIt=false;
						timeAdd=0;
						gameObject.layer=0;}
					else
					{	tempo=Vector3.Slerp(colTrans,col,timeAdd);
						yada.a=tempo.z;
						please.material.color=yada;
						gameObject.layer=8;
						timeAdd-=(Time.deltaTime*2);}}
				else
				{	if(timeAdd>=1){
						yada=please.material.color;
						yada.a=1;
						please.material.color=yada;
						isGoingIn=true;
						timeAdd=1;}
					else
					{	tempo=Vector3.Slerp(colTrans,col,timeAdd);
						yada.a=tempo.z;
						please.material.color=yada;
						gameObject.layer=8;
						timeAdd+=(Time.deltaTime*2);}}}
			else if((me.position-tr.position).sqrMagnitude<400){
				yada.a=.05F+(.9F*(400-(tr.position-me.position).sqrMagnitude)/400);
				colTrans.z=yada.a;
				please.material.color=yada;}
			else please.material.color=new Color(1,1,1,.05F);}
		else 
		{	please.material.color=new Color(1,1,1,0);} //This needs to be a fadeout
		//Debug.Log((me.position-tr.position).sqrMagnitude);
	}
	
	
	void OnTriggerEnter(Collider col){
		if(col.tag=="Player" && workIt){
			isGoingIn=!isGoingIn;
			colTrans.z=.05F+(.9F*(400-(tr.position-me.position).sqrMagnitude)/400);}
		else if(col.tag=="Player" && !workIt){
			workIt=true;}
		
	}
	
	
	
	/*void sloth(Transform yada){
		
		//does it have a child? go down. 
		
		// does it have a renderer? do stuff
		
		if(
		
		foreach(Transform child in meshes){
			bengy=child;
			tmp=bengy.GetComponent<Renderer>();
			if(tmp!=null){
				please.Add(tmp);
				col.Add(new Vector3(0,0,tmp.material.color.a));}
			else if(bengy.childCount>0){ //children present
				foreach(Transform child in bengy){
					tmp=child.GetComponent<Renderer>();
					if(tmp!=null){
						please.Add(tmp);
						col.Add(new Vector3(0,0,tmp.material.color.a));}}}}
	
	}*/
	/*
	
	void Update(){
		if(workIt){
			if(isGoingIn){
				if(timeAdd<=0){
					isGoingIn=false;
					workIt=false;
					timeAdd=0;
					foreach(Transform blah in lines){
						blah.gameObject.layer=8;}
					foreach(Transform child in meshes)
						child.gameObject.layer=0;}
				else
				{	for(i=0;i<please.Length;i++){
						yada=please[i].material.color;
						tempo=Vector3.Slerp(colTrans,col[i],timeAdd);
						yada.a=tempo.z;
						please[i].material.color=yada;}
					timeAdd-=(Time.deltaTime*2);}}
			else
			{	if(timeAdd>=1){
					foreach(Transform blah in lines){
						blah.gameObject.layer=0;}
					isGoingIn=true;
					workIt=false;
					timeAdd=1;}
				else
				{	for(i=0;i<please.Length;i++){
						yada=please[i].material.color;
						tempo=Vector3.Slerp(colTrans,col[i],timeAdd);
						yada.a=tempo.z;
						please[i].material.color=yada;}
					foreach(Transform child in meshes){
						child.gameObject.layer=8;}
					timeAdd+=(Time.deltaTime*2);}}}
	}
	
	
	
	
	*/
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

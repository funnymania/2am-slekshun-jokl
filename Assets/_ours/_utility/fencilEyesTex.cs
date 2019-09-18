﻿using UnityEngine;
using System.Collections;

public class fencilEyesTex : MonoBehaviour {

	public Color col,col2,colLine,colBird;
	public Texture2D openL,openR,squintL,squintR,irisL,irisR;
	public Material irisL1,irisL2,irisR1,irisR2;
	Transform tr;
	Texture2D rekt;
	GUIStyle style,style2;
	Rect roc,roc2;
	RaycastHit presentEye,pastEye;
	Color cols;
	float work;
	int i,j;
	bool isSquinting=false;
	
	void Start(){
		tr=transform;
		style=new GUIStyle();
		roc=new Rect(Screen.width*.53F,Screen.height*.59F,Screen.width*.25F,Screen.width*.25F);
		roc2=new Rect(Screen.width*.22F,Screen.height*.59F,Screen.width*.25F,Screen.width*.25F);
		col=new Color(0,0,0,1);
		col2=new Color(1,1,1,1);
	}

	void Update(){
		/* cast ray forward */
		if(Input.GetAxis("Squint")!=0){
			isSquinting=true;
			//roc.height=Screen.height*.0833F;
			//roc2.height=Screen.height*.0833F;
			if(Physics.Raycast(tr.position,tr.forward,out presentEye,24)){Debug.DrawRay(tr.position,tr.forward*24);
				if(presentEye.transform.name=="lineSighting"){
					work=(tr.position-presentEye.transform.position).sqrMagnitude;
					if(work==0){
						work=1;col=new Color(0,.6F,.765F,1);}
					else
					{	work=1-work/576;col=new Color(0,.6F*work,.765F*work,1);}}
				else 
				{	col=new Color(0,0,0,1);}}
			if(Physics.Raycast(tr.position,tr.forward,out pastEye,24)){
				if(pastEye.transform.name=="Bird"){
					if(work==0){
						work=1;col2=new Color(.969F,.714F,0,1);}
					else
					{	work=1-work/576;col2=new Color(.969F*work,.714F*work,0,1);}}
				else col2=new Color(1,1,1,1);}}
		else
		{	isSquinting=false;
			//roc.height=Screen.height*.125F;
			//roc2.height=Screen.height*.125F;
			if(Physics.Raycast(tr.position,tr.forward,out presentEye,12)){Debug.DrawRay(tr.position,tr.forward*8);
				if(presentEye.transform.name=="lineSighting"){
					work=(tr.position-presentEye.transform.position).sqrMagnitude;
					if(work==0){
						work=1;col=new Color(0,.6F,.765F,1);}
					else
					{	work=1-work/144;
						for(j=0;j<256;j++){
							for(i=0;i<256;i++){
								cols=irisR.GetPixel(j,i);//Debug.Log(col);
								if(cols.a!=0){
									cols=new Color(0,.6F*work,.765F*work,1);
									irisR.SetPixel(j,i,cols);}}}
						irisR.Apply(false);}}
				else 
				{	col=new Color(0,0,0,1);}}
			if(Physics.Raycast(tr.position,tr.forward,out pastEye,12)){
				if(pastEye.transform.name=="Bird"){
					if(work==0){
						work=1;col2=new Color(.969F,.714F,0,1);}
					else
					{	work=1-work/144;col2=new Color(.969F*work,.714F*work,0,1);}}
				else col2=new Color(1,1,1,1);}}
		
	}
	/*
		cols=openR.GetPixels(0);
		work=1-work/144;
		for(i=0;i<cols.length;i++){
			if(cols[i].a!=0)
				cols[i]=new Color(0,.6F*work,.765F*work,1);}
		openR.SetPixels(cols,0);
		openR.Apply(0);
	*/
	void OnGUI(){
		/*rekt.SetPixel(0,0,col);
		rekt.Apply();*/
		if(!isSquinting){
			/*style.normal.background=openL;
			GUI.Box(roc,GUIContent.none,style);
			style.normal.background=irisL;
			GUI.Box(roc,GUIContent.none,style);*/
			style.normal.background=openR;
			GUI.Box(roc2,GUIContent.none,style);
			style.normal.background=irisR;
			GUI.Box(roc2,GUIContent.none,style);}
		else
		{	style.normal.background=squintL;
			GUI.Box(roc,GUIContent.none,style);
			style.normal.background=squintR;
			GUI.Box(roc2,GUIContent.none,style);}
		/*GUI.DrawTexture(new Rect(CameraBlah.player.rect.x*(float)Screen.width/6,CameraBlah.player.rect.y*(float)Screen.height,
				CameraBlah.player.rect.width*(float)Screen.width,CameraBlah.player.rect.height*(float)Screen.height),
				page1,ScaleMode.ScaleToFit);*/
				/* blend materials*/
	}
}

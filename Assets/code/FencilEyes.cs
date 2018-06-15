using UnityEngine;
using System.Collections;

public class FencilEyes : MonoBehaviour {

	public Color col,col2,colLine,colBird;
	Transform tr;
	Texture2D rekt;
	GUIStyle style,style2;
	Rect roc,roc2;
	RaycastHit presentEye,pastEye;
	float work;
	bool isSquinting=false;
	
	void Start(){
		tr=transform;
		rekt=new Texture2D(1,1);
		style=new GUIStyle();
		roc=new Rect(Screen.width*.595F,Screen.height*.84F,Screen.width*.125F,Screen.height*.125F);
		roc2=new Rect(Screen.width*.28F,Screen.height*.84F,Screen.width*.125F,Screen.height*.125F);
		col=new Color(0,0,0,1);
		col2=new Color(1,1,1,1);
	}

	void Update(){
		/* cast ray forward */
		if(Input.GetAxis("Squint")!=0){
			isSquinting=true;
			roc.height=Screen.height*.0833F;
			roc2.height=Screen.height*.0833F;
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
			roc.height=Screen.height*.125F;
			roc2.height=Screen.height*.125F;
			if(Physics.Raycast(tr.position,tr.forward,out presentEye,12)){Debug.DrawRay(tr.position,tr.forward*8);
				if(presentEye.transform.name=="lineSighting"){
					work=(tr.position-presentEye.transform.position).sqrMagnitude;
					if(work==0){
						work=1;col=new Color(0,.6F,.765F,1);}
					else
					{	work=1-work/144;col=new Color(0,.6F*work,.765F*work,1);}}
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
	
	void OnGUI(){
		rekt.SetPixel(0,0,col);
		rekt.Apply();
		style.normal.background=rekt;
		GUI.Box(roc,GUIContent.none,style);
		rekt.SetPixel(0,0,col2);
		rekt.Apply();
		style.normal.background=rekt;
		GUI.Box(roc2,GUIContent.none,style);
		/*GUI.DrawTexture(new Rect(CameraBlah.player.rect.x*(float)Screen.width/6,CameraBlah.player.rect.y*(float)Screen.height,
				CameraBlah.player.rect.width*(float)Screen.width,CameraBlah.player.rect.height*(float)Screen.height),
				page1,ScaleMode.ScaleToFit);*/
	}
}

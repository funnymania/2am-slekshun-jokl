using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// PROBLEMS: Do Luya collision.. also up/down camera

// Ideas: 	 Have a slow/medium way the camera "switches" to look at things (hallucinations).
//           AND a faster one for NPC interactions. Way in which camera & character movements behave act the same in either,
//			 with the exception of speed.

//			 Targeting should be towards "important part" of enemies (not always their feet :\)
//			 Right Analog jerks switch targets
/*			 A common use for LateUpdate would be a following third-person camera. 
			 If you make your character move and turn inside Update, you can perform all camera movement and rotation calculations in LateUpdate. 
			 This will ensure that the character has moved completely before the camera tracks its position.*/
//			eulerAngles.x to be NOT greater than +/- 67

public class battleCameraHell : MonoBehaviour {
#region Declarations
	public static Transform target;
	public static Transform tr;
	public static Transform sceneCam;
	public static bool eventFlag = false;
	public static bool movingWith = false;
	public static float sizeFactor=1;
	public static Vector3 a;
	
	public Transform beHere;
	public Transform lookHere;
	public bool camSteady = true;
	GameObject yada;
	Camera camera;
	Vector3 b,bPast;
	Vector3 c;
	Vector3 NewMovement;
	Vector3 d;
	Vector3 initial;
	Vector3 final;
	Vector3 posPast;
	Vector3 offset;
	Vector3 oldDist,current;
	float maxTranslate;
	float amt;
	float timer;
	float horiz;
	float vertic;
	float distance,dist1,dist2;
	int i;
	public bool entrance = false;
	public bool arranging = false;
	bool firstMode=false;
	public static bool isTiny =false;
	public Transform enemy;
	List <Transform> enemies;
	Transform targetedEnemy;
	RaycastHit left;
	RaycastHit right,up,down,back;
#endregion
	void Start(){
		tr=transform;
		tr.position=beHere.position;
		tr.LookAt(lookHere);
		distance=(beHere.position-lookHere.position).magnitude;
	}

	void FixedUpdate(){
		/* differentiate beginning positioning from rest.*/
		/*if(!movingWith){
			tr.position=beHere.position;
			tr.LookAt(lookHere);}*/
		if(movingWith)
		{	dist2=(Player.spriteLocale.position-lookHere.position).magnitude;
			dist1=(tr.position-lookHere.position).magnitude;
			if(Player.charState!=Player.CharacterState.Jump)
				tr.position+=Player.camOffset*distance/dist2*sizeFactor;
			else tr.position+=new Vector3(Player.camOffset.x,0,Player.camOffset.z)*distance/dist2*sizeFactor;
			tr.LookAt(lookHere);
			//tr.Translate(new Vector3(0,0,(tr.position-lookHere.position).magnitude-distance));
			tr.Translate(new Vector3(0,0,dist1-distance));}
		
	}
}


/* isitcool is false means scene is being loading */
	// void FixedUpdate(){ posPast=tr.position;
		// if(!eventFlag){
			// if(!SaveState.startScreen){
				// if(!firstMode && !Player.startFlag){
					// #region Targeting
					// if(false/*Input.GetAxis("Target") > 0.2F && Input.GetAxis("LBumper")==0*/){
						// if(entrance){
							// i=0;timer=0;
							// foreach(Transform child in enemy){
								// b=GetComponent<Camera>().WorldToViewportPoint(child.position);
								// if(b.x>0 && b.x<1 && b.y>0 && b.y<1){ 
									// if((child.position-target.position).magnitude<40){
										// enemies.Add(child);
										// if(i==0){
											// targetedEnemy=child;
											// i=1;}
										// else if((target.position-child.position).magnitude<(target.position-targetedEnemy.position).magnitude)
											// targetedEnemy=child;}}}
							// if(enemies.Count!=0){					
								// amt = maxTranslate * Mathf.Sin(Vector3.Angle(target.position-tr.position,
																			 // targetedEnemy.position-target.position)) // be aware.. no negatives 
									// * Mathf.Cos(tr.eulerAngles.x);							// will be reoported.
								// tr.LookAt(targetedEnemy.position);
								// final=tr.TransformPoint(new Vector3(amt,0,0));
								// initial=tr.position;
								// tr.LookAt(target.position);
								// tr.position=Vector3.Lerp(initial,final,1);
								// tr.rotation=Quaternion.Lerp(tr.rotation,
															// Quaternion.LookRotation(targetedEnemy.position-tr.position,Vector3.up),1);}
							// else
							// {	tr.position=target.TransformPoint(new Vector3(0,0,-distance));
								// tr.LookAt(target);}}
						// if(enemies.Count!=0)
							// tr.LookAt(targetedEnemy);
						// if(!entrance){
							// b=a;
							// a=target.position;b=a-b;
							// tr.Translate(b,Space.World);}	// Translation which follows Player2's movements
						// else entrance=false;
					// }
					// #endregion
					// else if(!camSteady)
					// {	if(!entrance){
							// entrance=true;
							// enemies.Clear();}

						// /*if(Player.charState==Player.CharacterState.Strafe){
							// b=new Vector3(horiz,vertic,distance);
							// d=new Vector3(b.x,b.y,b.magnitude-distance)*sizeFactor;
							// tr.position+=Player.moveDirection;
							// tr.Translate(d);tr.LookAt(target);}*/

							// posPast=tr.position;
							// tr.position+=Player.camOffset;
							// if(tr.localEulerAngles.x<270 && tr.localEulerAngles.x>50 && vertic>0)
								// vertic=0;
							// if(tr.localEulerAngles.x>270 && tr.localEulerAngles.x<314 && vertic<0)
								// vertic=0;
							// b=new Vector3(horiz/2F,vertic/2.5F,0);
							// offset=tr.position+tr.InverseTransformDirection(b)-posPast;
						// /*#region CameraCollision
						 // Top Collision & really oblique angles */
						// /*	if(Physics.Raycast(tr.position,tr.forward*-1,out back,0.8F)){
								// NewMovement=Vector3.Cross(Vector3.Cross(back.normal,offset.normalized),back.normal);
								// tr.Translate(b);tr.LookAt(target);
								// if(NewMovement.z>0){
									// tr.Translate(new Vector3(0,0,NewMovement.z));
									// distance-=NewMovement.z;}
								// else if(NewMovement.z<0){
									// tr.Translate(new Vector3(0,0,NewMovement.z*-1));
									// distance+=NewMovement.z;}}
							// else if(Physics.Raycast(tr.position,tr.right,out right,0.8F)){
								// if(Physics.Raycast(tr.position,tr.right*-1,out left,0.8F)){
									// if((right.point-tr.position).sqrMagnitude<(left.point-tr.position).sqrMagnitude){
										// NewMovement=Vector3.Cross(Vector3.Cross(right.normal,offset.normalized),right.normal);
										// tr.Translate(b);tr.LookAt(target);
										// tr.Translate(new Vector3(0,0,NewMovement.z));
										// distance-=NewMovement.z;}
									// else
									// {	NewMovement=Vector3.Cross(Vector3.Cross(left.normal,offset.normalized),left.normal);
										// tr.Translate(b);tr.LookAt(target);//NewMovement=tr.InverseTransformDirection(NewMovement);
										// tr.position+=new Vector3(0,0,NewMovement.z);
										// distance-=NewMovement.z;}}
								// else
								// {	NewMovement=Vector3.Cross(Vector3.Cross(right.normal,offset.normalized),right.normal);
									// tr.Translate(b);tr.LookAt(target);//NewMovement=tr.InverseTransformDirection(NewMovement);
									// if(NewMovement.z>0){
										// tr.Translate(new Vector3(0,0,NewMovement.z));
										// distance-=NewMovement.z;}}}
							// else if(Physics.Raycast(tr.position,tr.right*-1,out left,0.8F)){
								// NewMovement=Vector3.Cross(Vector3.Cross(left.normal,offset.normalized),left.normal);
								// tr.Translate(b);tr.LookAt(target);
								// if(NewMovement.z>0){
									// tr.Translate(new Vector3(0,0,NewMovement.z));
									// distance-=NewMovement.z;}}
							// else if(Physics.Raycast(tr.position,tr.up*-1,out down,0.8F)){
								// NewMovement=Vector3.Cross(Vector3.Cross(down.normal,offset.normalized),down.normal);
								// tr.Translate(b);tr.LookAt(target);
								// if(NewMovement.z>0){
									// tr.Translate(new Vector3(0,0,NewMovement.z));
									// distance-=NewMovement.z;}}
							// else if(Physics.Raycast(tr.position,tr.up,out up,0.8F)){
								// NewMovement=Vector3.Cross(Vector3.Cross(up.normal,offset.normalized),up.normal);
								// tr.Translate(b);tr.LookAt(target);
								// if(NewMovement.z>0){
									// tr.Translate(new Vector3(0,0,NewMovement.z));
									// distance-=NewMovement.z;}}
							// #endregion
							// else
							// {   tr.Translate(b);*/
								// //if(b!=bPast){
									// /*tr.rotation = Quaternion.Lerp(tr.rotation,
									   // Quaternion.LookRotation(target.position - tr.position, Vector3.up),0.2F);*/
									// /*tr.LookAt(target);}*/ // maybe whenever letting on the control stick, make THAT plane the sort
															// // of plane whereby the camera moves along
								// //tr.Translate(new Vector3((tr.position-target.position).magnitude-distance*sizeFactor,0,0));
								// /*if(distance<38) distance+=.125F;*/
								
						// /*else 
						// {	//collision check... if collider... do the z-trick above
							// tr.position+=Player.moveDirection;}*/
							// /*if(distance<6.4){
									// distance+=.06F;
									// tr.LookAt(target);tr.Translate(new Vector3(0,0,(tr.position-target.position).magnitude-distance)*sizeFactor);}*/
						// /*if((tr.position+NewMovement-target.position).sqrMagnitude<=41.1F)
						// {	tr.Translate(NewMovement);tr.LookAt(target);}*/
						// }
					// /*Debug.Log((tr.position+NewMovement-target.position).sqrMagnitude);*/}}}
		// else
		// {	
			// /*tr.position=sceneCam.position;
			// tr.rotation=sceneCam.rotation*/;}
		
	// }
/* sceneCam set to 'tr' of posCam in Scener code 
	This is also done with Luya
   
   
   
   /*  else if(!Player2.startFlag)
		{	b=a;
			a=target.position;
			b=a-b;
			tr.Translate(b,Space.World);	// Translation which follows Player2's movements
			tr.eulerAngles+=new Vector3(Input.GetAxisRaw("Vertical2"),Input.GetAxisRaw("Horizontal2")*-1,0);}*/
   
   
   

















/*
Must project from Player2 at space behind and in front of space a ray to be aware of cliffs.
when running->walking...


stepRaycast has to be a good enough length, and STRAIGHT down... cast a SPEED-RELATED amount away from torso

Step check is NOT active while or idle

// Alternate Idea: Force Player2 to have to hit a button to Jump at edge.
					THIS WAY!! If Player2 does not hit button, literally runs off the edge.
						This is actually kinda cool in the context of having to make the decision
						because then to "run off the edge" is actually a decision, not a happenstance of gaming.
						And given that one can cloud, it means that one's cloud is closer to the edge,
						making it unnecessary to slow down to grab the ledge and then move down to fall.
						Thus: If there is a platform DIRECTLY underneath you that you wish to access,
						
//// Player2 Code
if (stepRaycast false)
    	if(running) JUMP AT NEXT STEP FORWAAAARD!!! (runny)
    	else if(skidding) will reach out to grab ledge.. if speed slow enough, will grab ledge
    	else if(walking) Hope at next step forward.
    	set state == Leap
    	onCloud=false;
    	
   This way, stepRaycast points always down at proper step offset amount, no need for other checks.
 
 else if(charState==CharacterState.Leap){
 	if(Input.GetAxis("LBumper") && Input.GetAxis("Target") && !onCloud){ // Cloud.
 	    charState=CharacterState.Cloud; onCloud=true;
 	    // For falling stuff in raycast, if(onCloud), no movement.
 	    timer=0;}
 	//make charSpeed Large and upwards (not along forward)}
 	   
 else if(charState==CharacterState.Cloud){
    if(timer>=0.4){
    	charState=CharacterState.Idle;isIdle=true;
    	charSpeed=0;endurVelocity=40;}
	timer+=timey;
 }





*/























	
	

		//transform.position = new Vector3(target.position.x,target.position.y+1,target.position.z+distance);
		//a = transform.position - target.position;
		//b = new Vector3(Input.GetAxisRaw("Horizontal2"),Input.GetAxisRaw("Vertical2"),a.z);
		//a+=b;
		//transform.position += a*(6/a.magnitude);

		/* Collision Code
		 * 
		 * if(Physics.Linecast(transform.position,d-transform.position)) //Travel Direction
		 *     if(Physics.Linecast(transform.position,)) //left
		 * 	   else if()	//right
		 *     else if()	//down
		 *     else if()    //up
		 *     else         //Move it up :\
		 * 
		 * 
		 * */


// using eulerAngles of rotation, particularly y and x... sin(euler) + cos(euler)

// sphere radius has to change as consequence of time...

// rotation has to change as consequence of time... 


/*
	Casting raycast out of X+ or X- depending on Input.GetAxis("Horizontal2").
	-	A very SHORT raycast. Consider the spatial "presence" you would want your camera to have 
	Can treat this EXACTLY like slope, moving camera along normal.

	Alternative.. Have camera CATCH colliders, until Player2 moves away, or TUGS camera Cstick. This could have the interesting effect
	of something akin to having to push aside branches while moving swiftly.

	To deal with DOWNS... Do not allow rotation.X to exceed a certain amount. Camera will do what it does as above, but disallow it from
	moving over the ground to the point of being directly underneath (or past) Luya. Do not move below 319.

	With UPS... do not allow to exceed 64. 


	
	
	
	
	
	
	
 */

/*
						b=a;
						a=target.position;
						b=a-b;
						tr.Translate(b,Space.World);	// Translation which follows Player2's movements
						if(Player2.charState==Player2.CharacterState.Strafe){
							b=new Vector3(Input.GetAxisRaw("Horizontal2"),Input.GetAxisRaw("Vertical2"),distance);
							d=new Vector3(b.x,b.y,b.magnitude-distance)*sizeFactor;}
						else
						{	b=new Vector3(Input.GetAxisRaw("Horizontal2")/2.5F,Input.GetAxisRaw("Vertical2")/2.5F,distance);
							d=new Vector3(b.x,b.y,b.magnitude-distance)*sizeFactor;}
						
						tr.Translate(d); // here is the problem???
							*/






// if(eulerAngles.x == 0)
//		amt = maxTranslate;


// else
// 		amt = maxTranslate *  sin( angleBetween(Distance(Player2.transform.position - tr.position),
//								 		   Distance(enemy.transform.position-Player2.transform.position)) ) 
//										   / cos(eulerAngles.x);

// convert position to world... rotate... (ONLY DO THIS ONCE, i think... )

// slerp until worldPos is at new Vector3(worldPos.x+amt,worldPos.y,worldPos.z);

// add character movement translation, repeat!


// To take care of the problem where the camera rotates/moves such that Player2 is barely in frame, 
// allow check of ratio of (Player2-enemy) to cos(eulerAngles.x)
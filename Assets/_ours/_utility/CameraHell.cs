using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Ideas: 	 Have a slow/medium way the camera "switches" to look at things (hallucinations).
//           AND a faster one for NPC interactions. Way in which camera & character movements behave act the same in either,
//			 with the exception of speed.
//			 Targeting should be towards "important part" of enemies (not always their feet :\)
//			 Right Analog jerks switch targets
//           A common use for LateUpdate would be a following third-person camera. 
// 			 If you make your character move and turn inside Update, you can perform all camera movement and rotation calculations in LateUpdate. 
// 			 This will ensure that the character has moved completely before the camera tracks its position.
//			 eulerAngles.x to be NOT greater than +/- 67

public class CameraHell : MonoBehaviour {

#region Declarations
	public static Transform target;
	public static Transform tr;
	public static Transform sceneCam;
	public static bool eventFlag = false;
	public static float sizeFactor = 1;
	public static Vector3 a;
	public float distance;
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
	Vector3 oldDist, current;
	float maxTranslate;
	float amt;
	float timer;
	float horiz;
	float vertic;
	int i;
	public bool entrance = false;
	public bool arranging = false;
	bool firstMode = false;
	public static bool isTiny = false;
	public Transform enemy;
	List <Transform> enemies;
	Transform targetedEnemy;
	RaycastHit left;
	RaycastHit right, up, down, back;
#endregion
	
    void Start ()
    {
		camera = GetComponent<Camera>();
		//if(!SaveState.startScreen){
			tr = transform;
            offset = Vector3.zero;
			yada = GameObject.FindWithTag("Player");
			target = yada.transform;
			if (!CameraTitleScreen.isitCool) {
				CameraTitleScreen.isitCool = true;
            }
			if (!firstMode) {
				tr.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
				tr.LookAt(target);
				tr.position += new Vector3(0, 4.5F, 0);
            } else {
                tr.position = new Vector3(target.position.x + 0.35F, target.position.y + 3.4F, target.position.z);
            }
			a = target.position;
			c = Vector3.zero;
			maxTranslate = (float) 2 * Mathf.Sin(59) * distance;
			enemies = new List<Transform>();
	}
	
	void FixedUpdate ()
    { 
        posPast = tr.position;
		if (!eventFlag) {
			if (!SaveState.startScreen) {
				if (!firstMode && !Player.startFlag) {
					if (!entrance) {
                        entrance = true;
                        enemies.Clear();
                    }
					
                    posPast = tr.position;
					tr.position += Player.camOffset;
					
                    if (tr.localEulerAngles.x < 270 && tr.localEulerAngles.x > 50 && vertic > 0)
						vertic=0;
					if (tr.localEulerAngles.x > 270 && tr.localEulerAngles.x < 314 && vertic < 0)
						vertic=0;
					b = new Vector3(horiz / 2F, vertic / 2.5F, 0);
					offset = tr.position + tr.InverseTransformDirection(b) - posPast;
					tr.Translate(b);
							
					/*else 
					{	//collision check... if collider... do the z-trick above
						tr.position+=Player.moveDirection;}*/
						/*if(distance<6.4){
								distance+=.06F;
								tr.LookAt(target);tr.Translate(new Vector3(0,0,(tr.position-target.position).magnitude-distance)*sizeFactor);}*/
					/*if((tr.position+NewMovement-target.position).sqrMagnitude<=41.1F)
					{	tr.Translate(NewMovement);tr.LookAt(target);}*/
                }
				// Debug.Log((tr.position+NewMovement-target.position).sqrMagnitude);
            }
        } else {	
			// tr.position=sceneCam.position;
			// tr.rotation=sceneCam.rotation;
        }
	}
	
	void Update ()
    { 
		horiz = Input.GetAxis("Horizontal2");
        vertic = Input.GetAxis("Vertical2");
	}
}



   
   
   

















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
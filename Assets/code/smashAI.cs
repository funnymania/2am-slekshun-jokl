using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*	slekshun:	first jump to surface leads to going downwards for some reason

							it must "discern" in midair which direction you will be moving, whether you will throw out
								an attack, what your DI will be, what its own DI should be,
											if you are on the ground, whether you will jump
					

				Pick your color.
				[ ]
				if(favCol==Color.Black)
					Densil favors fs.
				else Densil picks 0s.
		
*/

public class smashAI : MonoBehaviour {
	#region Declarations
	public enum CharacterState {
	Idle = 0,
	Walking = 1,
	Trotting = 2,
	Running = 3,
	Sprint = 4,
	Jumping = 5,
	Upslope = 6,
	Downslope = 7,
	DownslopeRun = 8,
	Climb = 9,
	Strafe = 16,
	Sitting = 17,
	Respawn = 18,
	Attack = 20,
	Stammer = 21,
	Tattle = 22,
	Leap = 24,
	Roll = 25,
	Ledge = 26,
	CallBack = 27,
	Skip = 28,
	WhileDoing = 30,
	Jump = 31,
	Hurt = 32,
	Conjure = 33,
	Turning = 34
}
	public enum PainType {
		VeryLight = 1,
		Light = 2,
		Med = 3,
		Heavy = 4,
		VeryHeavy = 5
}
	
	public static CharacterState charState = CharacterState.Idle;
	PainType painType,inPainType;
	CharacterState charStateLast = CharacterState.Idle;
	
	public static System.String ledgeType;
	public static Transform tr,deathpit;
	public static Vector3 orientVec = Vector3.zero;
	public static float hurtAmt = 0;
	public static float health = 100;
	public static bool Lshift = false;
	public static bool hurtSquib = false;
	public static bool hurtHard = false;
	public static bool hurtMed = false;
	public static bool startFlag = false;
	public static bool onLine = false;
	public static bool ledgeNearby = false;	
	public static bool vulnerable = false;
	public static bool respawning = true;
	public static bool onFaller = false;
	
	float healthBarlength;
	
	public List<GameObject> airAttack = new List<GameObject>();
	public List<GameObject> groundAttack = new List<GameObject>();
	public GameObject particle;
	public CharacterController me;
	public AudioSource aud,loopAud;
	public Transform turnTranny;
	public Transform slopeDetect;
	public RectTransform healthBar;
	public EnemyInfo enemyInfo;
	public CameraBlah cameraBlah;
	public AudioClip jump,walk,death;
	public Texture gameOver,controls;
	public int maxHealth=100;
	public AudioClip[] footValokaas;
	public AudioClip Spawn;
	
	List<float> whereat=new List<float>();
	double camRotor,camRotorPast,camRotorOffset,skidRotor,
		   skidRotorLast,skidRotorSumUp,strafeRotor,strafeRotor2;
	int i, idleState;
	float chance;
	float atkTimer;
	float endurance=100;
	float endurVelocity = 0;
	float endurScalar = 1;
	float charSpeed=0;
	float breathAmt = 2.3F;
	float timey = 0;
	float SQ = 10;
	float horiz;
	float vertic;
	float distance;
	float slopeChanger;
	float transAnimTime;
	float timePastFixed;
	float ftimer = 0,strafeTimer = 0,strafeTimer2 = 0;
	float halltimer = 0;
	float dashDanceTimer = 0;
	float weaponCoolDown = 8;
	float magicMax = 100;
	float pain = 0;
	float gameOverTimer = 0;
	float lastHoriz,lastVertic;
	float jumpHoriz,jumpVertic;
	float horiz2,vertic2;
	float downDistanceLast;
	float reactTime=0,thoughtTimer=0,distBtwn=0;
	float laterDist,initDist=0;
	float jumpInput,attackInput;
	public float bellamy;
	public float tinyDude=1;
	public static float timer = 0;
	public float fallTimer = 0;
	public float accelTimer = 0;
	public float hideTimer = 0;
	public float breathTimer = 0;
	public float strafeJumpTimer = 0;
	public float upslope = 0;
	public float downslope = 0;
	public float vecAdd = 0;
	public float vecAdd2 = 0;
	public float slopeCounter = 0;
	public float footTime = 0;
	public float cloudTimer = 0;
	public GameObject CloudMain;
	public GameObject Cloud;
	public GameObject Cloud2;
	public GameObject CloudPlatform;
	Transform dumTr;
	Transform blockPos;
	RaycastHit downwards,forwards,slopewards,upwards;
	RaycastHit ledge;
	RaycastHit ledgey;
	RaycastHit[] body;
	GameObject[] cloudMats;
	Animator matAn;
	MeshRenderer cloudMatMesh;
	MeshCollider cloudMatCollide;
    public static Vector3 moveDirection = Vector3.zero;
	public static Vector3 camOffset = Vector3.zero;
    public static Vector3 rootOffset = Vector3.zero;
	Vector3 gravity = new Vector3(0,-21F,0);
	Vector3 playersWill = Vector3.zero;
	Vector3 surface = Vector3.zero;
	Vector3 velocity = Vector3.zero;
	Vector3 burstVel = Vector3.zero;
	Vector3 accel = Vector3.zero;
	Vector3 slope;
	Vector3 footCheck;
	Vector3 movingPlatformOffset;
	Vector3 movingPlatformPast;
	Vector3 rootPast,origPosition,rootOrig,camOffsetPast;
	Vector3 transy;
	Vector3 dud;
	Vector3 dumTrForward;
	Vector3 downwardsLast;
	Vector3 blockPosLast;
	Vector3 oldPosition;
	public static Vector3 imHit = Vector3.zero;
	private Vector3 lastPosition = Vector3.zero;
	double rotor=0;
	double rotorLast=0;
	double lastCameraRotor = 0;
	double skipRotor=0;
	public Transform camRotation;
	public Transform Colliders1;
	public Transform Breath;
	public Transform Ledger;
	public Transform Claw,Shard,Dagger;
	Animation an;
	Animator meshan;
	Animator cl1;
	Animator cl2;
	GameObject clone, clone2;
	ParticleSystem breathing;
	public static bool loopOver = false;
	public bool justEntered = true;
	public bool blurb = true;
	bool isStrafe = false;
	bool isWalk = false;
	bool walkDepressed = true;
	bool isFalling = true;
	bool isIdle=true;		// Also for sitting? Or maybe sitting can make you go out of your body?
	bool hardStrafe=false;
	bool amDead = false;
	bool isStammer = false;
	bool entrance = true;
	bool strafeSwitch = false;
	bool offBreath = false;
	bool leaper = false; // flipped to false upon leapTimer > 0.2
	bool leaping = false;
	bool onCloud = false;
	bool onCloud4real = false;
	bool cloudOut = false;
	bool footStepsStop = false;
	bool gameStart = true;
	bool leftFootDown = false;
	bool rightFootDown = true;
	bool altDown = false;
	bool altDepressed = false;
	bool footDown = false;
	bool switchbird = false;
	bool leapSwitch = false;
	bool animSwitch = false;
	bool daggerOut = false;
	bool attackDown,attackDown2;
	bool startDown = false;
	bool start4Real = false;
	bool loader = false;
	bool strafing = false;
	bool straightDown = false;
	bool slopeCorrect = false;
	bool isThereFloorAtAll = true;
	bool kontakt = false;
	bool jumpDone = false;
	bool isSpace = false;
	bool isSpaceDown = false;
	bool spacing = false;
	bool jumpInAir = false;
	bool jumpBack = true;
	bool inAir = false;
	bool committed = false;
	bool isHit=false;
	bool ahHa=false;
	bool straightUp=false;
	int coinflip;
	float distCounter=0;
	
	/* AI Variables */
	Vector3 oppPastPos,AIpastPosition;
	float reactTimer;
	int baiting;
	bool fairCommit,waitCommit;
	
	#endregion
	void Start() {
		particle.SetActive(false);
		breathing=Breath.GetComponent<ParticleSystem>();
		tr=transform;
		meshan=GetComponent<Animator>();
		downwardsLast=new Vector3(0,1,0);
		/*cl1=Cloud.GetComponent<Animator>();
		cl2=Cloud2.GetComponent<Animator>();
		Cloud.SetActive(false);
		Cloud2.SetActive(false);*/
		//an["Attack"].speed=4;
		//an["Strafe"].speed=0.11F;
		//an["WalkHandsy"].speed=2.5F;
		timer=0;
		meshan.Play("idleLeft");
		playersWill=Vector3.zero;
		rootPast=tr.position;
		body=new RaycastHit[8];
		endurVelocity=20;
		chance=Random.Range(2,6)*2.667F;
		idleState=0;skidRotor=0;
		camRotor=camRotation.eulerAngles.y*Mathf.PI/180;
		camOffsetPast=tr.position;
		me=GetComponent<CharacterController>();
		orientVec=new Vector3(0,-1,0);
	}
	void FixedUpdate(){
		timey=Time.deltaTime;
		downwards=new RaycastHit();ledge=new RaycastHit();ledgey=new RaycastHit();
		camOffset=tr.position-camOffsetPast;
		if(respawning){
			charState=CharacterState.Idle;meshan.Play("idleLeft");loopAud.Stop();}
								Debug.DrawRay(turnTranny.position,turnTranny.forward);	
		straightDown=Physics.Raycast(turnTranny.position,orientVec,out downwards,.98F,~0,QueryTriggerInteraction.Ignore);
		straightUp=Physics.Raycast(turnTranny.position,orientVec*-1,out upwards,.98F,~0,QueryTriggerInteraction.Ignore);
		slopeCorrect=Physics.Raycast(slopeDetect.position,orientVec,out slopewards,.98F,~0,QueryTriggerInteraction.Ignore);
		if(downwards.transform!=null){
			if(downwards.transform.name=="deathpit") straightDown=false;
			/*if(downwards.collider.isTrigger) straightDown=false;*/}
		if(slopewards.transform!=null)
			if(slopewards.transform.name=="deathpit") slopeCorrect=false;
		if((straightDown || (slopeCorrect && !isFalling)) && !jumpDone && !enemyInfo.isHit){camOffsetPast=tr.position;
			if(isThereFloorAtAll){
				if(isFalling){ // dust particle here!
					accel.y=0;velocity.y=0;camOffsetPast=tr.position;leaping=false;
					isFalling=false;jumpDone=false;jumpInAir=false;//meshan.SetBool("isGrounded",true);
					if(gameStart){
						//particle.SetActive(true);aud.PlayOneShot(Spawn,0.125F);
						gameStart=false;}}
				else
				{	if(kontakt) kontakt=false;
					//collision and slope accompanying
					if(straightDown){respawning=false;
						if(downwards.transform.tag=="block" || downwards.transform.tag=="faller"){
							if(downwards.transform!=movingObject.mover){
								movingObject.mover=downwards.transform;
								movingObject.past=movingObject.mover.position;}
							if(!onLine){
								movingObject.mover=downwards.transform;
								movingObject.past=movingObject.mover.position;}
							onLine=true;}
						else
						{	onLine=false;
							if(movingObject.mover!=null)
								movingObject.mover.position=Vector3.zero;
							movingObject.past=Vector3.zero;}
						
						downDistanceLast=downwards.distance;downwardsLast=downwards.normal;
						orientVec=downwards.normal*-1;
						slope=Vector3.Cross(Vector3.Cross(downwards.normal,camOffset),downwards.normal);
						if(downwards.distance==0){
							if(!Lshift) slope.y-=.004F;
							else slope.z+=.004F;}
						else /* should be correcting only if below a certain height, and then it
								should be correcting to just BELOW that height (should never be falling)*/
						{	if(.98F-downwards.distance>.004F && !switchbird){
								if(!Lshift){
									slope.y+=.976F-downwards.distance;switchbird=true;}
								else
								{	slope.z-=.976F-downwards.distance;switchbird=true;}}
							else switchbird=false;}
							if(charState!=CharacterState.Skip  && downwards.transform.tag=="floor"){
								if(!Lshift){
									if(!kontakt){
										me.Move(new Vector3(0,slope.y,0));}
										//turnTranny.rotation=Quaternion.FromToRotation(orientVec,downwards.normal);
									else tr.position+=new Vector3(0,slope.y,0);}
								else 
								{	if(!kontakt){
										me.Move(new Vector3(0,0,slope.z));}
										//turnTranny.rotation=Quaternion.FromToRotation(orientVec,downwards.normal);
									else tr.position+=new Vector3(0,0,slope.z);}}}
					else 
					{	respawning=false;
						if(slopewards.transform.tag=="block" || slopewards.transform.tag=="faller"){
							if(slopewards.transform!=movingObject.mover){
								movingObject.mover=slopewards.transform;
								movingObject.past=movingObject.mover.position;}
							if(!onLine){
								movingObject.mover=slopewards.transform;
								movingObject.past=movingObject.mover.position;}
							onLine=true;}
						else
						{	onLine=false;
							if(movingObject.mover!=null)
								movingObject.mover.position=Vector3.zero;
							movingObject.past=Vector3.zero;}
						orientVec=slopewards.normal*-1;
						slope=Vector3.Cross(Vector3.Cross(slopewards.normal,camOffset),slopewards.normal);
						if(slopewards.distance==0){
								if(!Lshift) slope.y-=.004F;
								else slope.z+=.004F;}//difference between slopewards.distance && downwards.lastDistance
							else
							{	if(.98F-slopewards.distance>.004F && !switchbird){
									if(!Lshift){
										slope.y+=.98F-slopewards.distance-.224F;switchbird=true;}
									else 
									{	slope.z-=.98F-slopewards.distance-.224F;switchbird=true;}}
								else switchbird=false;}
							if(charState!=CharacterState.Skip  && slopewards.transform.tag=="floor"){
								if(!Lshift){
									if(!kontakt) me.Move(new Vector3(0,slope.y,0));
									else tr.position+=new Vector3(0,slope.y,0);}
								else 
								{	if(!kontakt) me.Move(new Vector3(0,0,slope.z));
									else tr.position+=new Vector3(0,0,slope.z);}}}
					
					// Shooting up
						if(charState==CharacterState.Jump && jumpBack){
							if(horiz!=0 || vertic!=0){
								me.Move(new Vector3(turnTranny.forward.x/10,0,turnTranny.forward.z/10));}
							accel=turnTranny.up*400;velocity=Vector3.zero;
							velocity+=accel*Time.deltaTime;//i completely want to kill myself
							moveDirection=(accel/2*Time.deltaTime+velocity)*Time.deltaTime;//but unfortunately
							me.Move(moveDirection);jumpInAir=true;
							aud.PlayOneShot(jump);
							jumpDone=true;isFalling=true;jumpBack=false;}//icannot
			}}
		}
				
		else if(charState!=CharacterState.Ledge && charState!=CharacterState.Climb)
		{	camOffsetPast=tr.position;onLine=false;
			if(charState==CharacterState.Leap){
				if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Leap") && leapSwitch){
					accel=new Vector3(0,-3.1F,0);leapSwitch=false;velocity=rootOffset*5;Debug.Log("butt");}
				else accel=Vector3.zero/*+new Vector3(0,-21,0)+surface*/;}
			if(charState==CharacterState.Jump || (charStateLast==CharacterState.Jump && 
												charState==CharacterState.Attack)) //Must be moving y-down RELATIVE to normal axis.
			{	if(!enemyInfo.isHit){
					if(horiz!=0 || vertic!=0){
						me.Move(new Vector3(turnTranny.forward.x/8,0,turnTranny.forward.z/8));}
					if(!jumpInAir){
						accel=turnTranny.up*400;velocity=Vector3.zero;
						velocity+=accel*Time.deltaTime;//i completely want to kill myself
						moveDirection=(accel/2*Time.deltaTime+velocity)*Time.deltaTime;//but unfortunately
						me.Move(moveDirection);jumpInAir=true;
						jumpDone=true;isFalling=true;jumpBack=false;
						aud.PlayOneShot(jump);
					}
					else
					{	accel=downwardsLast*-19.1F;
						velocity+=accel*Time.deltaTime;
						moveDirection=(accel/2*Time.deltaTime+velocity)*Time.deltaTime;
						moveDirection*=tinyDude;
						if(moveDirection.y<0)
							jumpDone=false;
						me.Move(moveDirection);}}
				else
				{	accel=(enemyInfo.theirKB*enemyInfo.health*enemyInfo.theirAngle)+downwardsLast*-19.1F;
					velocity+=accel*Time.deltaTime;
					moveDirection=(accel/2*Time.deltaTime+velocity)*Time.deltaTime;
					moveDirection*=tinyDude;
					if(moveDirection.y<0)
						jumpDone=false;
					me.Move(moveDirection);}}
			else
			{	if(!enemyInfo.isHit){
					if(horiz!=0 || vertic!=0){
						me.Move(new Vector3(turnTranny.forward.x/8,0,turnTranny.forward.z/8));}
					accel=new Vector3(0,-1,0)*19.1F;
					velocity+=accel*Time.deltaTime;
					moveDirection=(accel/2*Time.deltaTime+velocity)*Time.deltaTime;
					moveDirection*=tinyDude;
					tr.position+=moveDirection;}
				else
				{	accel=(enemyInfo.theirKB*enemyInfo.theirAngle)+new Vector3(0,-1,0)*19.1F;;
					velocity+=accel*Time.deltaTime;
					moveDirection=(accel/2*Time.deltaTime+velocity)*Time.deltaTime;
					moveDirection*=tinyDude;
					if(moveDirection.y<0)
						jumpDone=false;
					me.Move(moveDirection);}}
			isFalling=true;
			rootOffset=tr.position-rootPast;
			rootPast=tr.position;
			/*if(!Lshift) orientVec=new Vector3(0,-1,0);
			else orientVec=new Vector3(0,0,1);*/
			if(enemyInfo.isHit){
				 if(moveDirection.y<0)
					 enemyInfo.isHit=false;}}
		else camOffsetPast=tr.position;
	}
    void Update() {
		if(!EventHandler.eventFlag){
			timey=Time.deltaTime;
			horiz2=Input.GetAxis("Horizontal2");vertic2=Input.GetAxis("Vertical2");
			camRotorPast=camRotor;camRotor=camRotation.eulerAngles.y;
			camRotorOffset=camRotor-camRotorPast;
			coinflip=Random.Range(0,2);
			healthBar.sizeDelta=new Vector2(enemyInfo.health,8);
			if(reactTimer==5){ //Time not frames
				if(!isFalling){
					ahHa=false;
					if((oppPastPos-AIpastPosition).sqrMagnitude>Mathf.Pow(23.35F,2)
							&& (Player.tr.position-AIpastPosition).sqrMagnitude<Mathf.Pow(23.35F,2))
							baiting++;
					if(baiting>3){
						turnTranny.LookAt(new Vector3(Player.tr.position.x,turnTranny.position.y,Player.tr.position.z));
						horiz=turnTranny.forward.x/-8;vertic=turnTranny.forward.z/-8;
						me.Move(new Vector3(horiz,0,vertic));
						baiting=0;ahHa=true;}
					else if(!Player.isFalling){
						if((oppPastPos-AIpastPosition).sqrMagnitude >= (Player.tr.position-AIpastPosition).sqrMagnitude){
							if((tr.position-Player.tr.position).sqrMagnitude-turnTranny.forward.x/8<Mathf.Pow(23.35F,2))
								jumpInput=1;ahHa=true;horiz=0;vertic=0;}
						else if(baiting==0){
							horiz=0;vertic=0;jumpInput=1;ahHa=true;}
						else if((tr.position-Player.tr.position).sqrMagnitude-turnTranny.forward.x/8<Mathf.Pow(23.35F,2)){
							ahHa=true;
							attackInput=1;}}
					else
					{	if(Player.charState==Player.CharacterState.Attack){
							if((tr.position-Player.tr.position).sqrMagnitude-
								((-19.1F/2*(0.6F-atkTimer)+(Player.velocity.magnitude+(3*-19.1F*(0.6F-atkTimer))))*(0.6F-atkTimer))
									- (-19.1F/2*(0.6F-atkTimer))+(Player.velocity.magnitude+(400-(2*-19.1F)*(0.6F-atkTimer)))*(0.6F-atkTimer)>0){
								fairCommit=true;//move forward and commit to a fair
								jumpInput=1;
								turnTranny.LookAt(new Vector3(Player.tr.position.x,turnTranny.position.y,Player.tr.position.z));
								ahHa=true;
								horiz=turnTranny.forward.x/8;vertic=turnTranny.forward.z/8;
								me.Move(new Vector3(horiz,0,vertic));}
							else if((-19.1F/2*(0.6F-atkTimer)+(Player.velocity.magnitude+(3*-19.1F*(0.6F-atkTimer))))*(0.6F-atkTimer)<0){//if it would hit the ground before the attack completes
								ahHa=true;
								horiz=0;vertic=0;/* Wait. */}}
						if((tr.position-Player.tr.position).sqrMagnitude-turnTranny.forward.x/8<Mathf.Pow(9.45F,2)
							&& (tr.position-Player.tr.position).sqrMagnitude < 169){
							ahHa=true;attackInput=1;horiz=0;vertic=0;}
					}
				}
				else
				{	ahHa=false;
					if(Player.tr.position.y>tr.position.y && (tr.position-Player.tr.position).sqrMagnitude < 169){
						turnTranny.LookAt(new Vector3(Player.tr.position.x,turnTranny.position.y,Player.tr.position.z));
						ahHa=true;
						horiz=turnTranny.forward.x/-8;vertic=turnTranny.forward.z/-8;
						me.Move(new Vector3(horiz,0,vertic));}
					if((tr.position-Player.tr.position).sqrMagnitude < 169){
						attackInput=1;ahHa=true;}
					if((tr.position-Player.tr.position).sqrMagnitude-turnTranny.forward.x/4<Mathf.Pow(9.45F,2)){
						waitCommit=true;ahHa=true;camOffset=Vector3.zero;}
					if(baiting<2){
						turnTranny.LookAt(new Vector3(Player.tr.position.x,turnTranny.position.y,Player.tr.position.z));
						horiz=turnTranny.forward.x/8;vertic=turnTranny.forward.z/8;
						me.Move(new Vector3(horiz,0,vertic));
						ahHa=true;}
				}
				reactTimer=0;}
			if(!ahHa){
				if(thoughtTimer>0.5F){//Debug.Log(initDist-laterDist);
					if(committed){
						committed=false;}
					/*whereat.Remove(0);
					whereat.Add(distBtwn);*/
					laterDist=(oldPosition-Player.tr.position).sqrMagnitude;
					if(laterDist-initDist>10){
						committed=true;
						turnTranny.LookAt(new Vector3(Player.tr.position.x,turnTranny.position.y,Player.tr.position.z));
						horiz=turnTranny.forward.x/8;vertic=turnTranny.forward.z/8;
						me.Move(new Vector3(horiz,0,vertic));
						/*if(turnTranny.eulerAngles.y>=315 || turnTranny.eulerAngles.y<=45){
							charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("in");}
						else if(turnTranny.eulerAngles.y<=315 && turnTranny.eulerAngles.y<225){
							charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("right");}
						else if(turnTranny.eulerAngles.y<=225 && turnTranny.eulerAngles.y<135){
							charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("out");}
						else
						{	charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("left");}
						loopAud.clip=walk;
						loopAud.Play();*/}
					/*else if(turnTranny.eulerAngles.y<360 && turnTranny.eulerAngles.y>180)
					{	meshan.Play("idleRight");}
					else
					{	meshan.Play("idleLeft");}*/
					oldPosition=tr.position;
					initDist=(oldPosition-Player.tr.position).sqrMagnitude;
					thoughtTimer=0;}
				else
				{	if(committed){
						turnTranny.LookAt(new Vector3(Player.tr.position.x,turnTranny.position.y,Player.tr.position.z));
						horiz=turnTranny.forward.x/8;vertic=turnTranny.forward.z/8;
						me.Move(new Vector3(horiz,0,vertic));
						/*if(turnTranny.eulerAngles.y>=315 || turnTranny.eulerAngles.y<=45){
							//charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("in");}
						else if(turnTranny.eulerAngles.y<=315 && turnTranny.eulerAngles.y<225){
							//charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("right");}
						else if(turnTranny.eulerAngles.y<=225 && turnTranny.eulerAngles.y<135){
							//charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("out");}
						else
						{	//charState=CharacterState.Trotting;
							endurVelocity=4;breathAmt=1.5F;
							timer=0;meshan.Play("left");}*/}
						thoughtTimer+=timey;}}
			else if(fairCommit){
				if((tr.position-Player.tr.position).sqrMagnitude < 169)
					attackInput=1;
				horiz=turnTranny.forward.x/8;vertic=turnTranny.forward.z/8;
				me.Move(new Vector3(horiz,0,vertic));}
			else if(waitCommit){
				if(!isFalling){
					attackInput=1;
					waitCommit=false;ahHa=false;}
				if((tr.position-Player.tr.position).sqrMagnitude<Mathf.Pow(9.45F,2)){
					turnTranny.LookAt(new Vector3(Player.tr.position.x,turnTranny.position.y,Player.tr.position.z));
					horiz=turnTranny.forward.x/-8;vertic=turnTranny.forward.z/-8;
					me.Move(new Vector3(horiz,0,vertic));
					waitCommit=false;ahHa=false;}
				}
			//else if(backTrackCommit) 
			reactTimer++;
			oppPastPos=Player.tr.position;
			AIpastPosition=tr.position;
			
			
					#region Rotation
			// + -
			// - +
			/*if(charState!=CharacterState.Respawn && charState!=CharacterState.Roll && charState!=CharacterState.CallBack
			&& charState!=CharacterState.Ledge
			&& charState!=CharacterState.WhileDoing && charState!=CharacterState.Conjure && charState!=CharacterState.Sitting){
				if(horiz!=0){
					rotorLast=rotor;
					rotor=Mathf.Atan(vertic/horiz);
					rotor*=180/Mathf.PI; // <-- Capture it here
					skidRotorLast=skidRotor;
					
					if(vertic<0 && horiz<0)
						rotor+=180;
					else if(vertic>0 && horiz<0)
						rotor+=180;
					else if(horiz<0)
						rotor=180;
					if(strafing){
						strafeRotor=rotor;strafing=false;}
					if(charState!=CharacterState.Strafe){
						skidRotor=rotor;
						rotor+=camRotation.eulerAngles.y-90;
						if(rotor>359)
							rotor-=360;
						if(charState==CharacterState.Skip){
							tr.eulerAngles=new Vector3(0,(float)(rotor),0);
							if(horiz>0) dumTrForward=tr.forward;
							else dumTrForward=tr.forward*-1;
							tr.eulerAngles=new Vector3(0,(float)(skipRotor),0);}
						else turnTranny.eulerAngles=new Vector3(turnTranny.eulerAngles.x,(float)(rotor),turnTranny.eulerAngles.z);}
					else
					{	strafeRotor2=strafeRotor+camRotation.eulerAngles.y-90;
						if(strafeRotor2>359)
							strafeRotor2-=360;
						tr.eulerAngles=new Vector3(0,(float)(strafeRotor2),0);}}
				else if(charState!=CharacterState.Strafe){
					if(vertic>0){
						turnTranny.eulerAngles=new Vector3(0,(float)camRotation.eulerAngles.y,0); // rotor = camRotation.y
						skidRotor=90;
						if(charState==CharacterState.Skip){
							dumTrForward=tr.forward;
							tr.eulerAngles=new Vector3(0,(float)(skipRotor),0);}}
					else if(vertic<0){
						turnTranny.eulerAngles=new Vector3(0,(float)(camRotation.eulerAngles.y-180),0);
						skidRotor=270;
						if(charState==CharacterState.Skip){
							dumTrForward=tr.forward*-1;
							tr.eulerAngles=new Vector3(0,(float)(skipRotor),0);}
						}}}*/
				#endregion
															#region Idle
			if(charState==CharacterState.Idle)
			{		if(timer<=chance)
						timer+=timey;
					/*/	Idle State-Changes
					else if(idleState==0)
					{	chance=6.167F;idleState=1;
						an["OneArmBehindBack"].time=an["OneArmBehindBack"].length;
						meshan.CrossFade("NotHere",0.4F);timer=0;}
					else if(idleState==1)
					{	chance=Random.Range(2,6)*2.667F;idleState=0;
						meshan.CrossFade("Take 001",1);timer=0;}
					else if(idleState==1)
					{	chance=Random.Range(2,6)*an["BreatheArmBehindBack"].length;idleState=2;
						an.Play("BreatheArmBehindBack");timer=0;}
					else if(idleState==2)
					{	chance=an["OneArmBehindBack"].length;idleState=3;
						an["OneArmBehindBack"].speed=-1;an["OneArmBehindBack"].time=an["OneArmBehindBack"].length;
						an.Play("OneArmBehindBack");timer=0;}
					else if(idleState==3)
					{	chance=Random.Range(2,6)*an["Take 001e"].length;idleState=0;
						meshan.CrossFade("Take 001e",1);timer=0;}
				 breathe for whatever... switch to NotHere */
				if(enemyInfo.inHitstun)
					charState=CharacterState.Hurt;
				else if(horiz!=0 || vertic!=0) /*&& meshan.GetCurrentAnimatorStateInfo(0).IsName("Idle")*/{
					/*if(horiz>.6F){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("left");}
					else if(horiz<-.6F){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("right");}
					else if(vertic>.6F){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("in");}
					else if(vertic<-.6F){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("out");}
					else */if(horiz>0)
					{	charStateLast=CharacterState.Idle;isWalk=false;timer=0;
						charSpeed=160;endurVelocity=18;entrance=true;
						meshan.Play("right");
						charState=CharacterState.Trotting;
						endurVelocity=4;breathTimer=0;
						timer=0;}
					else if(horiz<0)
					{	charStateLast=CharacterState.Idle;isWalk=false;timer=0;
						charSpeed=160;endurVelocity=18;entrance=true;
						meshan.Play("left");
						charState=CharacterState.Trotting;
						endurVelocity=4;breathTimer=0;
						timer=0;}
					else if(vertic>0)
					{	charStateLast=CharacterState.Idle;isWalk=false;timer=0;
						charSpeed=160;endurVelocity=18;entrance=true;
						meshan.Play("in");
						charState=CharacterState.Trotting;
						endurVelocity=4;breathTimer=0;
						timer=0;}
					else if(vertic<0)
					{	charStateLast=CharacterState.Idle;isWalk=false;timer=0;
						charSpeed=160;endurVelocity=18;entrance=true;
						meshan.Play("out");
						charState=CharacterState.Trotting;
						endurVelocity=4;breathTimer=0;
						timer=0;}
					/*loopAud.clip=walk;
					loopAud.Play();*/
				}
				else if(jumpInput==1){
					leaping=true;timer=0;endurVelocity=0;leaper=true;entrance=true;vulnerable=true;jumpDone=false;
					charState=CharacterState.Jump;charSpeed=88;burstVel=tr.up;
					/*meshan.Play("Jump");*/}
				else if(attackInput==1){atkTimer=0;
					charState=CharacterState.Attack;timer=0;entrance=true;
					charStateLast=CharacterState.Idle;}
				/*else if(Input.GetAxis("RBumper")!=0){
					if(cloudOut){
						Cloud.SetActive(false);Cloud2.SetActive(false);CloudPlatform.SetActive(false);
						//  play callback animation
						//  Gothroughlistofclouds, allow to finish animations' current fadeout iteration,then SetActive(false). 
						    play cloudMat animators in reverse. these animators can be default active
							anMat.speed=-1;
						foreach(GameObject cl in cloudMats){
							matAn=cl.GetComponent<Animator>();
							if(matAn!=null){
								matAn.speed=-1;matAn.Play("Take 001");}
							else 
							{	cloudMatMesh=cl.GetComponent<MeshRenderer>();
								cloudMatMesh.enabled=false;
								cloudMatCollide=cl.GetComponent<MeshCollider>();
								cloudMatCollide.enabled=false;}}
						charState=CharacterState.CallBack;timer=0;cloudOut=false;}}*/
				else if(Input.GetAxis("Alt")==1){
					/*meshan.CrossFade("Sitting",0.4F);*/altDown=true;
					altDepressed=false;charState=CharacterState.Sitting;}
				else if(weaponCoolDown>7){
					if(Input.GetAxis("Select")>0){
					}
					else if(Input.GetAxis("Select")<0){
						if(daggerOut){
							charState=CharacterState.Conjure;daggerOut=false;magicMax=100;
							entrance=true;/*meshan.Play("DaggerConjureGround");*/}
						else
						{	charState=CharacterState.Conjure;daggerOut=true;magicMax=60;
							entrance=true;/*meshan.Play("DaggerConjureGround");*/}
						/* play painful SFX */}
					else if(Input.GetAxis("Select2")<0){
					}
					else if(Input.GetAxis("Select2")>0){
					}
				}
					
				/*if(ftimer<0.4F && charStateLast==CharacterState.Walking)
					luyaMesh.position=Vector3.zero;*/ //CrossFade does not give a fuck about position changes while it is running. 
				ftimer+=timey;strafeTimer+=timey;
			}
			#endregion
									 					    #region Trotting
			else if(charState==CharacterState.Trotting)
			{   // if alt != 0 and 
				if(Input.GetAxis("Alt")==0)
					walkDepressed=false;
				/*if(!footStepsStop){
					if(an["Runcycle"].time>.917F)	
						an["Runcycle"].time-=.917F;
					if(an["Runcycle"].time>0.1082F && an["Runcycle"].time<0.5502F){
						footSteps=true;footStepsStop=true;
						footTime=.4120F+an["Runcycle"].time-0.1082F;}}
				else
				{	if(an["Runcycle"].time>0.5502F){
						footSteps=true;footStepsStop=false;
						footTime=.475F+an["Runcycle"].time-0.5502F;}}*/
				/*if(skidRotor<0) skidRotor+=360;
				if(skidRotorLast<0) skidRotorLast+=360;
				skidRotorSumUp=Mathf.Abs((float)(skidRotor-skidRotorLast));
				if(skidRotorSumUp>180) skidRotorSumUp-=180;*/
				if(enemyInfo.inHitstun){
					charState=CharacterState.Hurt;
				}
				else if(hurtSquib==true){
					charState=CharacterState.Hurt;hurtSquib=false;
					/*meshan.Play("HurtSquib");*/timer=0;dashDanceTimer=0;}
				// else if((skidRotor>=0 && skidRotor<90 && skidRotorLast<270 && skidRotorLast>=180) || 
						// (skidRotor>=90 && skidRotor<180 && skidRotorLast<360 && skidRotorLast>=270) ||
						// (skidRotor>=180 && skidRotor<270 && skidRotorLast<90 && skidRotorLast>=0) ||
						// (skidRotor>=270 && skidRotor<360 && skidRotorLast<180 && skidRotorLast>=90)){
					// /* Skidding */
					// if(dashDanceTimer>0.05F){
						// charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Trotting;
						// chance=Random.Range(2,6)*2.667F;idleState=0;dashDanceTimer=0;
						// charSpeed=0;ftimer=0;meshan.CrossFade("Take 001",0F);
						// endurVelocity=40;}
					// else{ dashDanceTimer=0;}}
				else if(attackInput==1){atkTimer=0;
						charState=CharacterState.Attack;timer=0;entrance=true;
						charStateLast=CharacterState.Trotting;dashDanceTimer=0;}
				else if(jumpInput==1){
					leaping=true;timer=0;endurVelocity=0;leaper=true;entrance=true;vulnerable=true;jumpDone=false;
					charState=CharacterState.Jump;charSpeed=88;burstVel=tr.up;loopAud.Stop();
					jumpHoriz=horiz;}
				else if(Input.GetAxis("Alt")!=0){
					if(!walkDepressed){
						charState=CharacterState.Walking;endurVelocity=18;isWalk=true;walkDepressed=true;
						meshan.CrossFade("Take 001",0.3F);dashDanceTimer=0;}} // meshan.CrossFade("WalkHandsy",0.3F);
				else if(Input.GetAxisRaw("Shift")==1 && endurance>0){
					charState=CharacterState.Running;meshan.CrossFade("RunLoop",0.2F);dashDanceTimer=0;
					meshan.speed=1;}
				// else if(Input.GetAxis("Evade")!=0 && !isStrafe){
					// charState=CharacterState.Strafe;charStateLast=CharacterState.Trotting;
					// timer=0;isStrafe=true;dashDanceTimer=0;
					// meshan.CrossFade("Limenous",0.2F);}
				/*else if(horiz>0.4F){
					meshan.Play("left");}
				else if(horiz<-0.4F){
					meshan.Play("right");}
				else if(vertic>0.4F){
					meshan.Play("in");}
				else if(vertic<-0.4F){
					meshan.Play("out");}*/
				else if(horiz==0 && vertic==0)
				{	charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Trotting;
					chance=Random.Range(2,6)*2.667F;idleState=0;
					charSpeed=0;ftimer=0;dashDanceTimer=0;
					endurVelocity=40;loopAud.Stop();
					if(Mathf.Abs(lastHoriz)>Mathf.Abs(lastVertic)){
						if(lastHoriz>0) meshan.CrossFade("idleRight",0F);
						else meshan.CrossFade("idleLeft",0F);}
					else
					{	if(lastVertic>0) meshan.Play("idleIn");
						else meshan.Play("idleOut");}}
				/* FootSteps 
				if(footDown){
					if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime%1>0.458983){
						GetComponent<AudioSource>().PlayOneShot(footValokaas[coinflip],0.4F);footDown=false;}}
				else
				{	if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime%1<0.458983){
						GetComponent<AudioSource>().PlayOneShot(footValokaas[coinflip],0.4F);footDown=true;}}*/
			}
			#endregion
															#region Running
			else if(charState==CharacterState.Running)
			{
				if(enemyInfo.inHitstun){
					charState=CharacterState.Hurt;
				}
				else if(horiz==0 && vertic==0 && endurance>0){
					charState=CharacterState.Trotting;endurVelocity=4;timer=0;meshan.speed=0.75F;
					meshan.CrossFade("TrotLoopJ",0.2F);}
				else if(Input.GetAxis("Shift")>0){	// Here we go... force stuck at constant direction because of here
					if(timer>0.15F){
						charState=CharacterState.Trotting;endurVelocity=4;timer=0;breathTimer=0;}
					else if(timer>0){
						charState=CharacterState.Sprint;charSpeed=5;meshan.speed=2;
						endurVelocity=endurScalar*-4;}}
				else if(Input.GetAxis("Shift")==0){Debug.Log(timer);
					if(timer>0.15F){
						charState=CharacterState.Trotting;endurVelocity=4;timer=0;meshan.speed=0.75F;
						meshan.CrossFade("TrotLoopJ",0.2F);}
					else timer+=Time.deltaTime;}
			}
			 #endregion
															#region Sprint
			else if(charState == CharacterState.Sprint)
			{
				if(enemyInfo.inHitstun){
					charState=CharacterState.Hurt;
				}
				else if(endurance<=0){
					charState=CharacterState.Trotting;timer=0;endurVelocity=4;} // meshan.CrossFade("RunElbows",0.3F);
				else if(horiz==0 && vertic==0){
					timer=0;
					charState=CharacterState.Running;charSpeed=22;
					endurVelocity=endurScalar*-0.6F;}
				else if(Input.GetAxis("Shift")==0){
					timer=0;meshan.speed=1.5F;
					charState=CharacterState.Running;charSpeed=22;
					endurVelocity=endurScalar*-0.6F;}
			}
			#endregion
															#region Attack
			else if(charState==CharacterState.Attack)
			{
				if(enemyInfo.inHitstun)
					charState=CharacterState.Hurt;
				
				if(inAir){
					if(!isFalling)
					{	charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Jump;
						chance=Random.Range(2,6)*2.667F;idleState=0;
						endurVelocity=40;jumpBack=true;
						if(jumpHoriz>0) meshan.Play("idleRight");
						else if(jumpHoriz<0) meshan.Play("idleLeft");}
					else if(atkTimer<0.12F){
						airAttack[0].SetActive(true);
						enemyInfo.myPain=11;
						enemyInfo.myKB=4;
						enemyInfo.myHitstun=0.7F;
						enemyInfo.myAngle=turnTranny.forward;
						enemyInfo.myAngle.y=turnTranny.forward.y/Mathf.Cos(19);} 
					else if(atkTimer<0.24F){ //just before third
						airAttack[0].SetActive(false);
						airAttack[1].SetActive(true);
						enemyInfo.myPain=11;
						enemyInfo.myKB=7;
						enemyInfo.myHitstun=0.7F;
						enemyInfo.myAngle=turnTranny.forward;
						enemyInfo.myAngle.y=turnTranny.forward.y/Mathf.Cos(4);}
					else if(atkTimer<0.36F){
						airAttack[1].SetActive(false);
						airAttack[2].SetActive(true);
						enemyInfo.myPain=11;
						enemyInfo.myKB=7;
						enemyInfo.myHitstun=0.7F;
						enemyInfo.myAngle=turnTranny.forward;
						enemyInfo.myAngle.y=turnTranny.forward.y/Mathf.Cos(4);}
					else if(atkTimer<0.48F){
						airAttack[2].SetActive(false);
						airAttack[3].SetActive(true);
						enemyInfo.myPain=11;
						enemyInfo.myKB=4;
						enemyInfo.myHitstun=0.7F;
						enemyInfo.myAngle=turnTranny.forward;
						enemyInfo.myAngle.y=turnTranny.forward.y/Mathf.Cos(19);}
					else if(atkTimer>0.6F){
						airAttack[3].SetActive(false);
						charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Trotting;
						chance=Random.Range(2,6)*2.667F;idleState=0;
						charSpeed=0;ftimer=0;dashDanceTimer=0;ahHa=false;
						endurVelocity=40;loopAud.Stop();fairCommit=false;
						if(lastHoriz>0) meshan.Play("idleRight");
						else meshan.Play("idleLeft");}}
				else
				{	if(atkTimer<0.12F){
						groundAttack[0].SetActive(true);
						enemyInfo.myPain=11;
						enemyInfo.myKB=4;
						enemyInfo.myHitstun=0.7F;
						enemyInfo.myAngle=turnTranny.forward;
						enemyInfo.myAngle.y=turnTranny.forward.y/Mathf.Cos(45);}
					else if(atkTimer<0.2F){ //just before third
						groundAttack[1].SetActive(true);
						enemyInfo.myPain=11;
						enemyInfo.myKB=9;
						enemyInfo.myHitstun=0.7F;
						enemyInfo.myAngle=turnTranny.forward;
						enemyInfo.myAngle.y=turnTranny.forward.y/Mathf.Cos(10);}
					else if(atkTimer>0.3F){ //just before third
						groundAttack[0].SetActive(false);
						groundAttack[1].SetActive(false);
						charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Attack;
						chance=Random.Range(2,6)*2.667F;idleState=0;
						charSpeed=0;ftimer=0;dashDanceTimer=0;
						endurVelocity=40;loopAud.Stop();
						if(lastHoriz>0) meshan.Play("idleRight");
						else meshan.Play("idleLeft");}}
				atkTimer+=timey;
			}
			#endregion
															#region Walking
			else if(charState==CharacterState.Walking)
			{
				if(isWalk){
					if(Input.GetAxis("Alt")==0)
						walkDepressed=false;
					else if(Input.GetAxis("Alt")==1 && !walkDepressed){
						isWalk=false;walkDepressed=true;}}
				else if(Input.GetAxis("Alt")!=0 && !walkDepressed)
					isWalk=true;
				/*
				if(!footStepsStop){
					if(an["Walk"].time> someamt ){
						footSteps=true;footStepsStop=true;}}
				else 
				{	if(an["Walk"].time>/* someamt ){
						footSteps=true;footStepsStop=false;}}
				*/
				if(charStateLast==CharacterState.Idle)
					timer+=timey;
				if(enemyInfo.inHitstun)
					charState=CharacterState.Hurt;
				/*else if(hurtSquib==true){
					charState=CharacterState.Hurt;hurtSquib=false;
					/*meshan.Play("HurtSquib");timer=0;}*/
				else if(Input.GetAxis("Roll")==1){
					charState=CharacterState.Roll;endurance-=20;timer=0;
					charSpeed=48;/*meshan.CrossFade("Rolling",0.3F);*/}
				else if(Input.GetAxis("Attack")==1){
					charState=CharacterState.Attack;/*meshan.CrossFade("Attack",0.3F);*/}
				else if(Input.GetAxis("Evade")!=0 && !isStrafe){
					charState=CharacterState.Strafe;isWalk=false;charStateLast=CharacterState.Walking;
					isStrafe=true;
					/*meshan.CrossFade("Limenous",0.2F);*/}
				else if((horiz>0.6F || vertic>0.6F
						 || horiz<-0.6F || vertic<-0.6F) && !isWalk){
						footDown=true;charState=CharacterState.Trotting;breathAmt=1.5F;
					/*if(meshan.GetCurrentAnimatorStateInfo(0).IsName("WalkEnter")){ //Kick off Left (at the moment)
						charState=CharacterState.Trotting;
						endurVelocity=4;breathTimer=0;
						timer=0;meshan.CrossFade("TrotEnterJ",0.2F);}
					else if(meshan.GetCurrentAnimatorStateInfo(0).IsName("WalkLoop")){ //Kick off Right
						charState=CharacterState.Trotting;
						endurVelocity=4;breathTimer=0;
						timer=0;meshan.CrossFade("TrotEnterJ",0.2F);}*/}
				else if(horiz>0.3F || vertic>0.3F
					    || horiz<-0.3F || vertic<-0.3F)
					charSpeed=40;
				else if(horiz!=0 || vertic!= 0)
					charSpeed=32;
			/* Animation Looping */
				/*else if(entrance && !meshan.GetCurrentAnimatorStateInfo(0).IsName("WalkEnter")){
					meshan.Play("WalkLoop");entrance=false;Debug.Log("FS");}*/
				else if(horiz==0 && vertic==0)
				{	charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Walking;
					chance=Random.Range(2,6)*2.667F;idleState=0;breathAmt=2;
					charSpeed=0;ftimer=0;//meshan.CrossFade("Take 001",0.4F);
					endurVelocity=40;}
			
			}/* if(ftimer<fadeTime && charStateLast==CharacterState.Walking)
					luyaMesh.position=new Vector3(tr.position.x,luyaMesh.position.y,tr.position.z);*/
			#endregion
															#region Hurt
			else if(charState==CharacterState.Hurt){
				if(enemyInfo.inHitstun){
					if(enemyInfo.theirHitstun<=0){
						enemyInfo.inHitstun=false;
						charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Trotting;
						chance=Random.Range(2,6)*2.667F;idleState=0;
						charSpeed=0;ftimer=0;dashDanceTimer=0;
						endurVelocity=40;loopAud.Stop();
						if(lastHoriz>0) meshan.Play("idleRight");
						else meshan.Play("idleLeft");}
					else enemyInfo.theirHitstun-=timey;}}
			#endregion
															#region Strafe
			else if(charState == CharacterState.Strafe)
			{
				if(Input.GetAxis("Evade")!=0){
					if((Mathf.Abs(horiz)>=0.9F || Mathf.Abs(vertic)>=0.9F) && !hardStrafe){
						hardStrafe=true;charSpeed=50;endurVelocity=0;endurance-=40;strafing=true;
						meshan.CrossFade("LimenousForwardEnter",0.1F);strafeSwitch=true;}
					else if(Input.GetAxis("Jump")!=0){ //bool hardStrafe passed to leap.. if hardStrafe, and then cloud button, initiate skip
						leaping=true;timer=0;endurVelocity=0;leaper=true;hardStrafe=false;
						charState=CharacterState.Skip;charSpeed=880;
						strafeJumpTimer=0;entrance=true;strafeSwitch=false; //Must set speed better for strafe... needs to not be 0.1 for skip
						meshan.SetBool("isGrounded",false);
						dumTr=tr;skipRotor=rotor;}
					strafeTimer2=0;}
				else
				{	charSpeed=0;
					if(strafeTimer2>0.2F){
						charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Skip;
						chance=Random.Range(2,6)*2.667F;idleState=0;
						charSpeed=0;ftimer=0;meshan.CrossFade("Take 001",0.3F);meshan.speed=1;strafeTimer2=0;
						dumTr=tr;skipRotor=rotor;
						endurVelocity=40;}
					else if(Input.GetAxis("Jump")!=0){ //hardStrafe kept TRUE until all potential strafes conclude with some Idle or Tattle (or injury)
						leaping=true;timer=0;endurVelocity=0;leaper=true;strafeTimer2=0;
						charState=CharacterState.Skip;strafeJumpTimer=0;charSpeed=880;
						CloudMain.transform.position=tr.position;CloudMain.transform.localScale=new Vector3(0.2F,0.2F,0.2F);
						Cloud.SetActive(true);meshan.SetBool("isGrounded",false);dumTr=tr;
						cloudMats=GameObject.FindGameObjectsWithTag("cloudMaterial");
						  foreach(GameObject cl in cloudMats){
							matAn=cl.GetComponent<Animator>();
							if(matAn!=null){
								matAn.enabled=true;matAn.speed=1;matAn.Play(0);}
							else
							{	cloudMatMesh=cl.GetComponent<MeshRenderer>();
								cloudMatMesh.enabled=true;
								cloudMatCollide=cl.GetComponent<MeshCollider>();
								cloudMatCollide.enabled=true;}}}
					else
					{	if(strafeSwitch){ // booo, assuming hardStrafe
							meshan.Play("LimenousForwardExit");
							strafeSwitch=false;}
						else //if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("LimenousForwardExit"))
						{	charState=CharacterState.Idle;isIdle=true;timer=0;hardStrafe=false;
							chance=Random.Range(6,17)*2.667F;idleState=0;
							charSpeed=0;entrance=true;
							endurVelocity=20;strafeJumpTimer=0;Cloud.SetActive(false);}}
					strafeTimer2+=timey;
				}
			}
			#endregion
															#region Conjure
			else if(charState == CharacterState.Conjure)
			{	
				if(enemyInfo.health<=0)
					charState=CharacterState.Respawn;
				else if(enemyInfo.inHitstun){
					charState=CharacterState.Hurt;
				}
				if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Take 001")){
					if(entrance && meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>0.481){
						entrance=false;
						if(daggerOut)
							Dagger.gameObject.SetActive(true);
						else Dagger.gameObject.SetActive(false);}}
				else
				{	charState=CharacterState.Idle;isIdle=true;timer=0;
					chance=Random.Range(2,6)*2.667F;idleState=0;weaponCoolDown=0;
					charSpeed=0;ftimer=0;meshan.Play("Take 001");
					endurVelocity=40;}
			}								
			#endregion
															#region Leap
		/*	Must code for side collision interrupts */
			else if(charState == CharacterState.Leap){
				if(Input.GetAxis("LBumper")>0 && ledgeNearby){
					charState=CharacterState.Ledge;leaping=false;}
				else if(Input.GetAxis("Evade")!=0)
					charState=CharacterState.Strafe;
				/*else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Leap")){
					leapSwitch=true;meshan.applyRootMotion=false;
					if(meshan.GetBool("isGrounded")){
						timer=0;charState=CharacterState.Trotting;meshan.applyRootMotion=true;
						hardStrafe=false;charStateLast=CharacterState.Leap;}}*/
				timer+=timey;
			}				
			#endregion
															#region Skip
			else if(charState == CharacterState.Skip){
				if(!leaping){		// this would ideally go into a landing animation
					Cloud.SetActive(false);
					charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Skip;
					chance=Random.Range(2,6)*2.667F;idleState=0;
					charSpeed=0;ftimer=0;meshan.CrossFade("Take 001",0.3F);meshan.speed=1;
					endurVelocity=40;rotor=tr.eulerAngles.y;}
				else if(timer>0.5F){
					if(Input.GetAxis("LBumper")>0 && ledgeNearby){
						charState=CharacterState.Ledge;leaping=false;} // Relevant ledge code here: moves Luya up to ledge's 'y'. No gravity, freezes motion.
					/*else if(Input.GetAxis("Evade")!=0){
						charState = CharacterState.Strafe;}*/}
				timer+=timey;
			}/* If strafeTimer<amt, & X
				*/
			#endregion
															#region Jump
			else if(charState==CharacterState.Jump){
				// if(entrance){
					// if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("JumpEnter")){
						// meshan.Play("Jump");entrance=false;}}
				if(hurtSquib){
					charState=CharacterState.Hurt;hurtSquib=false;
					/*meshan.Play("HurtSquib");*/timer=0;}
				else
				{	//if(Input.GetAxis("LBumper")>0 && ledgeNearby){
						// charState=CharacterState.Ledge;leaping=false;meshan.speed=0;rootOffset=Vector3.zero;
						// } // Relevant ledge code here: moves Luya up to ledge's 'y'. No gravity, freezes motion.
					// else if(Input.GetAxis("Evade")!=0){
						// charState=CharacterState.Strafe;charStateLast=CharacterState.Idle;
						// isStrafe=true;endurVelocity=0;endurance-=10;timer=0;strafeRotor=rotor;}
					// // else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Jump") && meshan.speed==1){
							// // meshan.speed=-1;meshan.Play("Jump");meshanGetCurrentAnimatorStateInfo(0).normalizedTime=1;}
					// // else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Jump") && meshan.speed==-1){
						// // meshan["JumpEnterJ"].speed=-1;meshan["JumpEnterJ"].time=meshan["JumpEnter"].length;
						// // meshan.Play("JumpEnter");}
					if(!isFalling)
					{	charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Jump;
						chance=Random.Range(2,6)*2.667F;idleState=0;
						endurVelocity=40;jumpBack=true;
						if(jumpHoriz>0) meshan.Play("idleRight");
						else if(jumpHoriz<0) meshan.Play("idleLeft");}
					else if(attackInput==1 && !enemyInfo.inHitstun){
						charState=CharacterState.Attack;timer=0;entrance=true;
						atkTimer=0;charStateLast=CharacterState.Jump;}
				}
			}
			#endregion	
															#region Ledge 
			// else if(charState == CharacterState.Ledge){
				// if(Input.GetAxis("LBumper")!=0){
					// if(vertic>0.3F){
						// if(ledgeType=="LedgeFloor"){
							// /* transAnimTime=an["ledgeStanding"].length; */
							// /* meshan.CrossFade("ledgeStanding",0.25F); 
							   // charState=CharacterState.WhileDoing;timer=0;*/
							// // move LN to tr
							// charState=CharacterState.Climb;
							// tr.position=new Vector3(luyaMesh.position.x,tr.position.y,luyaMesh.position.z);
							// meshan.Play("Climb4");/* Will need to be a blend */}
						// else if(ledgeType=="LedgePerch"){
						// /* transAnimTime=an["intoPerch"].length; */
							// /* meshan.CrossFade("intoPerch",0.25F); 
							   // charState=CharacterState.WhileDoing;timer=0;*/
							// /* charStateLast=CharacterState.Ledge;	*/;
						// }
						// else if(ledgeType=="LedgeCrawl"){
							// ;/* transAnimTime=an["IntoCrawl"].length;
							   // meshan.CrossFade("IntoCrawl",0.25F);
							   // charState=CharacterState.WhileDoing;timer=0;
							   // charStateLast=CharacterState.Ledge;
							   // crawlType=System.String.Empty;*/}
					// }
					// else if(Input.GetAxis("Jump")!=0){
						// if(horiz==0){ /* Jump from ledge animation crossfaded to */
							// leaping=true;timer=0;endurVelocity=0;leaper=true;
							// charState=CharacterState.Jump;charSpeed=880;burstVel=tr.forward*-1;}
						// else if(horiz<0){ /* Must account for grabbing&ungrabbing (LBump) */
							// leaping=true;timer=0;endurVelocity=0;leaper=true;
							// charState=CharacterState.Jump;charSpeed=880;burstVel=tr.right*-1;}
						// else
						// {	leaping=true;timer=0;endurVelocity=0;leaper=true;
							// charState=CharacterState.Jump;charSpeed=880;burstVel=tr.right;}}
					// else
					// {	if(horiz!=0){
							// charSpeed=40;vertic=0;}}} /* scuttle. */
				// else
				// {	charState=CharacterState.Jump;
					// if(jumpForward)
						// meshan["Jump"].speed=1;
					// else meshan["Jump"].speed=-1;} //falling
			// }
			#endregion
															#region Climb
			// else if(charState==CharacterState.Climb){
				// if(!meshan.IsPlaying("Climb4")){
					// tr.position=new Vector3(luyaMesh.position.x,tr.position.y,luyaMesh.position.z);
					// charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Climb;
					// chance=Random.Range(2,6)*meshan["Take 001"].length;idleState=0;
					// charSpeed=0;ftimer=0;meshan.Play("Take 001");
					// endurVelocity=40;
				// }
			// }
			#endregion
															#region WhileDoing
			else if(charState == CharacterState.WhileDoing){
				if(timer>=transAnimTime){
					if(charStateLast==CharacterState.Ledge){
						if(ledgeType=="floor"){	// Climb up
							charState=CharacterState.Idle;isIdle=true;timer=0;
							chance=Random.Range(6,17)*an["Idle"].length;idleState=0;an["OneArmBehindBack"].speed=-1;
							meshan.CrossFade("Take 001",0.4F);charSpeed=0; // meshan.CrossFade("Idle",0.4F);
							endurVelocity=40;}
						else if(ledgeType=="perch"){ // Perched
							;/* Perch? */}}}
				else timer+=timey;
			}
															#endregion
															#region CallBack
			// else if(charState == CharacterState.CallBack){
				// if(timer>1.7F){
					// charState=CharacterState.Idle;isIdle=true;timer=0;
					// chance=Random.Range(6,17)*an["Idle"].length;idleState=0;an["OneArmBehindBack"].speed=-1;
					// meshan.CrossFade("Take 001",0.4F);charSpeed=0;
					// endurVelocity=40;}
				// else timer+=timey;
			// }
			#endregion
															#region Tattle
			// else if(charState == CharacterState.Tattle)
			// {
				// if(timer>=0.5F){
					// if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0){
						// charState=CharacterState.Trotting;timer=0;endurVelocity=4;meshan.CrossFade("Runcycle");}
					// else{charState=CharacterState.Idle;meshan.CrossFade("Take 001",0.4F); // meshan.CrossFade("Idle",0.4F);
						// isIdle=true;charSpeed=0;timer=0;endurVelocity=20;}}
	
				// /*else
				 // *    if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
				 // *        
				 // *    }
				 // * */	
				// timer+=timey;
			// }
			#endregion
															#region Stammer
			// else if(charState==CharacterState.Stammer)
			// {
				// if(timer>0.75F){
					// charState=CharacterState.Idle;isIdle=true;timer=0;
					// endurVelocity=20;isStammer=false;}
				// else
					// isStammer=true;
				// meshan.CrossFade("Take 001",0.4F);
				// timer+=timey;
			// }
			#endregion
															#region Roll
			// else if(charState==CharacterState.Roll)
			// {
				// if(timer>0.75F){ // .54
					// charState=CharacterState.Idle;isIdle=true;timer=0;
					// meshan.CrossFade("Take 001",0.4F);endurVelocity=40;}
				// else if(timer>.5F)
					// charSpeed=0; /* No accel + surface friction = good slowdown */
				// else if(timer>0.1F)
					// charSpeed=40;
				// timer+=timey;
			// }
			#endregion
															#region Sitting 
			else if(charState == CharacterState.Sitting)
			{
				if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>=1){
					if(altDepressed && Input.GetAxis("Alt")!=0){
							meshan.speed=-1;meshan.Play("Sitting",-1,1);}
					else if(Input.GetAxis("Alt")==0 && altDown){
						altDepressed=true;altDown=false;}}
				else if(meshan.speed==-1 && meshan.GetCurrentAnimatorStateInfo(0).normalizedTime<=0){
					charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Sitting;
					chance=Random.Range(2,6)*2.667F;idleState=0;meshan.speed=1;
					endurVelocity=40;meshan.Play("Take 001");}}
			#endregion
			if(isStrafe && Input.GetAxis("Evade")==0)
				isStrafe=false;
			if(weaponCoolDown<=7)
				weaponCoolDown+=timey;
			
			lastHoriz=horiz;lastVertic=vertic;
															
			if(!offBreath){
				if(breathTimer>breathAmt){
					breathing.Stop();
					breathing.Play();
					if(Random.Range(0,1)<0.14F)
						offBreath=true;
					breathTimer=0;}}
			else
			{	if(breathTimer>breathAmt*2){
					breathing.Stop();
					breathing.Play();
					if(Random.Range(0,1)>0.14F)
						offBreath=false;
					breathTimer=0;}}
			breathTimer+=timey;
			
			if(health>100)
				health=100;
			else if(health<0)
				health=0;}
    }
	
	 void OnControllerColliderHit(ControllerColliderHit hit) {
        kontakt=true;
    }
	
	void OnGUI () {
		if(enemyInfo.health>0){
			if(start4Real)
			{	/*GUI.DrawTexture(new Rect(cameraBlah.toResize[1].rect.x*(float)Screen.width,CameraBlah.toResize[0].rect.y*(float)Screen.height,
				CameraBlah.toResize[0].rect.width*(float)Screen.width,CameraBlah.toResize[0].rect.height*(float)Screen.height),
				page2,ScaleMode.ScaleToFit);*/
				if(Input.GetAxis("Start")==1){
					start4Real=false;startDown=false;}}
			if(Input.GetAxis("Start")==1 && !startDown)
				startDown=true;
			else if(Input.GetAxis("Start")==0 && startDown){
				start4Real=true;}}
		else if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>=1 && meshan.GetCurrentAnimatorStateInfo(0).IsName("Death")){ //GAME OVER
			GUI.DrawTexture(new Rect(cameraBlah.toResize[1].rect.x*(float)Screen.width,cameraBlah.toResize[0].rect.y*(float)Screen.height,
			cameraBlah.toResize[0].rect.width*(float)Screen.width,cameraBlah.toResize[0].rect.height*(float)Screen.height),
			gameOver,ScaleMode.ScaleToFit);
			if(gameOverTimer>9 && !loader){
				StartCoroutine(LevelLoading());
				loader=true;}}
		

		healthBarlength=(Screen.width/3)*(enemyInfo.health/100);
   		/*GUI.Box(new Rect(170, 20, healthBarlength, 20), "Love");*/
	}
	/* if(Input.GetAxis("Rshift")==1)
			;
	
	
	*/
	IEnumerator LevelLoading(){ // Must include "destroy" code eventually.
		AsyncOperation async=Application.LoadLevelAsync("StartMenu5");
		yield return async;
		/*dummy=GameObject.Find(levelNames[trigger]);
		dummy.transform.position=positionsHere[i];
		dummy.transform.eulerAngles=rotationsHere[i];
		positionsHere.RemoveAt(trigger);*/
	}
}


/* Controller Controls */


/* 
	/* these decisions happen every 3 frames (for now) make it so grounded attacks make char immobile
									onGround... && 
									(tr.position-Player.tr.position).sqrMagnitude-speedGoing-opponentSpeed<groundhitboxsize
										either
											...movebackward
												if player is moving forwards and backwards often, outside of range of
											poke, movebackwards as well
							
											.....wait 
												 if opp air && amt of time attack out will eclipse before landing
																	
											.....jump
												if oppground && (tr.position-Player.tr.position).sqrMagnitude-speedGoing
																												(if is going)-opponentSpeed<groundhitboxsize
												else if they aren't approaching at all
												else if they are in the air and just started an attack,
															calculate if there is time enough to jump and fair.
																if so, commit to a jump forward and fair
											......attack
												if opp in air (tr.position-Player.tr.position).sqrMagnitude-opponentSpeed<groundhitbox 
																	&& > opp air hitbox
											
									inAir... && (tr.position-Player.tr.position).sqrMagnitude-speedGoing-opponentSpeed<groundhitboxsize
										(tr.position-Player.tr.position).sqrMagnitude-speedGoing-opponentSpeed<airhitboxsize
											...movebackward
												if (below, and thinks attack is coming)
													back.
											
											...throw out attack
												if (tr.position-Player.tr.position).sqrMagnitude fits hitbox size, 
													OR will, chuck attack
											
											...wait if thinks opp will also wait && groundattack will be  ...
											(tr.position-Player.tr.position).sqrMagnitude-speedGoing-opponentSpeed<groundhitboxsize
												this is a commit. 
														if player moves forwards > that small hitbox amount,
																	back off.
														else, take the option to wait and throw out ground attack.
															commit over.
											
											...moveforward if thinks opp will throw out attack or won't move backwards
								
								
 
 
 
 
 
	//if(!Lshift){
			if(straightDown){
				if(downwards.transform.tag!="floor"){
					distCounter+=downwards.distance;
					while(distCounter>.976F){ //need to set up the layers of colliders
						if(Physics.Raycast(turnTranny.position,orientVec,out downwards,.98F-downwards.distance)){
							distCounter+=downwards.distance;
							if(downwards.transform.tag=="floor"){
								isThereFloorAtAll=true;break;}}
						else
						{	isThereFloorAtAll=false;break;}}}}
			else if(slopeCorrect){
				if(slopewards.transform.tag!="floor"){
					distCounter+=slopewards.distance;
					while(distCounter>.976F){
						if(Physics.Raycast(turnTranny.position,orientVec,out slopewards,.98F-slopewards.distance)){
							distCounter+=slopewards.distance;
							if(slopewards.transform.tag=="floor"){
								isThereFloorAtAll=true;break;}}
						else 
						{	isThereFloorAtAll=false;break;}}}}
			//}

 */
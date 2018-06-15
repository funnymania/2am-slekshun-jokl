using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
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
	SlowDown = 10,
	Skid = 11,
	HeavySkid = 12,
	Falling = 13,
	DownslopeIdle = 14,
	UpslopeIdle = 15,
	Strafe = 16,
	Sitting = 17,
	Dying = 18,
	Hiding = 19,
	Attack = 20,
	Stammer = 21,
	Tattle = 22,
	KnockedBack = 23,
	Leap = 24,
	Roll = 25,
	Ledge = 26,
	CallBack = 27,
	Skip = 28,
	Crawl = 29,
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

	public static List<Transform> interestingStuff = new List<Transform>();
	public static System.String ledgeType;
	public static System.String crawlType;
	public static Transform tr,spriteLocale;
	public static Vector3 orientVec = Vector3.zero;
	public static Vector3 velocity = Vector3.zero;
	public static RaycastHit downwards;
	public static bool Lshift = false;
	public static bool hurtSquib = false;
	public static bool hurtHard = false;
	public static bool hurtMed = false;
	public static bool canDown = false;
	public static bool canUp = false;
	public static bool onLine = false;
	public static float hurtAmt = 0;
	public static int impactPriority=1;
	public static bool vulnerable = false;
	public static bool respawning = false;
	public static bool eyesOpen = false;
	public static bool dialogFlag = false;
	public static bool isFalling = true;
	public static bool noMovement = false;
	public static bool straightDown = false;
	public static bool burstCocked = false;
	public static bool isBattle = false;

	public CharacterController me;
	public Animator meshan;
	public Transform turnTranny;
	public Transform chain1,chainMID,chain2;
	public Transform spriteCont;
	public RectTransform healthBar;
	public EnemyInfo enemyInfo;
	public CameraBlah cameraBlah;
	public movingEvent clearMe;
	public Texture gameOver,controls;
	public List<Collider> HitBoxes = new List<Collider>();
	public float cloudTime;
	public int maxHealth=100;
	private float healthBarlength;
	private float endurBarlength;
	public float walkSpeed = 1;
	public float trotSpeed = 1;
	float runSpeed = 1;
	public float sprintSpeed = 1;
	public float stammerSpeed = 7;
	public float upslopeSpeed = 4.5F;
	public float upslopeRunSpeed = 6.7F;
	public float downslopeSpeed = 5;
	public float downslopeRunSpeed = 8;
	public float downslopeSprintSpeed = 15; // Sonic Speed, WWWWOOOOWOOWOOOOWOWOWOOWOO!!!
	public float jumpSpeed = 0;	// Not sure about this...
	public float landingLag = 0.25F;
	public static int knockerBackerAmt;
	public static bool startFlag = false;
	public static bool ledgeNearby = false;
	public static bool cursed = false;
	public AudioClip[] footValokaas;
	public AudioClip[] hallAudio;
	public AudioClip Spawn;
	StartScreen yada;

	class collision{
		public float distance;
		public Vector3 contactPt;
		public Vector3 normal;
		public collision(float distance,Vector3 normal,Vector3 contactPt){
			this.distance=distance;
			this.normal=normal;
			this.contactPt=contactPt;}
	}
	Vector3 blahBlah;
	int i,j, idleState;
	int framesDashing=0;
	int framesExplode=0;
	float distNear;
	float chance;
	float endurance=100;
	float endurVelocity = 0;
	float endurScalar = 1;
	float charSpeed=0;
	float breathAmt = 2.3F;
	double camRotor,camRotorPast,camRotorOffset,skidRotor,skidRotorLast,skidRotorSumUp,strafeRotor,strafeRotor2;
	public static float health=100;
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
	float haki = 7;
	float pain = 0;
	float gameOverTimer = 0;
	float horiz2,vertic2;
	float downDistanceLast;
	float lastHoriz,lastVertic;
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
	List<collision> cols=new List<collision>();
	RaycastHit forwards,slopewards;
	RaycastHit ledge;
	RaycastHit ledgey;
	RaycastHit[] body;
	RaycastHit[,] chain=new RaycastHit[4,3];
	GameObject particle;
	GameObject particle2;
	GameObject[] cloudMats;
	Animator matAn;
	MeshRenderer cloudMatMesh;
	MeshCollider cloudMatCollide;
    public static Vector3 moveDirection = Vector3.zero;
	public static Vector3 camOffset = Vector3.zero;
    public static Vector3 rootOffset = Vector3.zero;
	Vector3 gravity = new Vector3(0,-21F,0);
	Vector3 DI = Vector3.zero;
	Vector3 playersWill = Vector3.zero;
	Vector3 surface = Vector3.zero;
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
	Vector3 genNormal;
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
	bool isJump = false;
	bool strafeLeap = false;
	bool gameStart = true;
	bool onMoving = false;
	bool transNow = false;
	bool leftFootDown = false;
	bool rightFootDown = true;
	bool altDown = false;
	bool altDepressed = false;
	bool jumpForward = true;
	bool footDown = false;
	bool isGround = false;
	bool switchbird = false;
	bool sideCollide = false;
	bool leapSwitch = false;
	bool animSwitch = false;
	bool daggerOut = false;
	bool attackDown,attackDown2;
	bool startDown = false;
	bool start4Real = false;
	bool loader = false;
	bool strafing = false;
	bool goingDown = false;
	bool LshiftDown = true;
	bool slopeCorrect = false;
	bool isThereFloorAtAll = true;
	bool kontakt = false;
	bool shifter=false;
	bool explode=false;
	bool dashing=false;
	bool explode2=true;
	bool isHit=false;
	int coinflip;
	float distCounter=0;
	#endregion
	
    void Start () {
		particle = GameObject.Find("Player/stuff/sprite&controller/beefstrap 1");
		particle2 = GameObject.Find("Player/stuff/sprite&controller/beefstrap (1)");
		breathing = Breath.GetComponent<ParticleSystem>();
		interestingStuff = null;
		enemyInfo.health = 100;
		tr = turnTranny;
        spriteLocale = tr.parent;
		//meshan=GetComponent<Animator>();
		downwardsLast = new Vector3(0,1,0);
		timer = 0;
		meshan.Play("idleLeft");
		playersWill = Vector3.zero;
		rootPast = tr.position;
		body = new RaycastHit[8];
		endurVelocity = 20;
		chance = Random.Range(2,6) * 2.667F;
		idleState = 0;
        skidRotor = 0;
		camRotor = camRotation.eulerAngles.y * Mathf.PI / 180;
		Application.targetFrameRate = 60;
		camOffsetPast = spriteCont.position;
		orientVec = new Vector3(0,1,0);
		//Screen.fullScreen=true;
	}

	void FixedUpdate () {
		timey = Time.deltaTime;
		downwards = new RaycastHit();
        ledge = new RaycastHit();
        ledgey = new RaycastHit();
		slopewards = new RaycastHit();
		distNear = 1;

        // Preference to top chains. Sided. if multiple contact points, draw a line between those affected areas
		// && and impose this as the surface
        for (i = 0; i < 4; i++) { 
			for (j = 0; j < 3; j++) {
				chain[i,j] = new RaycastHit();
            }
        }
		cols.Clear();
		camOffset = spriteCont.position - camOffsetPast;
		// Debug.DrawRay(tr.position,downwardsLast*-2);

	#region colliderStuff
		// chain forward
			isHit = Physics.Raycast(chain1.position, chain1.forward, out chain[0,0], .3F, ~0, QueryTriggerInteraction.Ignore);
			// Debug.DrawRay(chain1.position,chain1.forward*2);
			if (isHit) {
				distNear = chain[0,0].distance;
				collision me = new collision(chain[0,0].distance, chain[0,0].normal, chain1.forward * chain[0,0].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chainMID.position, chainMID.forward, out chain[0,1], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[0,1].distance < distNear) {
				if (cols.Count != 0) {
                    cols.RemoveAt(cols.Count - 1);
                }
				distNear = chain[0,1].distance;
				collision me = new collision(chain[0,1].distance, chain[0,1].normal, chainMID.forward * chain[0,1].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chain2.position, chain2.forward, out chain[0,2], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[0,2].distance < distNear) {
				if (cols.Count != 0) {
                    cols.RemoveAt(cols.Count - 1);
                }
				distNear = chain[0,2].distance;
				collision me = new collision(chain[0,2].distance, chain[0,2].normal, chain2.forward * chain[0,2].distance);
				cols.Add(me);
                isHit = false;
            }
		// chain forward * -1
			isHit = Physics.Raycast(chain1.position, chain1.forward * -1, out chain[1,0], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit) {
				distNear = chain[1,0].distance;
				collision me = new collision(chain[1,0].distance, chain[1,0].normal, chain1.forward * -1 * chain[1,0].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chainMID.position, chainMID.forward * -1, out chain[1,1], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[1,1].distance < distNear) {
				if (cols.Count!=0) {
                    cols.RemoveAt(cols.Count - 1);
                }
                distNear = chain[1,1].distance;
				collision me = new collision(chain[1,1].distance, chain[1,1].normal, chainMID.forward * -1 * chain[1,1].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chain2.position, chain2.forward * -1, out chain[1,2], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[1,2].distance < distNear) {
				if (cols.Count != 0) {
                    cols.RemoveAt(cols.Count - 1);
                }
                distNear = chain[1,2].distance;
				collision me = new collision(chain[1,2].distance, chain[1,2].normal, chain2.forward * -1 * chain[1,2].distance);
				cols.Add(me);
                isHit = false;
            }
		// chain right
			isHit = Physics.Raycast(chain1.position, chain1.right, out chain[2,0], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit) {
				distNear = chain[2,0].distance;
				collision me = new collision(chain[2,0].distance, chain[2,0].normal, chain1.right * chain[2,0].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chainMID.position, chainMID.right, out chain[2,1], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[2,1].distance < distNear) {
				if (cols.Count != 0) {
                    cols.RemoveAt(cols.Count - 1);
                }
                distNear = chain[2,1].distance;
				collision me = new collision(chain[2,1].distance, chain[2,1].normal, chainMID.right * chain[2,1].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chain2.position, chain2.right, out chain[2,2], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[2,2].distance < distNear) {
				if (cols.Count != 0) {
                    cols.RemoveAt(cols.Count - 1);
                }
                distNear = chain[2,2].distance;
				collision me = new collision(chain[2,2].distance, chain[2,2].normal, chain2.right * chain[2,2].distance);
				cols.Add(me);
                isHit = false;
            }
		// chain right * -1
			isHit = Physics.Raycast(chain1.position, chain1.right * -1, out chain[3,0], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit) {
				distNear = chain[3,0].distance;
				collision me = new collision(chain[3,0].distance, chain[3,0].normal, chain1.right * -1 * chain[3,0].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chainMID.position, chainMID.right * -1, out chain[3,1], .3F, ~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[3,1].distance < distNear) {
				if (cols.Count != 0) {
                    cols.RemoveAt(cols.Count - 1);
                }
                distNear = chain[3,1].distance;
				collision me = new collision(chain[3,1].distance, chain[3,1].normal, chainMID.right * -1 * chain[3,1].distance);
				cols.Add(me);
                isHit = false;
            }
			isHit = Physics.Raycast(chain2.position, chain2.right * -1, out chain[3,2], .3F ,~0, QueryTriggerInteraction.Ignore);
			if (isHit && chain[3,2].distance < distNear) {
				if (cols.Count != 0) {
                    cols.RemoveAt(cols.Count - 1);
                }
                distNear = chain[3,2].distance;
				collision me = new collision(chain[3,2].distance, chain[3,2].normal, chain2.right * -1 * chain[3,2].distance);
				cols.Add(me);
                isHit = false;
            }
		// down
			straightDown = Physics.Raycast(chainMID.position, chainMID.up * -1, out downwards, .74F, ~0, QueryTriggerInteraction.Ignore);
			Debug.DrawRay(chainMID.position, chainMID.up * -1);
		// correction
			slopeCorrect = Physics.Raycast(chainMID.position, chainMID.up * -1, out slopewards, 2, ~0, QueryTriggerInteraction.Ignore);
		// logic
			//downwardsLast*-1
		/* if only one raycast detects, move according to its normal
			if >1, find the 2 nearest and impose a line between them. move according to this.
				capsule for fencil and opponent ; impactPriority will determine who moves through who*/
#endregion
		if (downwardsLast.y <= 0.1F) Debug.Log(downwards.transform.name);
		if (!slopeCorrect) {
			downwardsLast = new Vector3(0,1,0);
        }
		if (!noMovement) {
			if (straightDown || (slopeCorrect && !isFalling)) {
                camOffsetPast = spriteCont.position;
				if (isFalling) { // dust particle here!
					accel = Vector3.zero;
                    velocity = Vector3.zero;
                    camOffsetPast = spriteCont.position;
                    leaping = false;
					isFalling = false;
                    isGround = true;
                    downwardsLast = downwards.normal;
					if (gameStart) {
						//particle.SetActive(true);particle2.SetActive(true);GetComponent<AudioSource>().PlayOneShot(Spawn,0.125F);
						gameStart = false;
                    }
                } else {	
                    if (vertic == -1 && canDown)
						goingDown = true;
					if (dialogFlag)
                        ;
					else if (straightDown) {
						downDistanceLast = downwards.distance;
						if (downwards.transform.tag == "floor"){
							if (downwardsLast != downwards.normal) {
								downwardsLast = downwards.normal;
								Quaternion dummyRotation = Quaternion.FromToRotation(spriteCont.up, downwards.normal);
								spriteCont.eulerAngles = new Vector3(dummyRotation.eulerAngles.x, dummyRotation.eulerAngles.y, spriteCont.eulerAngles.z);
							}
                        }
						if (downDistanceLast < .64F) {
							spriteCont.position += new Vector3(0, .71F - downDistanceLast, 0);
                        }
						orientVec = downwards.normal * -1;
						if (explode) {
							framesExplode++;
							if (explode2) {
								accel = slope * 800;
								velocity += accel * Time.deltaTime;
								moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
								spriteCont.position += moveDirection;
								explode2 = false;
                            } else {	
                                if (framesExplode > 30) {
									explode2 = true;
									dashing = true;
									explode = false;
                                    framesExplode = 0;
                                } else {	
                                    accel = slope * -30;
									velocity += accel * Time.deltaTime;
									moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
									spriteCont.position += moveDirection;
                                }
                            }
                        } else if (horiz == 1 || vertic == 1 || horiz == -1 || vertic == -1) {
							if (moveDirection.sqrMagnitude <= .02 && framesDashing != 0) {
                                spriteCont.position += blahBlah;
                            }
							Vector3 garage = new Vector3(turnTranny.forward.x, 0, turnTranny.forward.z);
							slope = Vector3.Cross(Vector3.Cross(downwards.normal, garage), downwards.normal);
							slope = slope.normalized;
                            Debug.Log(cols.Count);
							if (cols.Count == 1) {
								genNormal = cols[0].normal;
								slope = (genNormal + slope).normalized;
                            } else if (cols.Count > 1) {
								Vector3 side1 = cols[1].contactPt - cols[0].contactPt;
								Vector3 tmp = new Vector3(cols[1].contactPt.x, cols[1].contactPt.y - 1, cols[1].contactPt.z);
								Vector3 side2 = tmp - cols[0].contactPt;
								genNormal = Vector3.Cross(side2, side1);
								genNormal = genNormal.normalized;
								Debug.DrawRay(chainMID.position, genNormal * 5F, Color.white, .5F);
								//Debug.Break();
								slope= (genNormal - slope).normalized;
                            }
							if (!dashing) {
								dashing = true;
								accel = slope * 303; //4169
								velocity += accel * Time.deltaTime;
								moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
								spriteCont.position += moveDirection;
                            } else {	
                                accel = Vector3.zero;
								moveDirection = velocity.magnitude * slope * Time.deltaTime;
								spriteCont.position += moveDirection;
                            }
							framesDashing++;
                        } else if (vertic == 0 && horiz == 0) {
							if (moveDirection.sqrMagnitude <=. 02F) {
								accel = Vector3.zero;
                                velocity=Vector3.zero;
                                dashing = false;
                            } else {	
                                accel = slope * -20;
								velocity += accel * Time.deltaTime;
								moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
								spriteCont.position += moveDirection;
                                dashing = false;
								framesDashing = 0;
                            }
                        }
                    } else {	
                        if (slopewards.transform.parent.name == "linees") {
                            onLine = true;
                        } else {
                            onLine = false;
                        }
						downDistanceLast = slopewards.distance;
                        // Debug.Log("slope");
						// slope=Vector3.Cross(Vector3.Cross(slopewards.normal,camOffset),slopewards.normal);
						if (slopewards.distance > .74F) {
							slope.y = slopewards.distance - 0.73F;
							// me.Move(new Vector3(0,slope.y*-1,0));
							spriteCont.position += new Vector3(0, slope.y * -1, 0);
							Debug.Log("whoaNow " + slope.y);
                        }
						if (explode) {
							if (explode2) {
								accel = slope * 800;
								velocity += accel * Time.deltaTime;
								moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
								spriteCont.position += moveDirection;
								explode2=false;
                            } else {	
                                if (moveDirection.sqrMagnitude < .13) {
									explode2 = true;
									explode = false;
                                } else {	
                                    accel = slope * -20;
									velocity += accel * Time.deltaTime;
									moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
									spriteCont.position += moveDirection;
                                    dashing = false;
                                }
                            }
                        } else if (horiz == 1 || vertic == 1 || horiz == -1 || vertic == -1) {
							if (moveDirection.sqrMagnitude <= .02 && framesDashing != 0) {
                                spriteCont.position += blahBlah;
                            }
                            Vector3 garage = new Vector3(turnTranny.forward.x, 0, turnTranny.forward.z);
							slope = Vector3.Cross(Vector3.Cross(slopewards.normal, garage), downwards.normal);
							slope = slope.normalized;
							if (cols.Count == 1) {
								genNormal = cols[0].normal;
								slope = (genNormal + slope).normalized;
                            } else if (cols.Count > 1) {
								Vector3 side1 = cols[1].contactPt - cols[0].contactPt;
								Vector3 tmp = new Vector3(cols[1].contactPt.x, cols[1].contactPt.y - 1, cols[1].contactPt.z);
								Vector3 side2 = tmp - cols[0].contactPt;
								genNormal = Vector3.Cross(side2, side1);
								genNormal = genNormal.normalized;
								// Debug.DrawRay(chainMID.position,slope*5F,Color.white,19);
								// Debug.Break();
								slope= (genNormal - slope).normalized;
                            }
							if (!dashing) {
								dashing = true;
								accel = slope * 270;
								velocity += accel * Time.deltaTime;
								moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
								spriteCont.position += moveDirection;
                            } else {	
                                accel = Vector3.zero;
								moveDirection = velocity.magnitude * slope * Time.deltaTime;
								spriteCont.position += moveDirection;
                            }
                        } else if (vertic == 0 && horiz == 0) {
							if (moveDirection.sqrMagnitude <= .02F) {
								accel = Vector3.zero;
                                velocity = Vector3.zero;
                                dashing = false;
                            } else {	
                                accel = slope * -20;
								velocity += accel * Time.deltaTime;
								moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
								spriteCont.position += moveDirection;
                                dashing = false;
                            }
                        }
						if (slopewards.transform.tag == "floor") {
							if (!Lshift) {
								downwardsLast = slopewards.normal;
								// tr.position-=new Vector3(0,slope.y,0);
								// spriteCont.rotation=Quaternion.FromToRotation(spriteCont.up,slopewards.normal);
                            } else {	
                                if (!kontakt) 
                                    me.Move(new Vector3(0, 0, slope.z));
								else 
                                    tr.position += new Vector3(0, 0, slope.z);
                            }
                        }
                    }
                }
			} else if (charState != CharacterState.Ledge && charState != CharacterState.Climb) {	
				onMoving = false;
                camOffsetPast = spriteCont.position;
				if (!Lshift) { // Must be moving y-down RELATIVE to normal axis.
					accel = downwardsLast * -3.1F;
					velocity += accel * Time.deltaTime;
					moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
					spriteCont.position += moveDirection;
                } else {	
                    accel = downwardsLast * -3.1F;
					velocity += accel * Time.deltaTime;
					moveDirection = (accel / 2 * Time.deltaTime + velocity) * Time.deltaTime;
					moveDirection *= tinyDude;
					spriteCont.position += moveDirection;
                }
				isFalling = true;
				rootOffset = tr.position - rootPast;
				rootPast = tr.position;
				/*if(!Lshift) orientVec=new Vector3(0,-1,0);
				else orientVec=new Vector3(0,0,1);*/
            } else {
                camOffsetPast=spriteCont.position;
            }
        }
	}

    void Update () {
		if(!EventHandler.eventFlag){
			timey=Time.deltaTime;horiz=Input.GetAxis("Horizontal");vertic=Input.GetAxis("Vertical");
			horiz2=Input.GetAxis("Horizontal2");vertic2=Input.GetAxis("Vertical2");
			/*if(Input.GetAxis("Start")>0 && blurb){
				startFlag=true;yada=gameObject.GetComponent<StartScreen>();playersWill=Vector3.zero;
				yada.enabled=true;}*/
			camRotorPast=camRotor;camRotor=camRotation.eulerAngles.y;
			camRotorOffset=camRotor-camRotorPast;
			coinflip=Random.Range(0,2);
			if(isBattle){
				healthBar.sizeDelta=new Vector2(enemyInfo.health,8);
				if(burstCocked && Input.GetAxis("Vertical2")==0){
					explode=true;burstCocked=false;}}
			else healthBar.sizeDelta=new Vector2(0,0);
			//Debug.Log(Lshift);
			/*if(canDown){
				if(Input.GetAxis("Eyes")==1 && !Lshift){
					Lshift=true;//orientVec=new Vector3(0,0,1);
					downwardsLast=orientVec;}
				else if(Input.GetAxis("Eyes")==1 && !LshiftDown){
					Lshift=false;/*orientVec=new Vector3(0,-1,0);}}*/

			if(Input.GetAxis("Eyes")==0 && Lshift) LshiftDown=false;
			//else LshiftDown=true;
					#region Rotation
			// + -
			// - +
			if(charState!=CharacterState.Dying && charState!=CharacterState.Roll && charState!=CharacterState.CallBack
			&& charState!=CharacterState.Crawl && charState!=CharacterState.Ledge && charState!=CharacterState.Jump
			&& charState!=CharacterState.WhileDoing && charState!=CharacterState.Conjure && charState!=CharacterState.Sitting
			&& !dialogFlag && !noMovement){
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
						else turnTranny.eulerAngles=new Vector3(turnTranny.localEulerAngles.x,(float)(rotor),turnTranny.localEulerAngles.z);}
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
						}}}
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
				else if(dialogFlag){
					if(horiz>0)
						meshan.Play("idleRight");
					else if(horiz<0)
						meshan.Play("idleLeft");
					else if(vertic>0)
						meshan.Play("idleIn");
					else if(vertic<0)
						meshan.Play("idleOut");}
				else if(horiz!=0 || vertic!=0){
					if(horiz>0){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("left");}
					else if(horiz<0){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("right");}
					else if(vertic>0){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("in");}
					else if(vertic<0){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathAmt=1.5F;
						timer=0;meshan.Play("out");}
					lastVertic=0;lastHoriz=0;}
				/*else if(horiz2!=0){
					if(horiz2>.4F || horiz2<-.4F){
						endurVelocity=4;breathTimer=0;
						charState=CharacterState.Turning;
						timer=0;//meshan.Play("Turn");
						/*meshan.speed=horiz2;}}*/
				else if(Input.GetAxis("Hiding")==1){
					charState=CharacterState.Hiding;charStateLast=CharacterState.Idle;endurVelocity=endurScalar*-4;
					meshan.CrossFade("Hiding",0.3F);}
				else if(Input.GetAxis("Roll")==1){
					charState=CharacterState.Roll;endurance-=10;timer=0;
					charSpeed=40;/*meshan.CrossFade("Rolling",0.3F);*/}
				else if(Input.GetAxis("Attack")==1){
					if(daggerOut){
						//meshan.CrossFade("DaggerSlashGround",0.1F);
						charState=CharacterState.Attack;timer=0;entrance=true;
						charStateLast=CharacterState.Idle;}}
				//else if(Input.GetAxis("Jump")!=0 && canUp){ /// rt ctrl!!
				//	leaping=true;timer=0;endurVelocity=0;leaper=true;entrance=true;vulnerable=true;
				//	charState=CharacterState.Jump;jumpForward=true;charSpeed=88;burstVel=tr.up;
				//	/*meshan.Play("Jump");*/}
				// else if(Input.GetAxis("Eyes")!=0/* && !isStrafe*/){
					// if(cursed) endurance-=20;
					// else endurance-=10;
					// charState=CharacterState.Strafe;charStateLast=CharacterState.Idle;
					// isStrafe=true;endurVelocity=0;timer=0;entrance=false;strafeTimer=0;
					// /*meshan.CrossFade("Limenous2", 0.2F);*/}
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
															#region Turning
			else if(charState==CharacterState.Turning){
				if((horiz!=0 || vertic!=0) /*&& meshan.GetCurrentAnimatorStateInfo(0).IsName("Take 001")*/){
					if(horiz>.6F || vertic>.6F || horiz<-.6F || vertic<-.6F){
						charState=CharacterState.Trotting;
						endurVelocity=4;breathTimer=0;//meshan.speed=1;
						timer=0;/*meshan.Play("TrotEnterJ");*/}
					else
					{	charStateLast=CharacterState.Idle;isWalk=false;timer=0;
						charSpeed=160;endurVelocity=18;entrance=true;
						//meshan.Play("WalkEnter");
						charState=CharacterState.Walking;//meshan.speed=1;
						endurVelocity=4;breathTimer=0;
						timer=0;}}
				else if(horiz2==0 /* || meshan.GetCurrentAnimatorStateInfo(0).normalizedTime%1>0.5*/){
					charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Turning;
					chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;
					charSpeed=0;ftimer=0;//meshan.Play("Take 001");meshan.speed=1;
					endurVelocity=40;rotor=tr.eulerAngles.y;}
				//else meshan.speed=horiz2/1.5F;/* If enough time passes, cross to idle, play turning again. Left */
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
				else if(dialogFlag){
					charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Trotting;
					chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;
					charSpeed=0;ftimer=0;dashDanceTimer=0;
					endurVelocity=40;clearMe.clearRemains();
					if(lastHoriz>0) meshan.CrossFade("idleRight",0F);
					else  meshan.CrossFade("idleLeft",0F);}
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
						// chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;dashDanceTimer=0;
						// charSpeed=0;ftimer=0;meshan.CrossFade("Take 001",0F);
						// endurVelocity=40;}
					// else{ dashDanceTimer=0;}}
				// else if(Input.GetAxis("Hiding")==1){
					// charState=CharacterState.Hiding;endurVelocity=endurScalar*-2;
					// meshan.CrossFade("Hiding",0.3F);dashDanceTimer=0;}
				else if(Input.GetAxis("Attack")==1 && daggerOut){
					if(!leaper){
						meshan.Play("DaggerSlashGround");
						charState=CharacterState.Attack;timer=0;entrance=true;
						charStateLast=CharacterState.Trotting;dashDanceTimer=0;}
					else
					{	leaping=true;timer=0;endurVelocity=0;
						charState=CharacterState.Leap;dashDanceTimer=0;/* meshan.CrossFade("RunXJump",0.3F); */}}
				//else if(Input.GetAxis("Jump")==1){
					//leaping=true;timer=0;endurVelocity=0;leaper=true;/*meshan.Play("Leap");*/isGround=false;
					//charState=CharacterState.Leap;dashDanceTimer=0;/*charSpeed=880;burstVel=tr.up;*/}// meshan.CrossFade("RunXJump",0.3F);
				else if(Input.GetAxis("Roll")==1){
					charState=CharacterState.Roll;endurance-=10;timer=0;dashDanceTimer=0;
					charSpeed=88;/*meshan.CrossFade("Rolling",0.3F);*/}
				else if(Input.GetAxis("Alt")!=0){
					if(!walkDepressed){
						charState=CharacterState.Walking;endurVelocity=18;isWalk=true;walkDepressed=true;
						meshan.CrossFade("Take 001",0.3F);dashDanceTimer=0;}} // meshan.CrossFade("WalkHandsy",0.3F);
				else if(Input.GetAxisRaw("Shift")==1 && endurance>0){
					charState=CharacterState.Running;meshan.CrossFade("RunLoop",0.2F);dashDanceTimer=0;
					meshan.speed=1;}
				// else if(Input.GetAxis("Eyes")!=0 && !isStrafe){
					// charState=CharacterState.Strafe;charStateLast=CharacterState.Trotting;
					// timer=0;isStrafe=true;dashDanceTimer=0;
					// meshan.CrossFade("Limenous",0.2F);}
				else if(horiz>0){
					meshan.Play("left");/*if(lastHoriz<0)clearMe.clearRemains();*/}
				else if(horiz<0){
					meshan.Play("right");/*if(lastHoriz>0)clearMe.clearRemains();*/}
				else if(vertic>0){
					meshan.Play("in");/*if(lastVertic<0)clearMe.clearRemains();*/}
				else if(vertic<0){
					meshan.Play("out");/*if(lastVertic>0)clearMe.clearRemains();*/}
				else if(horiz==0 && vertic==0)
				{	charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Trotting;
					chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;
					charSpeed=0;ftimer=0;dashDanceTimer=0;
					endurVelocity=40;clearMe.clearRemains();
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
				else if(Input.GetAxis("Alt")==1){
					timer=0;
					charState=CharacterState.Skid;}
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
				else if(Input.GetAxis("Alt")==1){
					charState=CharacterState.HeavySkid;timer=0;}
			}
			#endregion
															#region Attack
			else if(charState==CharacterState.Attack)
			{
				if(enemyInfo.health<=0)
					charState=CharacterState.Dying;
				else if(enemyInfo.inHitstun)
					charState=CharacterState.Hurt;

				if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>0.126 &&
				   meshan.GetCurrentAnimatorStateInfo(0).normalizedTime<0.166){ //just before 2nd slash, look for 'A' button
					for(i=0;i<HitBoxes.Count;i++)
						HitBoxes[i].enabled=true;
					attackDown2=true;
					enemyInfo.theirPain=8;entrance=false;
					if(Input.GetAxis("Attack")>0) attackDown=true;}
				else if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>0.35 &&
						meshan.GetCurrentAnimatorStateInfo(0).normalizedTime<0.526){ //just before third
					for(i=0;i<HitBoxes.Count;i++)
						HitBoxes[i].enabled=true;
					attackDown2=true;enemyInfo.theirPain=10;
					if(Input.GetAxis("Attack")>0) attackDown=true;}
				else if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>=1 &&
						meshan.GetCurrentAnimatorStateInfo(0).IsName("DaggerSlashGround")){
					for(i=0;i<HitBoxes.Count;i++)
						HitBoxes[i].enabled=false;enemyInfo.theirPain=12;
					charState=CharacterState.Idle;isIdle=true;timer=0;attackDown2=false;
					charSpeed=0;chance=Random.Range(6,17)*2.667F;idleState=0;
					endurVelocity=20;meshan.CrossFade("Take 001",0.1F);}
				else if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.166)
				{	for(i=0;i<HitBoxes.Count;i++)
						HitBoxes[i].enabled=false;
					if(attackDown2){
						if(!attackDown){
							charState=CharacterState.Idle;isIdle=true;timer=0;attackDown2=false;
							charSpeed=0;chance=Random.Range(6,17)*2.667F;idleState=0;
							endurVelocity=20;meshan.CrossFade("Take 001",0.1F);}
						else
						{	attackDown2=false;attackDown=false;}}
					/*else if(meshan.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.166 &&
							meshan.GetCurrentAnimatorStateInfo(0).normalizedTime<=0.35 ) attackDown2=true;*/}
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
				else if(Input.GetAxis("Hiding")==1){
					charState=CharacterState.Hiding;endurVelocity=endurScalar*-4;/*meshan.CrossFade("Hiding",0.3F);*/}
				else if(Input.GetAxis("Roll")==1){
					charState=CharacterState.Roll;endurance-=20;timer=0;
					charSpeed=48;/*meshan.CrossFade("Rolling",0.3F);*/}
				else if(Input.GetAxis("Attack")==1){
					charState=CharacterState.Attack;/*meshan.CrossFade("Attack",0.3F);*/}
				else if(Input.GetAxis("Eyes")!=0 && !isStrafe){
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
					chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;breathAmt=2;
					charSpeed=0;ftimer=0;//meshan.CrossFade("Take 001",0.4F);
					endurVelocity=40;}

			}/* if(ftimer<fadeTime && charStateLast==CharacterState.Walking)
					luyaMesh.position=new Vector3(tr.position.x,luyaMesh.position.y,tr.position.z);*/
			#endregion
															#region Hurt
			else if(charState==CharacterState.Hurt){
				if(enemyInfo.health<=0){
					meshan.CrossFade("Death",0.1F);Debug.Log("You Died.");
					charState=CharacterState.Dying;}
				else if(enemyInfo.painType==EnemyInfo.PainType.Med){
					if(!animSwitch){
						meshan.Play("HurtSquib");charSpeed=0;animSwitch=true;}
					else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("HurtSquib")){
						endurVelocity=20;charState=CharacterState.Idle;isIdle=true;timer=0;
						animSwitch=false;enemyInfo.inHitstun=false;enemyInfo.vulnerable=true;}}
				else if(enemyInfo.painType==EnemyInfo.PainType.Heavy){
					if(!animSwitch){
						meshan.Play("HurtHard");charSpeed=0;animSwitch=true;}
					else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("HurtHard")){
						endurVelocity=20;charState=CharacterState.Idle;isIdle=true;timer=0;
						animSwitch=false;enemyInfo.inHitstun=false;enemyInfo.vulnerable=true;}}}
			#endregion
															#region Strafe
			else if(charState == CharacterState.Strafe)
			{
				if(Input.GetAxis("Eyes")!=0){
					if((Mathf.Abs(horiz)>=0.9F || Mathf.Abs(vertic)>=0.9F) && !hardStrafe){
						hardStrafe=true;charSpeed=50;endurVelocity=0;endurance-=40;strafing=true;
						meshan.CrossFade("LimenousForwardEnter",0.1F);strafeSwitch=true;}
					else if(Input.GetAxis("Jump")!=0){ //bool hardStrafe passed to leap.. if hardStrafe, and then cloud button, initiate skip
						leaping=true;timer=0;endurVelocity=0;leaper=true;isJump=true;hardStrafe=false;
						charState=CharacterState.Skip;charSpeed=880;
						strafeJumpTimer=0;entrance=true;strafeSwitch=false; //Must set speed better for strafe... needs to not be 0.1 for skip
						strafeLeap=false;meshan.SetBool("isGrounded",false);
						dumTr=tr;skipRotor=rotor;}
					strafeTimer2=0;}
				else
				{	charSpeed=0;strafeLeap=true;
					if(strafeTimer2>0.2F){
						charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Skip;
						chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;
						charSpeed=0;ftimer=0;meshan.CrossFade("Take 001",0.3F);meshan.speed=1;strafeTimer2=0;
						dumTr=tr;skipRotor=rotor;
						endurVelocity=40;}
					else if(Input.GetAxis("Jump")!=0){ //hardStrafe kept TRUE until all potential strafes conclude with some Idle or Tattle (or injury)
						leaping=true;timer=0;endurVelocity=0;leaper=true;strafeLeap=false;strafeTimer2=0;
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
							endurVelocity=20;strafeJumpTimer=0;strafeLeap=false;Cloud.SetActive(false);}}
					strafeTimer2+=timey;
				}
			}
			#endregion
															#region Conjure
			else if(charState == CharacterState.Conjure)
			{
				if(enemyInfo.health<=0)
					charState=CharacterState.Dying;
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
				else if(Input.GetAxis("Eyes")!=0)
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
					chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;
					charSpeed=0;ftimer=0;meshan.CrossFade("Take 001",0.3F);meshan.speed=1;
					endurVelocity=40;rotor=tr.eulerAngles.y;}
				else if(timer>0.5F){
					if(Input.GetAxis("LBumper")>0 && ledgeNearby){
						charState=CharacterState.Ledge;leaping=false;} // Relevant ledge code here: moves Luya up to ledge's 'y'. No gravity, freezes motion.
					/*else if(Input.GetAxis("Eyes")!=0){
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
				{	if(Input.GetAxis("LBumper")>0 && ledgeNearby){
						charState=CharacterState.Ledge;leaping=false;meshan.speed=0;rootOffset=Vector3.zero;
						} // Relevant ledge code here: moves Luya up to ledge's 'y'. No gravity, freezes motion.
					else if(Input.GetAxis("Eyes")!=0){
						charState=CharacterState.Strafe;charStateLast=CharacterState.Idle;
						isStrafe=true;endurVelocity=0;endurance-=10;timer=0;strafeRotor=rotor;}
					// else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Jump") && meshan.speed==1){
							// meshan.speed=-1;meshan.Play("Jump");meshanGetCurrentAnimatorStateInfo(0).normalizedTime=1;}
					// else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Jump") && meshan.speed==-1){
						// meshan["JumpEnterJ"].speed=-1;meshan["JumpEnterJ"].time=meshan["JumpEnter"].length;
						// meshan.Play("JumpEnter");jumpForward=false;}
					else if(!meshan.GetCurrentAnimatorStateInfo(0).IsName("Jump")){
							charState=CharacterState.Idle;isIdle=true;timer=0;charStateLast=CharacterState.Jump;
							chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;
							endurVelocity=40;meshan.Play("Take 001");}}
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
							// leaping=true;timer=0;endurVelocity=0;leaper=true;isJump=true;
							// charState=CharacterState.Jump;charSpeed=880;burstVel=tr.forward*-1;}
						// else if(horiz<0){ /* Must account for grabbing&ungrabbing (LBump) */
							// leaping=true;timer=0;endurVelocity=0;leaper=true;isJump=true;
							// charState=CharacterState.Jump;charSpeed=880;burstVel=tr.right*-1;}
						// else
						// {	leaping=true;timer=0;endurVelocity=0;leaper=true;isJump=true;
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
					// chance=Random.Range(2,6)*meshan["Take 001"].length;idleState=0;transNow=false;
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
							;/* Perch? */}
						else if(ledgeType=="crawl"){ //
							charState=CharacterState.Crawl;timer=0;
							meshan.CrossFade("Crawling",0.25F);}}}
				else timer+=timey;
			}
															#endregion
															#region Hiding
			// else if(charState==CharacterState.Hiding)
			// {
				// if(Input.GetAxis("Hiding")==0){
					// if(hideTimer<=1)
						// hideTimer+=Time.deltaTime;}
				// else hideTimer=0;

				// if(enemyInfo.health<=0)
					// charState=CharacterState.Dying;
				// else if(hideTimer>=1 || endurance<=0){
					// charState=CharacterState.Idle;isIdle=true;timer=0;
					// meshan.CrossFade("Take 001",0.4F);charSpeed=0;
					// endurVelocity=20;}
				// else if(horiz!=0 || vertic!=0)
					// charSpeed=16;
				// else
					// charSpeed=0;
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
															#region Dying
			else if(charState==CharacterState.Dying)
			{
				// Play GameOver
				/*
				if(!an.IsPlaying("Death")){
					//buncha sounds flood?
					//}*/
				gameOverTimer+=timey;
			}
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
					chance=Random.Range(2,6)*2.667F;idleState=0;transNow=false;meshan.speed=1;
					endurVelocity=40;meshan.Play("Take 001");}}
			#endregion
			if(isStrafe && Input.GetAxis("Eyes")==0)
				isStrafe=false;
			if(weaponCoolDown<=7)
				weaponCoolDown+=timey;
			if(endurance>100)
				endurance=100;
			else if(endurance>=0){
				if(cursed)
					endurance+=endurVelocity*timey*0.2F;
				else endurance+=endurVelocity*timey;}
			else endurance=0;
															// velocity increases (negatively) as spamming of endurance moves increases.
			/* halls: Start with the footsteps.
				Large list of audio halls. randomly sorted through
			halltimer+=timey;
			if(halltimer>8){
				halltimer=0;GetComponent<AudioSource>().PlayOneShot(hallAudio[Random.Range(0,hallAudio.Length)],0.5F);}*/

			if(Input.GetAxis("Eyes")!=0){
				eyesOpen=true;}
			else eyesOpen=false;

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
				lastHoriz=horiz;
				lastVertic=vertic;
			if(health>100)
				health=100;
			else if(health<0)
				health=0;}
    }

	 void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.transform.name=="wall"){
			blahBlah=hit.normal;}
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
		/*endurBarlength=(Screen.width/3)*(endurance/100);
		GUI.Box(new Rect(170, 45, endurBarlength, 20), "Magic");*/
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

/*if(!Lshift){
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
			}*/
/*spriteCont.rotation=Quaternion.FromToRotation(spriteCont.up,downwards.normal);*/
/*


 idea is to have endurance as a 'physics-based' system. losing endurance is a "hit", a force, which wears on you.

 store position(value), decelerate upon "hit" (as "hit" continues), accelerate logarithmicly to this value, and then linearly increase
 slowly afterwards.

 so, endurance is basically something that is tricky... the idea is that, OVERTIME, velocity increases according to acceleration, making
 endurance go down more. Fatigue... Health does NOT play in to this.

 successfully striking can lower acceleration.

 halls increase with acceleration



 Imagine! Luya running, then hold Y, become a sort of 'floating running', which is like a running jump

 */
/*if(.98F-downwards.distance>.004F && !switchbird){
								if(!Lshift){
									slope.y-=.976F-downwards.distance;switchbird=true;}
								else
								{	slope.z-=.976F-downwards.distance;switchbird=true;}}
						if(charState!=CharacterState.Skip && downwards.transform.tag=="floor"){
							if(!Lshift){
								downwardsLast=downwards.normal;
								//me.Move(new Vector3(0,0,slope.z)); ///MUST CREATE SLOPE ACCORDING TO NORMAL.
								}
							else
							{	if(!kontakt){
									me.Move(new Vector3(0,0,slope.z));}
									//turnTranny.rotation=Quaternion.FromToRotation(orientVec,downwards.normal);
								else tr.position+=new Vector3(0,0,slope.z);}}

*/
